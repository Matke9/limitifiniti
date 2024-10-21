using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] GameObject t1, t2;

    public void Next()
    {
        t1.SetActive(false);
        t2.SetActive(true);
    }

    public void Back()
    {
        t1.SetActive(true);
        t2.SetActive(false);
    }

}
