using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moveable : MonoBehaviour
{
    private bool moveable, linked, ghostControl, possessed;
    private SpringJoint2D springJoint;
    private Collider2D controller;

    [Header("Key Setting")]
    public KeyCode interactKey = KeyCode.F;

    // Start is called before the first frame update
    void Start()
    {
        // Create an inactive SpringJoint for simulating Pulling and Pushing.
        springJoint = gameObject.AddComponent<SpringJoint2D>();
        springJoint.frequency = 5;
        springJoint.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (controller != null && controller.CompareTag("Ghost"))
        {
            ghostControl = controller.gameObject.activeSelf;
        }

        if (moveable || linked)
        {
            if (Input.GetKeyDown(interactKey))
            {
                EnableJoint();

                // Simulate Ghost Possession. Turn ghost object invisible during object movement.
                if (!possessed && ghostControl)
                {
                    possessed = true;
                    controller.GetComponent<SpriteRenderer>().enabled = false;
                } 
                else if (possessed)
                {
                    DisableJoint();

                    possessed = false;
                    controller.GetComponent<SpriteRenderer>().enabled = true;
                }
            }

            if (Input.GetKeyUp(interactKey) && !ghostControl || possessed && !ghostControl)
            {
                DisableJoint();

                // Return ghost visibility.
                possessed = false;
                controller.GetComponent<SpriteRenderer>().enabled = true;
            }
        }
    }

    // Enable Pulling and Pushing via SpringJoint.
    private void EnableJoint()
    {
        springJoint.enabled = true;
        springJoint.connectedBody = controller.GetComponent<Rigidbody2D>();
        linked = true;

        Debug.Log("Pushing/Pulling");
    }

    // Disable Pulling and Pushing by Player
    private void DisableJoint()
    {
        springJoint.connectedBody = null;
        springJoint.enabled = false;

        Debug.Log("Released");
    }

    // OnTriggerEnter2D is called when the Collider2D other enters the trigger (2D physics only)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Test if the collider object is the player control. If yes, allow interaction.
        if (collision.CompareTag("Player"))
        {
            moveable = true;
            controller = collision;

            Debug.Log("Object Moveable by Player");
        }

        // Test if the collider object is the ghost control. If yes, allow interaction.
        if (collision.CompareTag("Ghost"))
        {
            moveable = true;
            controller = collision;

            Debug.Log("Object Moveable by Ghost");
        }
    }

    // OnTriggerExit2D is called when the Collider2D other has stopped touching the trigger (2D physics only)
    private void OnTriggerExit2D(Collider2D collision)
    {
        moveable = false;

        Debug.Log("Object No Longer Moveable");
    }
}
