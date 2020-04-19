using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Device : MonoBehaviour
{
    #region Variables
    [SerializeField]
    private GameObject[] m_beingActivated;
    [SerializeField]
    [Tooltip("Shows if this device is a timed or not timed event")]
    private bool m_timed;
    [SerializeField]
    [Tooltip("Shows if this device is opened for a certain amount of time or if a certain amount of actions must be performed within a period of time")]
    private bool m_timedOpened;
    [SerializeField]
    [Tooltip("How long the timed event is")]
    private float m_timedDuration;
    [SerializeField]
    [Tooltip("Shows if once triggered, can or can not be triggered again. NOTE: DOESN'T APPLY WITH m_timedOpened = True BUT SHOULD BE PAIRED WITH m_timedOpened = False")]
    private bool m_activateonce;
    [SerializeField]
    [Tooltip("All the buttons")]
    private Button[] m_buttons;

    private bool[] m_initialActive;
    private bool m_activated;

    private bool m_timeactivated;
    private float m_timedtimer;
    #endregion

    private void Awake() {
        m_initialActive = new bool[m_beingActivated.Length];
        m_activated = false;
        m_timeactivated = false;

        for (int i = 0; i < m_beingActivated.Length; i++) {
            m_initialActive[i] = m_beingActivated[i].active;
        }
    }

    void Update() {
        if (m_activated == true && m_activateonce) {
            return;
        }
        if (m_timeactivated) {
            m_timedtimer -= Time.deltaTime;
            if (m_timedtimer <= 0) {
                m_timeactivated = false;
                for (int i = 0; i < m_beingActivated.Length; i++) {
                    m_beingActivated[i].SetActive(m_beingActivated[i]);
                }
                foreach (Button button in m_buttons) {
                    button.turnOff();
                }
            }
        }
    }

    public void checkDevice() {
        if (m_activated == true && m_activateonce) {
            return;
        }
        if (m_timed && !m_timedOpened && !m_timeactivated) {
            m_timedtimer = m_timedDuration;
            m_timeactivated = true;
        }
        foreach (Button button in m_buttons) {
            Debug.Log($"{button.getActivated()} for {button.gameObject.name}");
            if (button.getActivated() == false) {
                for (int i = 0; i < m_beingActivated.Length; i++) {
                    m_beingActivated[i].SetActive(m_beingActivated[i]);
                }
                return;
            }
        }
        for (int i = 0; i < m_beingActivated.Length; i++) {
            m_beingActivated[i].SetActive(!m_beingActivated[i]);
            if (m_timed && m_timedOpened) {
                m_timedtimer = m_timedDuration;
                m_timeactivated = true;
            }
            else {
                m_activated = true;
            }
        }
    }

    public void toggle() {
        for (int i = 0; i < m_beingActivated.Length; i++) {
            m_beingActivated[i].SetActive(!m_beingActivated[i].active);
        }
    }
}
