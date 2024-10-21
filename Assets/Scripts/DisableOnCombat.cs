using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableOnCombat : MonoBehaviour
{
    private void OnEnable()
    {
        InvasionTimer.OnTimesUp += StartCombat;
    }

    private void OnDisable()
    {
        InvasionTimer.OnTimesUp -= StartCombat;
    }

    void StartCombat()
    {
        Destroy(gameObject);
    }
}
