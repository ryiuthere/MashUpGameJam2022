using UnityEngine;
using UnityEngine.SceneManagement;

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
        healthBar.UpdateHealthBar(health, maxHealth);
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
            movementDirection = Vector3.ClampMagnitude(movementDirection, 1f);
        }
    }

    public override void OnHit(int damage)
    {
        if (iframeCooldown >= iframes && damage > 0)
        {
            base.OnHit(damage);
            iframeCooldown = 0;
            squashAndStretch.customSquish(0.8f, 1, 0.2f);
            AudioManager.Instance.PlaySound(SoundType.Hit);
        }
        healthBar.UpdateHealthBar(health, maxHealth);
    }

    public void Kill()
    {
        iframes = iframeCooldown;
        OnHit(health);
    }

    public override void OnDeath()
    {
        PlayerPrefs.SetFloat("timeRemaining", CountdownTimer.Instance.CurrentTime);
        PlayerPrefs.SetInt("gameWon", 0);
        PlayerPrefs.Save();
        SceneManager.LoadScene("End Screen");
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

    public void OnShoot()
    {
        squashAndStretch.customSquish(1, 0.8f, 0.15f);
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
        health = Mathf.Clamp(health + healing, 0, maxHealth);
        healthBar.UpdateHealthBar(health, maxHealth);
    }
}
