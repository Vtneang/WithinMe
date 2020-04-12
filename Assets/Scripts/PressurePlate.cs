using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    #region plate_vars
    private List<GameObject> on_top;
    private bool activated;
    private int amount;
    [SerializeField]
    private GameObject device;
    #endregion

    #region plate_vars

    private void OnCollisionEnter2D(Collision2D collision)
    {
        on_top.Add(collision.gameObject);
        amount += 1;
        activated = true;
        device.active = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        on_top.Remove(collision.gameObject);
        amount -= 1;
        if(amount == 0)
        {
            activated = false;
        }
    }

    #endregion
}
