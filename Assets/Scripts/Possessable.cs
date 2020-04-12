using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Possessable : MonoBehaviour
{
    private bool moveable, linked;
    private SpringJoint2D springJoint;
    private Collider2D player;

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
        if (moveable || linked)
        {
            if (Input.GetKeyDown("f"))
            {
                // Enable Pulling and Pushing via SpringJoint.
                springJoint.enabled = true;
                springJoint.connectedBody = player.GetComponent<Rigidbody2D>();
                linked = true;

                Debug.Log("Pushing/Pulling");
            }
            if (Input.GetKeyUp("f"))
            {
                // Disable Pulling and Pushing.
                springJoint.connectedBody = null;
                springJoint.enabled = false;

                Debug.Log("Released");
            }
        }
    }

    // OnTriggerEnter2D is called when the Collider2D other enters the trigger (2D physics only)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Test if the collider object is the player control. If yes, allow interaction.
        if (collision.CompareTag("Player"))
        {
            moveable = true;
            player = collision;

            Debug.Log("Object Moveable");
        }
    }

    // OnTriggerExit2D is called when the Collider2D other has stopped touching the trigger (2D physics only)
    private void OnTriggerExit2D(Collider2D collision)
    {
        moveable = false;

        Debug.Log("Object No Longer Moveable");
    }
}
