public class WeaponItem : BaseItem
{
    protected WeaponBehavior behavior;

    public override void OnPickup(Player player)
    {
        player.SetItem(gameObject);
        player.SetWeapon(behavior);
    }
}
