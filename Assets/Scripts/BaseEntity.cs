using UnityEngine;
using System;
public abstract class BaseEntity : MonoBehaviour
{
    #region Movement
    [SerializeField]
    public float movementSpeed = 10;

    public Vector2 movementDirection;

    #endregion

    protected Rigidbody2D body2D;

    /** Maximum health of entity */
    [SerializeField]
    protected int maxHealth;
    /** Remaining Heath of Entity*/
    protected int health;

    [SerializeField]
    protected Alignments alignment;

    void Start()
    {
        body2D = GetComponent<Rigidbody2D>();
        this.health = this.maxHealth;
        StartHook();
    }

    void Update()
    {
        MovementHook();
        AI();
    }

    public void OnHit(int damage) {
        // @TODO: finish once projectils exist
        this.health = Math.Max(0, this.health - damage);
        if (this.health <= 0) {
            OnDeath();
        }
    }

    /** Called in Start. Modify Start behaviors */
    public void StartHook() {}
    /** Called in Update. Controls the entity's movment */
    public abstract void MovementHook();
    /** Called once the entity's health hits 0 or less */
    public abstract void OnDeath();
    /** called in update. Should be used for AI or player input not related to movement */
    public abstract void AI();

    private void FixedUpdate()
    {
        body2D.MovePosition(body2D.position + movementDirection * movementSpeed * Time.fixedDeltaTime);
    }
}
