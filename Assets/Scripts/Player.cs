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
    [SerializeField]
    protected ExpIndicator expIndicator;

    /** Invulnerability seconds after taking damage  */
    [SerializeField]
    protected float iframes = 0.2f;
    protected float iframeCooldown;

    // [SerializeField]
    // protected float fireRate = 0.6f;
    // protected float fireCooldown = 0f;

    protected int experience = 150;

    // [SerializeField]
    // protected GameObject projectile;

    [SerializeField]
    protected GameObject item;
    protected WeaponBehavior weapon;

    public override void StartHook()
    {
        healthBar.UpdateHealthBar(this.health, this.maxHealth);
        // if (weapon != null) {
        //     itemIndicator.UpdateItemIndicator(item);
        // }
        // expIndicator.UpdateExp(experience);
        alignment = Alignments.Friendly;
        iframeCooldown = iframes;
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

        if (movementDirection.x != 0 && movementDirection.y != 0)
        {
            movementDirection *= moveLimiter;
        }

        if (Input.GetButton("Fire1")) {
            if (weapon != null) {
                weapon.Attack(this);
            }
        }
    }

    public override void OnHit(int damage)
    {
        if (iframeCooldown >= iframes)
        {
            base.OnHit(damage);
            iframeCooldown = 0;
        }
        healthBar.UpdateHealthBar(this.health, this.maxHealth);
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
        // fireCooldown += Time.deltaTime;
        // if (fireCooldown >= fireRate)
        // {
        //     fireCooldown -= fireRate;
        //     var proj = Instantiate(projectile, transform.position, Quaternion.identity);
        //     var projectileScript = proj.GetComponent<BaseProjectile>();

        //     var mouseWorldCoordinates = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //     var towardsMouse = (mouseWorldCoordinates - transform.position).normalized;
        //     projectileScript.movementDirection = towardsMouse;
        // }
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
}
