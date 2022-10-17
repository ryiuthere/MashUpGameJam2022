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

    // alignment = Alignments.Friendly;
    [SerializeField] 
    protected float fireRate = 0.6f;
    protected float fireCooldown = 0f;

    protected int experience = 150;

    [SerializeField]
    protected GameObject projectile;

    public override void StartHook()
    {
        healthBar.UpdateHealthBar(this.health, this.maxHealth);
        itemIndicator.UpdateItemIndicator(projectile);
        expIndicator.UpdateExp(experience);
        // itemIndicator.UpdateItemIndicator()
        alignment = Alignments.Friendly;
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
    }

    public override void OnHit(int damage)
    {
        base.OnHit(damage);
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
        fireCooldown += Time.deltaTime;
        if (fireCooldown >= fireRate)
        {
            fireCooldown -= fireRate;
            var proj = Instantiate(projectile, transform.position, Quaternion.identity);
            var projectileScript = proj.GetComponent<BaseProjectile>();

            var mouseWorldCoordinates = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var towardsMouse = (mouseWorldCoordinates - transform.position).normalized;
            projectileScript.movementDirection = towardsMouse;
        }
    }

    public override bool ShouldUpdate()
    {
        return health > 0;
    }
}
