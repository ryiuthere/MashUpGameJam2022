using UnityEngine;

// https://weeklyhow.com/unity-top-down-character-movement/
// https://stuartspixelgames.com/2018/06/24/simple-2d-top-down-movement-unity-c/

public class EnemyChaser : BaseEnemy
{
    /** How close to the player the enemy wants to get. */
    [SerializeField]
    protected float targetDistance = 0;

    public override void Movement()
    {
        // Go towards or away from the player to maintain my target distance
        bool tooClose = Vector2.Distance(player.transform.position, transform.position) < targetDistance;
        movementDirection = tooClose ? -this.ToPlayer : this.ToPlayer;
    }
}