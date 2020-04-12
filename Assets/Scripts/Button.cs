using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{

    #region button_vars
    public bool activated;
    [SerializeField]
    [Tooltip("shows whether there is a timer or not")] 
    private bool timed;
    private Timer clock;
    public GameObject device;
    #endregion
    
    #region button_func
    public void interact()
    {
        if (timed)
        {
            activated = true;
            clock.start_timer();
        }
        else
        {
            activated = !activated;
            if (device != null) { 
            device.active = activated;
            }
            /* test*/
        }
    }

    public void turn_off()
    {
        activated = false;
    }
    #endregion
}
