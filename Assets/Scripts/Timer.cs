using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    #region timer_var
    [SerializeField]
    [Tooltip("holds all the buttons needed")]
    private Button[] buttons;
    private bool started;
    [SerializeField]
    [Tooltip("amount of time wanted for the timer")]
    private float end_time;
    public bool all_active;
    private Coroutine C;
    public bool istimer = true;
    public GameObject device;
    [SerializeField]
    [Tooltip("if true there is a time limit to how long it is open")]
    private bool alltimed;
    #endregion


    #region unity_func
    // Update is called once per frame
    void Update()
    {
        int count = 0;
        foreach(Button b in buttons)
        {
            if (b.activated)
            {
                count += 1;
            }
        }
        if(count == buttons.Length)
        {
            if (!alltimed)
            {
                StopCoroutine(C);
            }
            all_active = true;
            device.active = true;
        }

        
    }

    #endregion


    #region timer_funcs

    public void start_timer()
    {
        if (!started)
        {
            started = true;
            C = StartCoroutine(begin());
        }

    }

    IEnumerator begin()
    {
        float totaltime = 0;
        while(totaltime < end_time)
        {
            totaltime += Time.deltaTime;
            yield return null;
        }
        foreach(Button b in buttons)
        {
            b.turn_off();
        }
        all_active = false;
        started = false;
    }
    #endregion
}
