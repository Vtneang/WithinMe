using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{

    #region Variables
    private bool m_Activated;

    [SerializeField]
    [Tooltip("Device")] 
    private Device m_device;
    #endregion

    private bool opened, interactable;

    [Header("Key Setting")]
    public KeyCode interactKey = KeyCode.F;

    void Awake() {
        m_Activated = false;
    }

    // Update is called once per frame
    void Update() {
        if (interactable && Input.GetKeyDown(interactKey)) {
            m_Activated = !m_Activated;
            m_device.checkDevice();
        }
    }

    public bool getActivated() {
        return m_Activated;
    }

    public void turnOff() {
        m_Activated = false;
    }

    // OnTriggerEnter2D is called when the Collider2D other enters the trigger (2D physics only)
    private void OnTriggerEnter2D(Collider2D collision) {
        // Test if the collider object is the player controll. If yes, allow interaction.
        if (collision.CompareTag("Player") || collision.CompareTag("Ghost")) {
            interactable = true;
        }
    }

    // OnTriggerExit2D is called when the Collider2D other has stopped touching the trigger (2D physics only)
    private void OnTriggerExit2D(Collider2D collision) {
        interactable = false;
    }
}
