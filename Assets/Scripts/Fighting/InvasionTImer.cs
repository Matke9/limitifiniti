using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class InvasionTImer : MonoBehaviour
{
    [Header("In minutes")]
    [SerializeField] float timer = 5;
    [SerializeField] TextMeshProUGUI time;
    int timeInS;

    public static event Action OnTimesUp;
    void Start()
    {
        timeInS = (int)timer * 60;
        time.text = (timeInS / 60) + " : " + (timeInS % 60);
        StartCoroutine(Timer());
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(1);
        --timeInS;


        if (timeInS <=0)
        {
            OnTimesUp?.Invoke();
            StartCoroutine(SurviveTimer());
            time.color = Color.red;
        }
        else
        {
            time.text = (timeInS / 60) + " : " + (timeInS % 60);
            StartCoroutine(Timer());
        }
       
    }


    IEnumerator SurviveTimer()
    {
        yield return new WaitForSeconds(1);
        timeInS++;

        time.text = (timeInS / 60) + " : " + (timeInS % 60);
        StartCoroutine(Timer());

    }

}
