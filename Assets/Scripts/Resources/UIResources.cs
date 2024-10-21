using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UIResources : MonoBehaviour
{
    [SerializeField] GameObject buildUI;
    [SerializeField] GameObject gridShader;
    [SerializeField] TextMeshProUGUI cobT, silT, carT;
    [SerializeField] PlayerResources resources;
    private void OnEnable()
    {
        InvasionTimer.OnTimesUp += DisableUI;
    }

    private void OnDisable()
    {
        InvasionTimer.OnTimesUp -= DisableUI;
    }


    void DisableUI()
    {
        buildUI.SetActive(false);
        gridShader.SetActive(false);
    }

    private void FixedUpdate()
    {
        cobT.text = resources.cobalt.ToString();
        silT.text = resources.silicates.ToString();
        carT.text = resources.carbon.ToString();
    }
}
