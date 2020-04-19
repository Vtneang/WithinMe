using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gas : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Shows if this gas flickers or not")]
    private bool m_flicker;
    [SerializeField]
    [Tooltip("How long until the gas flickers on/off")]
    private float m_flickertimer;
    [SerializeField]
    [Tooltip("Gas starts off on/off")]
    private bool m_initial;

    private float m_timer;
    private bool m_on;

    private void Awake() {
        m_timer = m_flickertimer;
        m_on = m_initial;
        GetComponent<SpriteRenderer>().enabled = m_initial;
        GetComponent<BoxCollider2D>().enabled = m_initial;
    }

    private void Update() {
        if (m_flicker) {
            m_timer -= Time.deltaTime;
            if (m_timer <= 0) {
                GetComponent<SpriteRenderer>().enabled = !m_on;
                GetComponent<BoxCollider2D>().enabled = !m_on;
                m_on = !m_on;
                m_timer = m_flickertimer;
            }
        }
    }

    // Reload the scene if the player collides with the gas.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerControl player))
        {
            new PlayerControl().loadSceneItself();
        }
    }
}
