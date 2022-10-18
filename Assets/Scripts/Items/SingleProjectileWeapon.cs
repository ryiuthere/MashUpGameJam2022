using UnityEngine;

public class SingleProjectileWeapon : WeaponBehavior
{
    protected string projectilePath = "Prefabs/Projectiles/PlayerProjectile";

    
    protected float fireRate = 0.6f;
    protected float fireCooldown = 0f;

    private void FireProjectile(Player player) {
        var projectile = Resources.Load(projectilePath) as GameObject;
        if (fireCooldown >= fireRate)
        {
            fireCooldown = 0;
            var proj = Object.Instantiate(projectile, player.transform.position, Quaternion.identity);
            var projectileScript = proj.GetComponent<BaseProjectile>();

            var mouseWorldCoordinates = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var towardsMouse = (mouseWorldCoordinates - player.transform.position).normalized;
            projectileScript.movementDirection = towardsMouse;
        }
    }

    public override void UpdateCooldown() {
        if (fireCooldown < fireRate) {
            fireCooldown += Time.deltaTime;
        }
    }

    public override void Attack(Player player) {
        FireProjectile(player);
    }
}
