using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gas : MonoBehaviour
{
    // Reload the scene if the player collides with the gas.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerControl player))
        {
            new PlayerControl().loadSceneItself();
        }
    }
}
