using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class door : MonoBehaviour
{
    #region door_var
    [SerializeField]
    private GameObject Switch;
    private bool open;
    [SerializeField]
    private GameObject d;
    #endregion


    #region Unity_func
    private void Update()
    {
        if (Switch.CompareTag("Timer"))
        {
            open = Switch.GetComponent<Timer>().all_active;
        }
        else
        {
            open = Switch.GetComponent<Button>().activated;
        }

        if (open)
        {
            gameObject.active = true;
        }
        else
        {
            gameObject.active = false;
        }
    }
    #endregion
}
