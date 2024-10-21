using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Die : MonoBehaviour
{
    [SerializeField] GameObject EndScreen;
    public void Death()
    {
        EndScreen.SetActive(true);
    }
}
