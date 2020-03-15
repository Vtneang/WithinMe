using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostControll : MonoBehaviour
{
    public bool enableEditorDebug;

    [Header("Input Settings")]
    public KeyCode leftKey = KeyCode.A;
    public KeyCode rightKey = KeyCode.D;
    public KeyCode upKey = KeyCode.W;
    public KeyCode downKey = KeyCode.S;
    public KeyCode toggleKey = KeyCode.E;

    [Header("Movement Settings")]
    [Range(0f, 20f)]
    public float speed = 5;
    [Range(0f, 20f)]
    public float acceleration = 2;
    [Range(0f, 40f)]
    public float stopPower = 30;

    [Header("Graphics Settings")]
    public SpriteRenderer renderer;

    [Header("Physics Settings")]
    public Rigidbody2D rigidbody;
    [Range(0f, 5f)]
    public float bottomOffset = 0.5f;

    [Header("Player Conditions (Do not modify these fields through Editor)")]
    public float currentSpeedX;
    public float currentSpeedY;
    public float timeLeft;
    public bool toggled;
    public bool blockedX;
    public bool blockedY;
    public bool hasCheckedCollision;

    // Start is called before the first frame update
    void Start()
    {
        timeLeft = 20f;
        if (enableEditorDebug) Debug.Log("PlayerController.Start() is called.");
    }

    // Update is called once per frame
    void Update()
    {
        if (enableEditorDebug) Debug.Log("PlayerController.Update() is called.");

        // Get the time elapsed since last frame and decrease countdown accordingly.
        float timeElapsed = Time.deltaTime;
        timeLeft -= timeElapsed;
        // Remove ghost from scene if there is no time left in countdown
        if (timeLeft <= 0) { ToggleOff(); }

        // Get the current postion of player-controlled ghost.
        Vector2 currentPosition = transform.position;

        if (blockedX)
        {
            currentSpeedX = 0;
        } else if (blockedY)
        {
            currentSpeedY = 0;
        }

        // Check for inputs.
        bool leftPressed = Input.GetKey(leftKey);
        bool rightPressed = Input.GetKey(rightKey);
        bool upPressed = Input.GetKey(upKey);
        bool downPressed = Input.GetKey(downKey);
        bool togglePressed = Input.GetKey(toggleKey);

        float prevAccel = acceleration;
        // Change acceleration when moving diagonally.
        if ((upPressed || downPressed) && (leftPressed || rightPressed))
        {
            acceleration = acceleration * Mathf.Pow(2, 0.5f);
        }

        if (togglePressed)
        {
            ToggleOff();
        }

        // Process horizontal speed.
        if (leftPressed && !rightPressed && currentSpeedX <= 0)
        {
            currentSpeedX = Mathf.MoveTowards(currentSpeedX, -speed, acceleration * timeElapsed);
        }
        else if (!leftPressed && rightPressed && currentSpeedX >= 0)
        {
            currentSpeedX = Mathf.MoveTowards(currentSpeedX, speed, acceleration * timeElapsed);
        }
        else
        {
            currentSpeedX = Mathf.MoveTowards(currentSpeedX, 0, stopPower * timeElapsed);
        }

        // Process vertical speed.
        if (upPressed && !downPressed && currentSpeedY >= 0)
        {
            currentSpeedY = Mathf.MoveTowards(currentSpeedY, speed, acceleration * timeElapsed);
        }
        else if (!upPressed && downPressed && currentSpeedY <= 0)
        {
            currentSpeedY = Mathf.MoveTowards(currentSpeedY, -speed, acceleration * timeElapsed);
        }
        else
        {
            currentSpeedY = Mathf.MoveTowards(currentSpeedY, 0, stopPower * timeElapsed);
        }

        // Revert acceleration change.
        acceleration = prevAccel;

        rigidbody.velocity = new Vector2(currentSpeedX, currentSpeedY);
        blockedX = false;
        blockedY = false;
        hasCheckedCollision = true;
    }

    void ToggleOn()
    {
        // To Do
    }

    void ToggleOff()
    {
        // To Do
    }
}
