using UnityEngine;
using System;
public class BaseProjectile : MonoBehaviour
{
    #region Movement
    /** How fast the entity will move per second*/
    [SerializeField]
    public float movementSpeed = 10;
    /** The entity's current movement. Should be set by creator, or updated in OnUpdate if it is complex */
    public Vector2 movementDirection;

    #endregion

    protected Rigidbody2D body2D;

    /** Whether the entity is friendly or hostile towards the player */
    [SerializeField]
    public Alignments alignment;

    /** How much damage this projectile does on hit */
    [SerializeField]
    public int damage;


    /** DO NOT OVERRIDE, USE OR ADD NEW HOOKS IF MORE FUNCTIONALITY NEEDED */
    private void Start()
    {
        body2D = GetComponent<Rigidbody2D>();
        StartHook();
    }

    /** DO NOT OVERRIDE, USE OR ADD NEW HOOKS IF MORE FUNCTIONALITY NEEDED */
    private void Update()
    {
        OnUpdate();
    }

    /** DO NOT OVERRIDE, USE OR ADD NEW HOOKS IF MORE FUNCTIONALITY NEEDED */
    private void FixedUpdate()
    {
        movementDirection.Normalize();
        body2D.MovePosition(body2D.position + movementDirection * movementSpeed * Time.fixedDeltaTime);
        // Point the way we're going
        float angle = Mathf.Atan2(movementDirection.y, movementDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        OnFixedUpdate();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Makes it so that the projectile can no longer collide with the same target
        // Mainly a lazy way to let enemy projectiles pass through other enemies.
        Physics2D.IgnoreCollision(gameObject.GetComponent<Collider2D>(), collision.gameObject.GetComponent<Collider2D>(), true);

        BaseEntity target = collision.transform.GetComponent<BaseEntity>();
        PreCollision(target, collision);
        if (target != null)
        {
            // No friendly fire
            if (target.alignment == this.alignment)
            {
                return;
            }
            target.OnHit(this.damage);
        }
        // Destroy me if I shouldb e destroyed after contact
        bool destroyMe = PostCollision(target, collision);
        if (destroyMe)
        {
            Destroy(gameObject);
        }
    }

    /** Hook to modify Start behavior */
    public virtual void StartHook() {}
    /** Hook to modify FixedUpdate behavior */
    public virtual void OnFixedUpdate() { }
    /** Hook to modify Update behavior */
    public virtual void OnUpdate() { }

    /** Happens before damage is dealt/target is determined */
    public virtual void PreCollision(BaseEntity target, Collision2D collision) { }
    /** Happens after damage is dealt. Returns boolean of whether or no the projectile should get destroyed */
    public virtual bool PostCollision(BaseEntity target, Collision2D collision) { return true; }
}
