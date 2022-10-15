using UnityEngine;

// https://weeklyhow.com/unity-top-down-character-movement/
// https://stuartspixelgames.com/2018/06/24/simple-2d-top-down-movement-unity-c/

public class Player : BaseEntity
{
    [SerializeField]
    private bool useRawInput;

    [SerializeField]
    public float moveLimiter = 0.7f;

    [SerializeField] 
    protected float fireRate = 0.6f;
    protected float fireCooldown = 0f;

    [SerializeField]
    protected GameObject projectile;


    // alignment = Alignments.Friendly;

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

    public override void OnDeath()
    {
        Debug.Log("You died.");
    }

    public override void AI()
    {

    }

    public override void OnFixedUpdate()
    {
        fireCooldown += Time.fixedDeltaTime;
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
}
