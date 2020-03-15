﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour
{

    //Public stuff
    public float speedx = 2;
    public float speedy = 5;
    public Rigidbody2D rigy;
    public SpriteRenderer sprite;
    public Color Changer;
    public Color Cur;
    public float ChangerTime;
    public float gravity = 5;
    [Range(0f, 5f)]
    public float bot = .5f;

    //Private vars
    private float SpeedX;
    private float SpeedY;
    private bool onGround;

    // Update is called once per frame
    void Update()
    {

        float elapsed = Time.deltaTime;

        if (onGround)
        {

            if (Input.GetKey(KeyCode.D))
            {
                SpeedX = speedx;
            }
            else if (Input.GetKey(KeyCode.A))
            {
                SpeedX = -speedx;
            }
            else
            {
                SpeedX = 0;
            }
            if (Input.GetKey(KeyCode.Space))
            {
                SpeedY += speedy;
                onGround = false;
            }
        }
        else if (!onGround)
        {
            SpeedY -= gravity * elapsed;
            if (SpeedX <= 0f && Input.GetKey(KeyCode.D))
            {
                SpeedX += speedx * .01f;
            }
            else if (SpeedX > 0f && Input.GetKey(KeyCode.A))
            {
                SpeedX -= speedx * .01f;
            }
        }
        rigy.velocity = new Vector2(SpeedX, SpeedY);

        // Reloads the scene when pressing R.
        if (Input.GetKey(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

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
        }
    }

    private IEnumerator ChangeColor(Color c, float i)
    {
        while (sprite.color != c)
        {
            sprite.color = Color.Lerp(sprite.color, c, i / 100);
            yield return null;
        }
    }

    // When on the ground
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.position.y <= transform.position.y - bot)
        {
            onGround = true;
        }
        SpeedY = 0;
    }

}