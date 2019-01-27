using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Canvas playCanvas;
    public Canvas pauseCanvas;

    void Update()
    {
        if (playCanvas.gameObject.activeInHierarchy)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) Play();
        }

        if (Input.GetKeyDown(KeyCode.Escape) && !pauseCanvas.gameObject.activeInHierarchy)
        {
            pauseCanvas.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && pauseCanvas.gameObject.activeInHierarchy)
        {
            pauseCanvas.gameObject.SetActive(false);
            Time.timeScale = 1;
        }

        if (playCanvas.gameObject.activeInHierarchy && pauseCanvas.gameObject.activeInHierarchy) pauseCanvas.gameObject.SetActive(false);
    }

    public void Play()
    {
        Time.timeScale = 1;
        playCanvas.gameObject.SetActive(false);
    }
    public void Retry()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Level1");
    }   
    public void Exit()
    {
        Application.Quit();
    }
}
