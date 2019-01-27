using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Canvas pauseCanvas;
    public Canvas youLoseCanvas;
    public Canvas optionsCanvas;

    void Start()
    {
        Time.timeScale = 1;
    }

    void Update()
    {
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
    }

    public void YouWin()
    {
        SceneManager.LoadScene("WinGameScene");
    }
    public void YouLose()
    {
        Time.timeScale = 0;
        if (youLoseCanvas) youLoseCanvas.gameObject.SetActive(true);
    }

    public void Options()
    {
        pauseCanvas.gameObject.SetActive(false);
        optionsCanvas.gameObject.SetActive(true);
    }
    public void Back()
    {
        optionsCanvas.gameObject.SetActive(false);
        pauseCanvas.gameObject.SetActive(true);
    }
    public void Retry()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Level1");
    }
    public void Exit()
    {
        SceneManager.LoadScene("StartupMenu");
    }
}