using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fadeScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator animator;
    public Button setting, back;
    // Update is called once per frame
    void Update()
    {
        if(setting.onClick != null)
        {
            FadeTo(1);
        }
    }

    public void FadeTo(int levelIndex)
    {
        animator.SetTrigger("FadeOut");
    }
}
