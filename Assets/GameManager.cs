using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject winPanel;
    public bool restart = false;

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

    private void OnEnable()
    {
        SceneManager.activeSceneChanged += NewScene;
    }
    private void OnDisable()
    {
        SceneManager.activeSceneChanged -= NewScene;
    }

    private void Update()
    {
        Debug.Log("Restart from update: " + restart);
    }

    public void RestartScene()
    {
        restart = true;
        Debug.Log("Restart from game manager: " + restart);
        int sceneNum = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(sceneNum);

    }

    private void Start()
    {
        winPanel.SetActive(false);
    }

    public void NextLevel()
    {
        if (winPanel != null)
        {
            winPanel.SetActive(false);
        }
        else {
            winPanel = TextManager.Instance.winPanel;
        }
        int sceneNum = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(sceneNum + 1);
        restart = false;
    }

    public void ExitLevel()
    {
        Application.Quit();
    }

    public void NewScene(Scene scene, Scene scene2)
    {
        //Debug.Log("Call");
        if (TextManager.Instance != null && TextManager.Instance.winPanel != null)
        {
            winPanel = TextManager.Instance.winPanel;
        }
        else if (TextManager_Velocity.Instance != null && TextManager_Velocity.Instance.winPanel != null)
        { 
            winPanel = TextManager_Velocity.Instance.winPanel;
        }
        
    }

    

}
