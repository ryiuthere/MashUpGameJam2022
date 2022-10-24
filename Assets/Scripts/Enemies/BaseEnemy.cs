using System.Collections;
using UnityEngine;

// https://weeklyhow.com/unity-top-down-character-movement/
// https://stuartspixelgames.com/2018/06/24/simple-2d-top-down-movement-unity-c/

public class BaseEnemy : BaseEntity
{
    [SerializeField]
    protected bool isBossEnemy = false;

    [SerializeField]
    protected float activationRange = 12f;

    /** Melee damage */
    [SerializeField]
    protected int contactDamage = 5;

    protected bool _activated = false;

    protected GameObject player;

    protected bool dead = false;

    protected virtual bool countTowardsKills
    {
        get { return false; }
    }

    /** Get the normalied vector from this enemy to the player. (Convenience function) */
    public Vector2 ToPlayer
    {
        get => (player.transform.position - this.transform.position).normalized;
    }

    private void Awake()
    {
        if (isBossEnemy)
        {
            // Needs to occur before start for squash
            transform.localScale = new Vector3(2, 2);
        }
    }

    public override void StartHook()
    {
        alignment = Alignments.Enemy;
        player = GameObject.Find("Player");
        if (isBossEnemy)
        {
            spriteRenderer.color = new Color(1, 0.7f, 0.7f);
            animator.speed = 0.5f;
        }
    }

    public override void OnDeath()
    {
        if (dead)
        {
            return;
        }
        if (countTowardsKills)
        {
            PlayerPrefs.SetInt("exterminations", PlayerPrefs.GetInt("exterminations", 0) + 1);
        }
        if (isBossEnemy)
        {
            CountdownTimer.Instance.AddTime(30f);
        }
        var collider = GetComponent<BoxCollider2D>();
        if (collider != null)
        {
            collider.enabled = false;
        }
        dead = true;
        StartCoroutine(Death());
    }

    protected IEnumerator Death()
    {
        AudioManager.Instance.PlaySound(SoundType.Explosion);
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

        if (countTowardsKills)
            spriteRenderer.flipX = player.transform.position.x - transform.position.x > 0;
        animator.SetBool("Moving", movementDirection != Vector2.zero);
    }

    public override void OnHit(int damage)
    {
        base.OnHit(damage);
        AudioManager.Instance.PlaySound(SoundType.Hit);
        if (!_activated)
            _activated = true;
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
                foreach(var enemy in FindObjectsOfType<BaseEnemy>())
                {
                    if (Vector2.Distance(transform.position, enemy.transform.position) < 3)
                    {
                        enemy._activated = true;
                    }
                }
            }

        }

        return _activated;
    }
}