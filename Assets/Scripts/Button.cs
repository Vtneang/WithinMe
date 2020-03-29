using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{

    #region button_vars
    public bool active;
    [SerializeField]
    [Tooltip("shows whether there is a timer or not")] 
    private bool timed;
    private GameObject door;
    private Timer clock;
    #endregion


    #region button_func
    public void interact()
    {
        if (timed)
        {
            clock.start_timer();
        }
        else
        {
            active = !active;
            /* test*/
        }
    }

    public void turn_off()
    {
        active = false;
    }
    #endregion
}
