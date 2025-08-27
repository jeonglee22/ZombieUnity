using UnityEngine;

public class PickUp : MonoBehaviour
{
	public static readonly string playerStr = "Player";

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag(playerStr))
		{
			ItemAbility();
			gameObject.SetActive(false);
		}
	}

	protected virtual void ItemAbility() {}
}
