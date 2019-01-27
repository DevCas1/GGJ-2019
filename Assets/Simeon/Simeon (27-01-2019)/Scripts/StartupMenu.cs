using UnityEngine;
using UnityEngine.SceneManagement;

public class StartupMenu : MonoBehaviour
{
    public Canvas mainCanvas;
    public Canvas optionsCanvas;

    public void Play()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Level1");
    }
    public void Options()
    {
        mainCanvas.gameObject.SetActive(false);
        optionsCanvas.gameObject.SetActive(true);
    }
    public void Back()
    {
        optionsCanvas.gameObject.SetActive(false);
        mainCanvas.gameObject.SetActive(true);
    }
    public void Exit()
    {
        Application.Quit();
    }
}