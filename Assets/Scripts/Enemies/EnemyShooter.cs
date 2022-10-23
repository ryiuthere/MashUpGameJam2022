using UnityEngine;

// https://weeklyhow.com/unity-top-down-character-movement/
// https://stuartspixelgames.com/2018/06/24/simple-2d-top-down-movement-unity-c/

public class EnemyShooter : EnemyChaser
{
    /** Prefab projectile object this enemy will shoot */
    [SerializeField]
    public GameObject projectile;

    /** The velocity of projectiles shot by this enemy */
    [SerializeField]
    protected float projectileSpeed = 2.4f;

    /** The duration (seconds) between shots */
    [SerializeField]
    protected float fireRate = 0.6f;
    protected float fireCooldown;

    /** How much damage this enemy's shots will do */
    [SerializeField]
    protected int projectileDamage = 15;

    protected override bool countTowardsKills
    {
        get { return true; }
    }

    public override void AI()
    {
        if (dead)
            return;
        // Shoot if we exceeded our cooldown, and reset the cooldown
        fireCooldown += Time.deltaTime;
        if (fireCooldown >= fireRate)
        {
            fireCooldown -= fireRate;
            Shoot();
        }
        base.AI();
    }

    public virtual void Shoot()
    {
        // Shoot aprojectile towards the player with our projectile stats
        GameObject proj = Instantiate(projectile, transform.position, Quaternion.identity);
        BaseProjectile projScript = proj.GetComponent<BaseProjectile>();
        projScript.movementDirection = ToPlayer;
        projScript.alignment = alignment;
        projScript.movementSpeed = projectileSpeed;
        projScript.damage = projectileDamage;
    }
}