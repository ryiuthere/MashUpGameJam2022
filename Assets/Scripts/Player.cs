using UnityEngine;

// https://weeklyhow.com/unity-top-down-character-movement/
// https://stuartspixelgames.com/2018/06/24/simple-2d-top-down-movement-unity-c/

public class Player : BaseEntity
{
    [SerializeField]
    private bool useRawInput;

    [SerializeField]
    public float moveLimiter = 0.7f;

    public HealthBar healthBar;

    // alignment = Alignments.Friendly;

    public override void Movement() {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            this.OnHit(1);
        }
        if (useRawInput)
        {
            movementDirection.x = Input.GetAxisRaw("Horizontal");
            movementDirection.y = Input.GetAxisRaw("Vertical");
        }
        else
        {
            movementDirection.x = Input.GetAxis("Horizontal");
            movementDirection.y = Input.GetAxis("Vertical");
        }

        if (movementDirection.x != 0 && movementDirection.y != 0)
        {
            movementDirection *= moveLimiter;
        }
    }

    public override void OnHit(int damage)
    {
        base.OnHit(damage);
        healthBar.UpdateHealthBar(this.health - damage, this.maxHealth);
    }

    public override void OnDeath() {
        Debug.Log("You died.");
    }

    public override void AI() {
        // TODO: add shooting logic. remove placeholder dieing
        if (movementDirection.magnitude >= 0.8f)
        {
            this.OnHit(1);
        }
    }
}
