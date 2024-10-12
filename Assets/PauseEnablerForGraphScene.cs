using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseEnablerForGraphScene : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    private bool pause = false;

    private void Start()
    {
        pauseMenu.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pause = !pause;
            pauseMenu.SetActive(pause);
        }
    }

    public void RestartScene(int sceneNum)
    {
        SceneManager.LoadScene(sceneNum);
    }

    public void ExitLevel()
    {
        Application.Quit();
    }

    public void RestartEntireGame()
    {
        SceneManager.LoadScene(0);
    }
}
