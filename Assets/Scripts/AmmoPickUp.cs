public class AmmoPickUp : PickUp
{
    private int ammoUp = 20;

	public PlayerShooter shooter;

	protected override void ItemAbility()
	{
		base.ItemAbility();

		shooter.AddAmmo(ammoUp);
	}
}
