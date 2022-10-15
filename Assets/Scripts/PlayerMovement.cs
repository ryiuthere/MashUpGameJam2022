using UnityEngine;

// https://weeklyhow.com/unity-top-down-character-movement/
// https://stuartspixelgames.com/2018/06/24/simple-2d-top-down-movement-unity-c/

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed = 10;
    
    [SerializeField]
    private bool useRawInput;
    
    [SerializeField]
    private float moveLimiter = 0.7f;

    private Vector2 movementDirection;
    
    private Rigidbody2D body2D;

    void Start()
    {
        body2D = GetComponent<Rigidbody2D>();
    }

    void Update()
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

    private void FixedUpdate()
    {
        body2D.MovePosition(body2D.position + movementDirection * movementSpeed * Time.fixedDeltaTime);
    }
}
