using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moveable : MonoBehaviour
{
    private bool interactable, linked;
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
        if (interactable || linked)
        {
            if (Input.GetKeyDown("f"))
            {
                // Enable Pulling and Pushing via SpringJoint.
                springJoint.enabled = true;
                springJoint.connectedBody = player.GetComponent<Rigidbody2D>();
                linked = true;

                Debug.Log("Linked");
            }
            if (Input.GetKeyUp("f"))
            {
                // Disable Pulling and Pushing.
                springJoint.connectedBody = null;
                springJoint.enabled = false;

                Debug.Log("Unlinked");
            }
        }
    }

    // OnTriggerEnter2D is called when the Collider2D other enters the trigger (2D physics only)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Test if the collider object is the player controll. If yes, allow interaction.
        if (collision.CompareTag("Player"))
        {
            interactable = true;
            player = collision;

            Debug.Log("Interactable");
        }
    }

    // OnTriggerExit2D is called when the Collider2D other has stopped touching the trigger (2D physics only)
    private void OnTriggerExit2D(Collider2D collision)
    {
        interactable = false;

        Debug.Log("Not Interactable");
    }
}
