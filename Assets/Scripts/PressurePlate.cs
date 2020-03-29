using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    #region plate_vars
    private List<GameObject> on_top;
    private bool active;
    private int amount;
    #endregion

    #region plate_vars

    private void OnCollisionEnter2D(Collision2D collision)
    {
        on_top.Add(collision.gameObject);
        amount += 1;
        active = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        on_top.Remove(collision.gameObject);
        amount -= 1;
        if(amount == 0)
        {
            active = false;
        }
    }

    #endregion
}
