using UnityEngine;

public class WeaponItem : BaseItem {
    public WeaponItem() {}
    
    [SerializeField]
    protected float fireRate = 1f;

    protected WeaponBehavior behavior;

    public override void OnPickup(Player player)
    {
        player.SetItem(gameObject);
        player.SetWeapon(behavior);
    }
}
