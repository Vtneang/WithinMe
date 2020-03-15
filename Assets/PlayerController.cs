using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[Header("Input Settings")]
	public KeyCode leftKey = KeyCode.A;
	public KeyCode rightKey = KeyCode.D;
	public KeyCode jumpKey = KeyCode.W;

    [SerializeField]
    public int xSpeed = 5;
    public int ySpeed = 5;
    public float gravity = 10;

    private Rigidbody2D _rigidBody;
    private bool _movingRight = false;
    private bool _movingLeft = false;
    private bool _touchingGround = false;
    private bool isJumping = false;
    private float currXSpeed = 0;
    private float currYSpeed = 0;

    // Start is called before the first frame update
    void Start()
    {
        _rigidBody = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_touchingGround)
        {
            isJumping = false;
        }
        if (Input.GetKey("right"))
        {
            currXSpeed = xSpeed;
        }
        else if (Input.GetKey("left"))
        {
            currXSpeed = -xSpeed;
        }
        else
        {
            currXSpeed = 0;
        }
        if (Input.GetKey("space") && !isJumping) {
           currYSpeed = ySpeed;
           _touchingGround = false;
           isJumping = true;
        }
        if (!_touchingGround)
        {
            currYSpeed -= gravity * Time.deltaTime;
        }
        _rigidBody.velocity = new Vector2(currXSpeed, currYSpeed);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        foreach (ContactPoint2D contact in collision.contacts)
        {
            if (contact.point.y <= transform.position.y)
            { // Player touches the ground if any contact point is lower or at the same height as the player's bottom.
                _touchingGround = true;
                hasCheckedCollision = true;
                return;
            }
        }

        if (!hasCheckedCollision)
        {
            _touchingGround = false; // Otherwise, player is in the air.
            hasCheckedCollision = true;
        }
        else
        {
            currentSpeedX = 0; // This is for better character control, try to figure out the function of this line of code by yourself!
        }
    }
}
