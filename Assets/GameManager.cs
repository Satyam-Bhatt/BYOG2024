using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject winPanel;

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance)
        {
            Destroy(this);
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(this);
    }

    public void RestartScene()
    {
        int sceneNum = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(sceneNum);
    }

    private void Start()
    {
        winPanel.SetActive(false);
    }

    public void NextLevel()
    {
        winPanel.SetActive(false);
        int sceneNum = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(sceneNum + 1);
    }

    public void ExitLevel()
    {
        Application.Quit();
    }

}
