using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] GameObject panelR, panelT, panelB, tutorialP;

    private void Start()
    {
        if(PlayerPrefs.GetInt("Tutorial") == 0)
        {

        }
    }


}
