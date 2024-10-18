using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UIResources : MonoBehaviour
{
    [SerializeField] GameObject buildUI;
    [SerializeField] TextMeshProUGUI cobT, silT, carT;
    [SerializeField] PlayerResources resources;
    private void OnEnable()
    {
        InvasionTImer.OnTimesUp += DisableUI;
    }

    private void OnDisable()
    {
        InvasionTImer.OnTimesUp -= DisableUI;
    }


    void DisableUI()
    {
        buildUI.SetActive(false);
    }

    private void FixedUpdate()
    {
        cobT.text = resources.cobalt.ToString();
        silT.text = resources.silicates.ToString();
        carT.text = resources.carbon.ToString();
    }
}
