using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Copy : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI source;
    TextMeshProUGUI target;
    void OnEnable()
    {
        target = GetComponent<TextMeshProUGUI>();
        target.text = source.text;
    }
}
