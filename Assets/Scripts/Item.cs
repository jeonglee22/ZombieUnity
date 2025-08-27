using UnityEngine;

public class Item : MonoBehaviour, IItem
{
	public enum Types
	{
		Coin,
		Health,
		Ammo,
	}

	public Types itemType;
	public int value = 10;

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag(Defines.playerTag))
		{
			Use(other.gameObject);

			Destroy(gameObject);
		}
	}

	public void Use(GameObject go)
	{
		switch (itemType)
		{
			case Types.Coin:
			{

			}
			break;
			case Types.Health:
			{
				var health = go.GetComponent<PlayerHealth>();
				health.AddHealth(value);
			}
			break;
			case Types.Ammo:
			{
				var shooter = go.GetComponent<PlayerShooter>();
				shooter.AddAmmo(value);
			}
			break;
		}
	}
}
