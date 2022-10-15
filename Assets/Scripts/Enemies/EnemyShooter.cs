using UnityEngine;

// https://weeklyhow.com/unity-top-down-character-movement/
// https://stuartspixelgames.com/2018/06/24/simple-2d-top-down-movement-unity-c/

public class EnemyShooter : EnemyChaser
{
    [SerializeField]
    public GameObject projectile;

    [SerializeField]
    protected float projectileSpeed = 2.4f;

    [SerializeField]
    protected float fireRate = 0.6f;
    protected float fireCooldown;

    [SerializeField]
    protected int projectileDamage = 15;

    public override void AI()
    {
        fireCooldown += Time.deltaTime;
        if (fireCooldown >= fireRate)
        {
            fireCooldown -= fireRate;
            Shoot();
        }
    }

    public virtual void Shoot()
    {
        GameObject proj = Instantiate(projectile, transform.position, Quaternion.identity);
        BaseProjectile projScript = proj.GetComponent<BaseProjectile>();
        projScript.movementDirection = ToPlayer;
        projScript.alignment = alignment;
        projScript.movementSpeed = projectileSpeed;
        projScript.damage = projectileDamage;
    }
}