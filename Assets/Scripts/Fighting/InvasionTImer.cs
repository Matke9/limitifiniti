using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class InvasionTimer : MonoBehaviour
{
    [Header("In minutes")]
    [SerializeField] float timer = 5;
    [SerializeField] TextMeshProUGUI time;
    int timeInS;

    public static event Action OnTimesUp;
    void Start()
    {
        timeInS = (int)timer * 60;
        string zeroBefore = (timer < 10 ? "0" : "") + (timer < 1 ? "0" : ""); //mozda ce moci da se menja tajmer u zavisnosti od toga koliko bi da igras
        string zeroAfter = timeInS % 60 < 10 ? "0" : ""; //ako je jednocifreno da ima nulu
        time.text = zeroBefore + (timeInS / 60) + " : " + zeroAfter + (timeInS % 60);
        StartCoroutine(Timer());
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(1);
        --timeInS;


        if (timeInS <= 0)
        {
            OnTimesUp?.Invoke();
            StartCoroutine(SurviveTimer());
            time.color = Color.red;
        }
        else
        {
            string zeroBefore = (timer < 10 ? "0" : "") + (timer < 1 ? "0" : "");
            string zeroAfter = timeInS % 60 < 10 ? "0" : "";
            time.text = zeroBefore + (timeInS / 60) + " : " + zeroAfter + (timeInS % 60);
            StartCoroutine(Timer());
        }

    }


    IEnumerator SurviveTimer()
    {
        yield return new WaitForSeconds(1);
        timeInS++;

        string zeroBefore = (timer < 10 ? "0" : "") + (timer < 1 ? "0" : "");
        string zeroAfter = timeInS % 60 < 10 ? "0" : "";
        time.text = zeroBefore + (timeInS / 60) + " : " + zeroAfter + (timeInS % 60);
        StartCoroutine(SurviveTimer());

    }

}
