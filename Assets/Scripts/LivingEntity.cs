using System;
using UnityEngine;

public class LivingEntity : MonoBehaviour, IDamagable
{
	public float MaxHealth = 100f;

	public float Health { get; set; }
	public bool IsDead { get; set; }

	public event Action OnDeath;

	protected virtual void OnEnable()
	{
		IsDead = false;
		Health = MaxHealth;
	}

	public void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
	{
		Health -= damage;

		if(Health <= 0 && !IsDead)
		{
			Die();
		}
	}

	public virtual void Die()
	{
		if(OnDeath != null)
			OnDeath();
		IsDead = true;
	}
}
