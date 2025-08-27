public class HealthPickUp : PickUp
{
    private float healthUp = 20f;

    public PlayerHealth playerHealth;

	private void Update()
	{
		
	}

	protected override void ItemAbility()
	{
		base.ItemAbility();
        
        playerHealth.AddHealth(healthUp);
	}
}
