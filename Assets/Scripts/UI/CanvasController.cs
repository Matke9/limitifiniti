using UnityEngine.SceneManagement;
using UnityEngine;

public class CanvasController : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
