public class SingleProjectileWeaponItem : WeaponItem
{
  void Start()
  {
    behavior = new SingleProjectileWeapon(fireRate);
  }
}
