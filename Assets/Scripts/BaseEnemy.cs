using UnityEngine;

// https://weeklyhow.com/unity-top-down-character-movement/
// https://stuartspixelgames.com/2018/06/24/simple-2d-top-down-movement-unity-c/

public class BaseEnemy : BaseEntity
{

    public override void StartHook()
    {
        this.alignment = Alignments.Enemy;
    }

    public override void Movement()
    {

    }

    public override void OnDeath()
    {
        Destroy(gameObject);
    }

    public override void AI()
    {
    }
}