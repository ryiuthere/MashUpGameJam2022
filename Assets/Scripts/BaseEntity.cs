using UnityEngine;
using System;
public abstract class BaseEntity : MonoBehaviour
{
    #region Movement
    /** How fast the entity will move per second*/
    [SerializeField]
    public float movementSpeed = 10;
    /** The entity's current movement. Should get updated in Movement() */
    protected Vector2 movementDirection;

    #endregion

    protected Rigidbody2D body2D;

    /** Maximum health of entity */
    [SerializeField]
    protected int maxHealth;
    /** Remaining Heath of Entity*/
    protected int health;

    /** Whether the entity is friendly or hostile towards the player */
    [SerializeField]
    public Alignments alignment { get; protected set; }

    protected Animator animator;

    /** DO NOT OVERRIDE, USE OR ADD NEW HOOKS IF MORE FUNCTIONALITY NEEDED */
    private void Start()
    {
        body2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        this.health = this.maxHealth;
        StartHook();
    }

    /** DO NOT OVERRIDE, USE OR ADD NEW HOOKS IF MORE FUNCTIONALITY NEEDED. */
    protected virtual void Update()
    {
        if (!ShouldUpdate())
        {
            return;
        }
        Movement();
        AI();
    }

    /** DO NOT OVERRIDE, USE OR ADD NEW HOOKS IF MORE FUNCTIONALITY NEEDED */
    private void FixedUpdate()
    {
        if (!ShouldUpdate())
        {
            return;
        }
        
        animator.SetBool("Moving", movementDirection != Vector2.zero);
        body2D.MovePosition(body2D.position + movementDirection * movementSpeed * Time.fixedDeltaTime);
        OnFixedUpdate();
    }

    /** Triggered when damage is taken, should almost always call base.OnHit() when overriding*/
    public virtual void OnHit(int damage) {
        // @TODO: finish once projectiles exist
        this.health = Math.Max(0, this.health - damage);
        if (this.health <= 0) {
            OnDeath();
        }
    }

    /** Hook to modify Start behavior */
    public virtual void StartHook() {}
    /** Hook to modify FixedUpdate behavior */
    public virtual void OnFixedUpdate() { }
    /** Called in Update. Controls the entity's movment */
    public abstract void Movement();
    /** called in update. Should be used for AI or player input not related to movement */
    public abstract void AI();
    /** Called once the entity's reaches 0 */
    public abstract void OnDeath();

    public virtual bool ShouldUpdate() { return true; }
}
