using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TextManager : MonoBehaviour
{
    private static TextManager _instance;
    public static TextManager Instance
    {
        get
        {
            _instance = FindObjectOfType<TextManager>();
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<TextManager>();
            }

            return _instance;
        }
    }

    public string combinedText = "";
    public bool play = false;

    public GameObject exclamationMark;
    public GameObject winPanel;

    [SerializeField] private TMP_Text[] textBoxes;
    [SerializeField] private GameObject solutionPanel;
    [SerializeField] private TMP_Text chapterName;
    [SerializeField] private TMP_Text levelName;
    public GameObject endScreen;

    private void Awake()
    {
    }

    private void Start()
    {
        solutionPanel.SetActive(false);
        play = false;
        exclamationMark.SetActive(false);
        endScreen.SetActive(false);

        chapterName.text = LevelManger.Instance.chapterName;
        levelName.text = LevelManger.Instance.levelName;
    }

    private void OnEnable()
    {
        winPanel.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(NextLevel);
        winPanel.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(QuitGame);
    }

    private void CombineText()
    {
        combinedText = "";
        if (textBoxes[0].text == "-" || textBoxes[0].text == "+")
        {
            textBoxes[0].text = "0 " + textBoxes[0].text;
        }
        foreach (TMP_Text text in textBoxes)
        {
            combinedText += text.text;
        }

    }
    public void CommonButton(string str)
    {
        foreach (TMP_Text text in textBoxes)
        {
            if (text.text == "")
            {
                text.text = str;
                break;
            }
        }
        CombineText();
    }
    public void BackSpace()
    {
        for (int i = textBoxes.Length - 1; i >= 0; i--)
        {
            if (textBoxes[i].text != "")
            {
                textBoxes[i].text = "";
                break;
            }
        }
        CombineText();
    }

    public void Clear_All()
    {
        combinedText = "";
        foreach (TMP_Text text in textBoxes)
        {
            text.text = "";
        }
        CombineText();
    }

    public void SolutionReveal()
    {
        solutionPanel.SetActive(true);
        GameObject g =GameObject.Find("SolutionMain");
        g.GetComponent<TMP_Text>().text = LevelManger.Instance.trajectory_OnlySolution;
        combinedText = LevelManger.Instance.trajectory_OnlySolution;
    }

    public void Play()
    {
        play = true;
    }

    public void RestartScene()
    {
        GameManager.Instance.RestartScene();
    }

    public void NextLevel()
    {
        GameManager.Instance.NextLevel();
    }
    public void QuitGame()
    {
        GameManager.Instance.ExitLevel();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

}