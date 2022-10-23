using UnityEngine;

// https://weeklyhow.com/unity-top-down-character-movement/
// https://stuartspixelgames.com/2018/06/24/simple-2d-top-down-movement-unity-c/

public class Player : BaseEntity
{
    [SerializeField]
    private bool useRawInput;

    [SerializeField]
    public float moveLimiter = 0.7f;

    /** HUD variables **/
    [SerializeField]
    protected HealthBar healthBar;
    [SerializeField]
    protected ItemIndicator itemIndicator;

    /** Invulnerability seconds after taking damage  */
    [SerializeField]
    protected float iframes = 0.2f;
    protected float iframeCooldown;

    protected GameObject item;
    protected WeaponBehavior weapon;

    public override void StartHook()
    {
        healthBar.UpdateHealthBar(this.health, this.maxHealth);
        alignment = Alignments.Friendly;
        iframeCooldown = iframes;
        animator.SetBool("Running", false);
        base.StartHook();
    }

    public override void Movement()
    {
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

        if (movementDirection.x > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (movementDirection.x < 0)
        {
            spriteRenderer.flipX = true;
        }

        if (movementDirection.x != 0 || movementDirection.y != 0)
        {
            animator.SetBool("Running", true);
        }
        else
        {
            animator.SetBool("Running", false);

        }

        if (movementDirection.x != 0 && movementDirection.y != 0)
        {
            movementDirection *= moveLimiter;
        }
    }

    public override void OnHit(int damage)
    {
        if (iframeCooldown >= iframes && damage > 0)
        {
            base.OnHit(damage);
            iframeCooldown = 0;
        }
        healthBar.UpdateHealthBar(this.health, this.maxHealth);
    }

    public void Kill()
    {
        iframes = iframeCooldown;
        OnHit(health);
    }

    public override void OnDeath()
    {
        Debug.Log("You died.");
        // @TODO: better death
        this.transform.rotation = new Quaternion(90, 0, 0, 0);
    }

    public override void AI()
    {
        iframeCooldown += Time.deltaTime;
        if (weapon != null) {
            weapon.UpdateCooldown();
        }

        if (Input.GetButton("Fire1")) {
            if (weapon != null) {
                weapon.Attack(this);
            }
        }
    }

    public override bool ShouldUpdate()
    {
        return health > 0;
    }

    public void SetItem(GameObject newItem) {
        item = newItem;
        itemIndicator.UpdateItemIndicator(item);
    }

    public void SetWeapon(WeaponBehavior newWeapon) {
        weapon = newWeapon;
       
    }

    public void Heal(int healing) {
        health = Mathf.Clamp(healing, 0, maxHealth);
        healthBar.UpdateHealthBar(this.health, this.maxHealth);
    }
}
