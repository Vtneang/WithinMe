using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vent : MonoBehaviour
{
    private bool disabled, interactable;

    [Header("Gas Emission Setting")]
    public GameObject gasField;

    [Header("Key Setting")]
    public KeyCode interactKey = KeyCode.F;

    // Update is called once per frame
    void Update()
    {
        if (interactable && Input.GetKeyDown(interactKey))
        {
            disabled = !disabled;

            if (disabled)
            {
                gasField.SetActive(false);

                Debug.Log("Vent closed");

            } else
            {
                gasField.SetActive(true);

                Debug.Log("Vent opened");
            }
        }
    }

    // OnTriggerEnter2D is called when the Collider2D other enters the trigger (2D physics only)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Test if the collider object is the player controll. If yes, allow interaction.
        if (collision.CompareTag("Player") || collision.CompareTag("Ghost"))
        {
            interactable = true;

            Debug.Log("Object Interactable");
        }
    }

    // OnTriggerExit2D is called when the Collider2D other has stopped touching the trigger (2D physics only)
    private void OnTriggerExit2D(Collider2D collision)
    {
        interactable = false;

        Debug.Log("Object No Longer Interactable");
    }
}
