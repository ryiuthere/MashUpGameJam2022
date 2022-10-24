public class MultiProjectileWeaponItem : WeaponItem {
    void Start() {
        behavior = new MultiProjectileWeapon(fireRate);
    }
}
