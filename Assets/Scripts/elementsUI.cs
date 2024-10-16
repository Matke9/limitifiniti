using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // Add this for UI components

public class elementsUI : MonoBehaviour
{



    public void Quit()
    {
        Application.Quit();
    }

    public void Settings()
    {

        SceneManager.LoadScene("settings");
    }
    public void backButton()
    {

        SceneManager.LoadScene("mainmenu");
    }

}