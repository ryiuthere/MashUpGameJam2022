using UnityEngine;

public class MultiProjectileWeapon : WeaponBehavior
{
    public MultiProjectileWeapon (float initFireRate = 1f) {
        fireRate = initFireRate;
    }
    protected string projectilePath = "Prefabs/Projectiles/PlayerProjectile";
    
    protected float fireRate;
    
    protected float fireCooldown = 0f;

    private void FireProjectile(Player player) {
        var projectile = Resources.Load(projectilePath) as GameObject;
        if (fireCooldown >= fireRate)
        {
            fireCooldown = 0;

            var proj1 = Object.Instantiate(projectile, player.transform.position, Quaternion.identity);
            var proj2 = Object.Instantiate(projectile, player.transform.position, Quaternion.identity);
            var proj3 = Object.Instantiate(projectile, player.transform.position, Quaternion.identity);
            
            var projectileScript1 = proj1.GetComponent<BaseProjectile>();
            var projectileScript2 = proj2.GetComponent<BaseProjectile>();
            var projectileScript3 = proj3.GetComponent<BaseProjectile>();

            // Get coordinates pointing towards mouse
            var mouseWorldCoordinates = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var towardsMouse = (mouseWorldCoordinates - player.transform.position);

            projectileScript1.movementDirection = towardsMouse.normalized;
            projectileScript2.movementDirection = (Quaternion.Euler(0, 0, -20) * towardsMouse).normalized;
            projectileScript3.movementDirection = (Quaternion.Euler(0, 0, 20) * towardsMouse).normalized;
            AudioManager.Instance.PlaySound(SoundType.PlayerShoot);
            player.OnShoot();
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
