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

    [SerializeField]
    [Tooltip("Shows is button only toggles the door vs open/close")]
    private bool m_toggler;

    [SerializeField]
    [Tooltip("Shows is this is a pressure plate or not")]
    private bool m_pressure;
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
            if (m_toggler) {
                m_device.toggle();
                return;
            }
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
        if (m_pressure) {
            if (collision.CompareTag("Player") || collision.CompareTag("Crate")) {
                m_Activated = true;
                m_device.checkDevice();
            }
        }
        else {
            // Test if the collider object is the player controll. If yes, allow interaction.
            if (collision.CompareTag("Player") || collision.CompareTag("Ghost")) {
                interactable = true;
            }
        }

    }

    // OnTriggerExit2D is called when the Collider2D other has stopped touching the trigger (2D physics only)
    private void OnTriggerExit2D(Collider2D collision) {
        if (m_pressure) {
            if (collision.CompareTag("Player") || collision.CompareTag("Crate")) {
                m_Activated = false;
                m_device.checkDevice();
            }
        }
        else {
            interactable = false;
        }
    }
}
