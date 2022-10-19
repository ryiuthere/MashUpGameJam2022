using UnityEngine; 

// https://weeklyhow.com/unity-top-down-character-movement/
// https://stuartspixelgames.com/2018/06/24/simple-2d-top-down-movement-unity-c/

public class BaseEnemy : BaseEntity
{
    [SerializeField]
    protected float activationRange = 12f;

    /** Melee damage */
    [SerializeField]
    protected int contactDamage = 5;

    protected bool _activated = false;

    protected GameObject player;

    /** Get the normalied vector from this enemy to the player. (Convenience function) */
    public Vector2 ToPlayer
    {
        get => (player.transform.position - this.transform.position).normalized;
    }

    public override void StartHook()
    {
        this.alignment = Alignments.Enemy;
        this.player = GameObject.Find("Player");
    }

    public override void OnDeath()
    {
        Destroy(gameObject);
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        Player target = collision.gameObject.GetComponentInParent<Player>();
        if (target != null)
        {
            target.OnHit(contactDamage);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        // iframes willprevent this from immediately deleting the player
        Player target = collision.gameObject.GetComponentInParent<Player>();
        if (target != null)
        {
            target.OnHit(contactDamage);
        }
    }

    public override void Movement() { }
    public override void AI() { }
    public override bool ShouldUpdate()
    {
        if (_activated) return true;
        _activated = Vector2.Distance(transform.position, player.transform.position) <= activationRange;
        return _activated;
    }
}