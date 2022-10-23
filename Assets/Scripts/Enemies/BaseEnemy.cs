using System.Collections;
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

    protected bool dead = false;

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
        dead = true;
        StartCoroutine(Death());
    }

    protected IEnumerator Death()
    {
        squashAndStretch.SetToSquash(1);
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        Player target = collision.gameObject.GetComponentInParent<Player>();
        if (target != null && !dead)
        {
            target.OnHit(contactDamage);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        // iframes willprevent this from immediately deleting the player
        Player target = collision.gameObject.GetComponentInParent<Player>();
        if (target != null && !dead)
        {
            target.OnHit(contactDamage);
        }
    }

    public override void Movement() { }

    public override void AI() {
        if (dead)
            return;

        spriteRenderer.flipX = player.transform.position.x - transform.position.x > 0;
        animator.SetBool("Moving", movementDirection != Vector2.zero);
    }

    public override bool ShouldUpdate()
    {
        if (dead) return false;
        if (_activated) return true;
        bool inRange = Vector2.Distance(transform.position, player.transform.position) <= activationRange;
        // Only check LoS if in range to save computation
        if (inRange) {
            // layer mask only allows it to see things on the default layer, which should be the player's melee htibox and walls
            LayerMask mask = LayerMask.GetMask(new string[] { "Default" });
            RaycastHit2D LoS = Physics2D.Raycast(gameObject.transform.position,  this.ToPlayer, activationRange, mask);
            // Should be terrain or the player's melee hitbox
            if (LoS.collider.GetComponentInParent<Player>() != null)
            {
                _activated = true;
            }

        }

        return _activated;
    }
}