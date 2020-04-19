using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour {
    public GameObject ghost;

    [Header("Input Settings")]
    public KeyCode leftKey = KeyCode.A;
    public KeyCode rightKey = KeyCode.D;
    public KeyCode upKey = KeyCode.W;
    public KeyCode downKey = KeyCode.S;
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode resetKey = KeyCode.R;
    public KeyCode toggleKey = KeyCode.E;

    [Header("Movement Settings")]
    [Range(0f, 20f)]
    // Initial speed on left and right movement.
    public float speedX = 3;
    [Range(0f, 30f)]
    // Initial speed on jump.
    public float speedY = 7;
    [Range(0f, 50f)]
    public float gravity = 7;
    [Range(0f, 2f)]
    public float topOffset = 0.5f;
    [Range(0f, 2f)]
    public float bottomOffset = 0.5f;
    [Range(0f, 1f)]
    public float onAirAcceleration = 0.1f;
    [Range(0f, 200f)]
    public float maxFallSpeed = -100;
    [Range(0f, 5f)]
    public float bot = .5f;
    public bool is_in_light;

    [Header("Graphics Settings")]
    public SpriteRenderer renderer;

    [Header("Physics Settings")]
    public Rigidbody2D rigidbody;

    [Header("Player Conditions (Do not modify these fields through Editor)")]

    public Color Changer;
    public Color Cur;
    public float ChangerTime;

    // Current speed in x-axis
    private float dx = 0;
    // Current speed in y-axis
    private float dy = 0;
    // True if player is at an on-ground state.
    [SerializeField]
    private bool onGround;
    // True if the player character is being controlled.
    private bool isInControl = true;
    // Reference to ghost script.
    public GhostControl ghostScript;
    // Memorize the velocity of the player when switching to the ghost state.
    private Vector2 memorizeVelocity;
    private bool hasCheckedCollision;
    private int jumpFrame;

    void Start() {
        ghost = GameObject.Find("Ghost");
        ghostScript = ghost.GetComponent<GhostControl>();
        ghost.SetActive(false);
    }

    // Update is called once per frame
    void Update() {
        // dt.
        float elapsed = Time.deltaTime;

        // Reloads the scene when pressing the resetKey.
        if (Input.GetKey(resetKey)) {
            loadSceneItself();
        }

        if (Input.GetKeyDown(toggleKey)) {
            if (isInControl) {
                Debug.Log("ToggleOn.");
                toggleOn();
            } else {
                Debug.Log("ToggleOff.");
                toggleOff();
            }
        }
        
        if (onGround) {
            // Changes the X-axis speed on player input.
            if (isInControl) {
                if (Input.GetKey(rightKey)) {
                    dx = speedX;
                } else if (Input.GetKey(leftKey)) {
                    dx = -speedX;
                } else {
                    dx = 0;
                }
                
                // Jump.
                if (Input.GetKeyDown(jumpKey)) {
                    dy = speedY;
                    jumpFrame = Time.frameCount;
                } else {
                    if (Time.frameCount != jumpFrame + 1) jumpFrame = -1;
                    dy = 0;
                }
            } else {
                dx = 0;
                if (Time.frameCount != jumpFrame + 1) jumpFrame = -1;
            }
        } else if (!onGround) {
            // Cap on the maximum negative Y-axis speed.
            dy = Mathf.Max(maxFallSpeed, dy - gravity * elapsed);
            if (isInControl) {
                // Changes the speed by a constant factor of the speedX when not onGround.
                if (Input.GetKey(rightKey) && dx < 0) {
                    dx = Mathf.Min(0, dx + speedX * onAirAcceleration);
                } else if (Input.GetKey(leftKey) && dx > 0) {
                    dx = Mathf.Max(0, dx - speedX * onAirAcceleration);
                }
            }
        }

        /*
        // Updates the speed of the player.
        rigidbody.velocity = new Vector2(dx, dy);
        onGround = false;
        hasCheckedCollision = false;
        */

        /*
        if (Input.GetKey(KeyCode.C))
        {
            if (sprite.color == Changer)
            {
                Debug.Log("Trying");
                StartCoroutine(ChangeColor(Cur, ChangerTime));
            }
            else
            {
                StartCoroutine(ChangeColor(Changer, ChangerTime));
            }
        }*/
    }

    private void FixedUpdate() {
        // Updates the speed of the player.
        rigidbody.velocity = new Vector2(dx, dy);
        onGround = false;
        hasCheckedCollision = false;
    }

    // Switch into the ghost state
    private void toggleOn() {
        isInControl = false;
        // Memorize the current player's velocity and freeze the player.
        memorizeVelocity = rigidbody.velocity;
        // rigidbody.velocity = new Vector2(0, 0);
        //Make the player character dark or whatever we want to do.
        //////////////////////TOOOOOOOOOODOOOOOOOOOOOOO//////////////////////////////

        ghostScript.toggleOn();
    }

    // Switch out of the ghost state
    public void toggleOff() {
        isInControl = true;
        // Un-freeze the player.
        // rigidbody.velocity = memorizeVelocity;
        ghostScript.toggleOff(true);
    }

    public void loadScene(int sceneBuildIndex) {
        SceneManager.LoadScene(sceneBuildIndex);
    }

    public void loadScene(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }

    public void loadSceneItself() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
  
    private IEnumerator ChangeColor(Color c, float i) {
        while (renderer.color != c) {
            renderer.color = Color.Lerp(renderer.color, c, i / 100);
            yield return null;
        }
    }

    /*
    // When on the ground
    private void OnCollisionStay2D(Collision2D collision) {
        if (collision.transform.position.y <= transform.position.y - bot) {
            onGround = true;
        }
        dy = 0;
    }
    */
    
    private void OnCollisionStay2D(Collision2D collision) {
        foreach (ContactPoint2D contact in collision.contacts) {
            float yOffset = transform.position.y - contact.point.y;
            if (bottomOffset >= yOffset && yOffset > 0.4f) { // Player touches the ground if any contact point is lower or at the same height as the player's bottom.
                onGround = true;
                hasCheckedCollision = true;
                // Debug.Log("Bottom Hits");
            } else if (yOffset < 0 && topOffset > -yOffset && -yOffset > 0.4f) { // Player hits the ceiling.
                if (dy > 0) dy = 0;
                // Debug.Log("Top Hits");
            } else {
                if (Time.frameCount == jumpFrame + 1 && jumpFrame != -1) continue;
                if (dy > 0) dy *= .5f;
                /*
                if (Time.frameCount == jumpFrame + 1 && jumpFrame != -1) continue;
                float xOffset = transform.position.x - contact.point.x;
                if (xOffset > 0.45 && dx < 0) dx = 0;
                else if (xOffset < -0.45 && dx > 0) dx = 0;
                if (dy > 0) dy = 0;
                Debug.Log("Side Hits");
                */
            }
        }
		
        if (!hasCheckedCollision) {
            onGround = false; // Otherwise, player is in the air.
            hasCheckedCollision = true;
        }
    }
}
