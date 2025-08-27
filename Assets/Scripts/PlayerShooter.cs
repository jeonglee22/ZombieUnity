using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    public Gun gun;
	private Vector3 gunInitPos;
	private Quaternion gunInitRot;

	private Rigidbody gunRb;
	private Collider gunCollider;

    private PlayerInput input;
	private Animator animator;
	private AudioSource audioSource;

	public AudioClip itemPickupClip;

	public Transform gunPivot;
	public Transform leftHandMount;
	public Transform rightHandMount;

	private void Awake()
	{
		input = GetComponent<PlayerInput>();
		animator = GetComponent<Animator>();
		audioSource = GetComponent<AudioSource>();

		gunRb = gun.GetComponent<Rigidbody>();
		gunCollider = gun.GetComponent<Collider>();

		gunInitPos = gun.transform.localPosition;
		gunInitRot = gun.transform.localRotation;
	}

	private void OnEnable()
	{
		gunRb.linearVelocity = Vector3.zero;
		gunRb.angularVelocity = Vector3.zero;
		gun.transform.localPosition = gunInitPos;
		gun.transform.localRotation = gunInitRot;

		gunRb.isKinematic = true;
		gunCollider.enabled = false;
	}

	private void OnDisable()
	{
		gunRb.isKinematic = false;
		gunCollider.enabled = true;
	}

	private void Update()
	{
		if (input.Fire)
		{
			gun.Fire();
		}
		else if (input.Reload)
		{
			if(gun.Reload())
			{
				animator.SetTrigger(Defines.hashReload);
			}
		}
	}

	private void OnAnimatorIK(int layerIndex)
	{
		gunPivot.position = animator.GetIKHintPosition(AvatarIKHint.RightElbow);

		animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1f);
		animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1f);

		animator.SetIKPosition(AvatarIKGoal.LeftHand, leftHandMount.position);
		animator.SetIKRotation(AvatarIKGoal.LeftHand, leftHandMount.rotation);

		animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1f);
		animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1f);

		animator.SetIKPosition(AvatarIKGoal.RightHand, rightHandMount.position);
		animator.SetIKRotation(AvatarIKGoal.RightHand, rightHandMount.rotation);
	}

	public void AddAmmo(int ammo)
	{
		gun.AddAmmoRemain(ammo);
		audioSource.PlayOneShot(itemPickupClip);
	}
}
