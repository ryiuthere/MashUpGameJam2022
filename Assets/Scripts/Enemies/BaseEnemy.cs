using UnityEngine; 

// https://weeklyhow.com/unity-top-down-character-movement/
// https://stuartspixelgames.com/2018/06/24/simple-2d-top-down-movement-unity-c/

public class BaseEnemy : BaseEntity
{
    [SerializeField]
    protected float activationRange = 12f;

    protected bool _activated = false;
    public bool activated
    {
        get
        {
            if (_activated) return true;
            _activated = Vector2.Distance(transform.position, player.transform.position) <= activationRange;
            return _activated;
        }
    }

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

    protected override void Update()
    {
        if (!activated) return;
        base.Update();
    }

    public override void OnDeath()
    {
        Destroy(gameObject);
    }

    public override void Movement() { }
    public override void AI() { }
}