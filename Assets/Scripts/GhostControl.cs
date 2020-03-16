using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostControl : MonoBehaviour {
    public bool enableEditorDebug;
    public GameObject player;

    [Header("Movement Settings")]
    [Range(0f, 20f)]
    public float speed = 5;
    [Range(0f, 20f)]
    public float acceleration = 2;
    [Range(0f, 40f)]
    public float stopPower = 10;

    [Header("Game Settings")]
    [Range(1f, 100f)]
    public float timeLimit = 20;

    [Header("Graphics Settings")]
    public SpriteRenderer renderer;

    [Header("Physics Settings")]
    public Rigidbody2D rigidbody;

    [Header("Player Conditions (Do not modify these fields through Editor)")]
    public float currentSpeedX;
    public float currentSpeedY;
    public float timeLeft;
    public bool toggled;
    public bool blockedX;
    public bool blockedY;
    public bool hasCheckedCollision;

    // Reference to the player.
    public PlayerControl playerScript;
    // Receives the key inputs from the player at Start().
    private KeyCode leftKey;
    private KeyCode rightKey;
    private KeyCode upKey;
    private KeyCode downKey;

    // Start is called before the first frame update
    void Start() {
        player = GameObject.Find("Player");
        playerScript = player.GetComponent<PlayerControl>();
        leftKey = playerScript.leftKey;
        rightKey = playerScript.rightKey;
        upKey = playerScript.upKey;
        downKey = playerScript.downKey;
        if (enableEditorDebug) Debug.Log("PlayerController.Start() is called.");
    }

    // Update is called once per frame
    void Update() {
        if (enableEditorDebug) Debug.Log("PlayerController.Update() is called.");

        // Get the time elapsed since last frame and decrease countdown accordingly.
        float timeElapsed = Time.deltaTime;
        timeLeft -= timeElapsed;
        // Remove ghost from scene if there is no time left in countdown
        if (timeLeft <= 0) {
            toggleOff(false); 
        }

        // Get the current postion of player-controlled ghost.
        Vector2 currentPosition = transform.position;

        if (blockedX) {
            currentSpeedX = 0;
        } else if (blockedY) {
            currentSpeedY = 0;
        }

        // Check for inputs.
        bool leftPressed = Input.GetKey(leftKey);
        bool rightPressed = Input.GetKey(rightKey);
        bool upPressed = Input.GetKey(upKey);
        bool downPressed = Input.GetKey(downKey);

        float prevAccel = acceleration;
        // Change acceleration when moving diagonally.
        if ((upPressed || downPressed) && (leftPressed || rightPressed)) {
            acceleration = acceleration * Mathf.Pow(2, 0.5f);
        }

        // Process horizontal speed.
        if (leftPressed && !rightPressed && currentSpeedX <= 0) {
            currentSpeedX = Mathf.MoveTowards(currentSpeedX, -speed, acceleration * timeElapsed);
        } else if (!leftPressed && rightPressed && currentSpeedX >= 0) {
            currentSpeedX = Mathf.MoveTowards(currentSpeedX, speed, acceleration * timeElapsed);
        } else {
            currentSpeedX = Mathf.MoveTowards(currentSpeedX, 0, stopPower * timeElapsed);
        }

        // Process vertical speed.
        if (upPressed && !downPressed && currentSpeedY >= 0) {
            currentSpeedY = Mathf.MoveTowards(currentSpeedY, speed, acceleration * timeElapsed);
        } else if (!upPressed && downPressed && currentSpeedY <= 0) {
            currentSpeedY = Mathf.MoveTowards(currentSpeedY, -speed, acceleration * timeElapsed);
        } else {
            currentSpeedY = Mathf.MoveTowards(currentSpeedY, 0, stopPower * timeElapsed);
        }

        // Revert acceleration change.
        acceleration = prevAccel;

        rigidbody.velocity = new Vector2(currentSpeedX, currentSpeedY);
        blockedX = false;
        blockedY = false;
        hasCheckedCollision = true;
    }

    public void toggleOn() {
        // Set the ghost's position as the player's position
        transform.position = player.transform.position;
        timeLeft = timeLimit;
        gameObject.SetActive(true);
    }

    public void toggleOff(bool forced) {
        if (!forced) {
            playerScript.toggleOff();
        }
        gameObject.SetActive(false);
    }
}