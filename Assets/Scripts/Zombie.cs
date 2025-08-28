using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : LivingEntity
{

	public enum Status
	{
		Idle,
		Trace,
		Attack,
		Die,
	}

	private Status currentStatus;

	public Status CurrentStatus
	{
		get { return currentStatus; }
		set 
		{
			var prevStatus = currentStatus;
			currentStatus = value;
			switch (currentStatus)
			{
				case Status.Idle:
					animator.SetBool(Defines.hashHasTarget, false);
					agent.isStopped = true;
					break;
				case Status.Trace:
					animator.SetBool(Defines.hashHasTarget, true);
					agent.isStopped = false;
					break;
				case Status.Attack:
					animator.SetBool(Defines.hashHasTarget, false);
					agent.isStopped = true;
					break;
				case Status.Die:
					animator.SetTrigger(Defines.hashDie);
					agent.isStopped = true;
					collider.enabled = false;
					break;
			}
		}
	}

	private Transform target;

	public float traceDistance;
	public float attackDistance;

	public float damage = 10;
	public float lastAttackTime;
	public float attackInterval = 0.5f;

	private Animator animator;
	private NavMeshAgent agent;
	private Collider collider;

	private AudioSource audioSource;
	public AudioClip damageClip;
	public AudioClip deathClip;

	public ParticleSystem bloodEffect;

	public Renderer zombeiRenderer;

	private void Awake()
	{
		agent = GetComponent<NavMeshAgent>();
		animator = GetComponent<Animator>();
		collider = GetComponent<Collider>();
		audioSource = GetComponent<AudioSource>();
	}

	private void Update()
	{
		switch (currentStatus)
		{
			case Status.Idle:
				UpdateIdle();
				break;
			case Status.Trace:
				UpdateTrace();
				break;
			case Status.Attack:
				UpdateAttack();
				break;
			case Status.Die:
				UpdateDie();
				break;
		}
	}

	private void UpdateDie()
	{
	}

	private void UpdateAttack()
	{
		if (target == null ||
			(target != null &&
			Vector3.Distance(transform.position, target.position) > attackDistance))
		{
			CurrentStatus = Status.Trace;
			return;
		}

		var lookAt = target.position;
		lookAt.y = transform.position.y;
		transform.LookAt(lookAt);

		if(lastAttackTime + attackInterval < Time.time)
		{
			lastAttackTime = Time.time;

			var damagable = target.GetComponent<IDamagable>();
			if (damagable != null)
			{
				damagable.OnDamage(damage, transform.position, -Vector3.forward);
			}
		}
	}

	private void UpdateTrace()
	{
		if (target != null &&
			Vector3.Distance(transform.position, target.position) < attackDistance)
		{
			CurrentStatus = Status.Attack;
			return;
		}

		if (target == null ||
			Vector3.Distance(transform.position, target.position) > traceDistance)
		{
			CurrentStatus = Status.Idle;
			return;
		}

		agent.SetDestination(target.position);
	}

	private void UpdateIdle()
	{
		if (target != null &&
			Vector3.Distance(transform.position, target.position) < traceDistance)
		{
			CurrentStatus = Status.Trace;
			return;
		}

		target = FindTarget(traceDistance);
	}

	protected override void OnEnable()
	{
		base.OnEnable();

		collider.enabled = true;
		currentStatus = Status.Idle;
	}

	public override void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
	{
		base.OnDamage(damage, hitPoint, hitNormal);
		audioSource.PlayOneShot(damageClip);

		bloodEffect.transform.position = hitPoint;
		bloodEffect.transform.forward = hitNormal;
		bloodEffect.Play();
	}

	public override void Die()
	{
		base.Die();
		audioSource.PlayOneShot(deathClip);
		CurrentStatus = Status.Die;
	}

	public LayerMask targetLayer;

	protected Transform FindTarget(float radius)
	{
		var colliders = Physics.OverlapSphere(transform.position, radius, targetLayer.value);
		if (colliders.Length == 0)
		{
			return null;
		}

		var target = colliders.OrderBy(
			x => Vector3.Distance(x.transform.position, transform.position)).First();
		return target.transform;
	}

	public void SetUp(ZombieData data)
	{
		MaxHealth = data.maxHP;
		damage = data.damage;

		agent.speed = data.speed;
		zombeiRenderer.material.color = data.skinColor;
	}
}
