using UnityEngine;

public class SingleProjectileWeaponItem : WeaponItem {
    void Start() {
        Debug.Log("INSTANTIATING: " + fireRate);
        behavior = new SingleProjectileWeapon(fireRate);
    }
}
