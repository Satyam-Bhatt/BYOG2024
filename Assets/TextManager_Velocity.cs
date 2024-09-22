using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class TextManager_Velocity : MonoBehaviour
{
    private static TextManager_Velocity _instance;
    public static TextManager_Velocity Instance
    {
        get
        {
            _instance = FindObjectOfType<TextManager_Velocity>();
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<TextManager_Velocity>();
            }

            return _instance;
        }
    }

    public string trajectoryText = "";
    public string velocityText = "";
    public bool velocity = false;

    public bool play = false;

    public GameObject exclamationMark_Traj;
    public GameObject exclamationMark_Vel;
    public GameObject winPanel;
    public GameObject captionPanel;

    [SerializeField] private GameObject trajText;
    [SerializeField] private GameObject velText;

    [SerializeField] private TMP_Text[] textBoxes;

    [SerializeField] private GameObject traj_solutionPanel;
    [SerializeField] private GameObject vel_solutionPanel;

    [SerializeField] private TMP_Text chapterName;
    [SerializeField] private TMP_Text levelName;

    [SerializeField] private AudioClip[] revealButtonClips;


    private void Awake()
    {
        
    }

    private void Start()
    {
        textBoxes = new TMP_Text[trajText.transform.childCount];
        for (int i = 0; i < textBoxes.Length; i++)
        {
            textBoxes[i] = trajText.transform.GetChild(i).GetComponent<TMP_Text>();
        }
        velocity = false;

        traj_solutionPanel.SetActive(false);
        vel_solutionPanel.SetActive(false);
        play = false;
        exclamationMark_Traj.SetActive(false);
        exclamationMark_Vel.SetActive(false);

        chapterName.text = LevelManger.Instance.chapterName;
        levelName.text = LevelManger.Instance.levelName;
        if (GameManager.Instance.restart)
        {
            captionPanel.SetActive(false);
        }
    }

    private void OnEnable()
    {
        winPanel.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(NextLevel);
        winPanel.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(QuitGame);
        winPanel.transform.GetChild(3).GetComponent<Button>().onClick.AddListener(RestartScene);
    }

    private void CombineText()
    {
        if (!velocity)
        {
            trajectoryText = "";
        }
        else
        {
            velocityText = "";
        }

        if (textBoxes[0].text == "-" || textBoxes[0].text == "+")
        {
            textBoxes[0].text = "0 " + textBoxes[0].text;
        }
        foreach (TMP_Text text in textBoxes)
        {
            if (!velocity)
            {
                trajectoryText += text.text;
            }
            else {
                velocityText += text.text;
            }
        }
        if (textBoxes[0].text == "0 -" || textBoxes[0].text == "0 +")
        {
            textBoxes[0].text = textBoxes[0].text.ToCharArray()[2].ToString();
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
        if (velocity)
        {
            velocityText = "";
        }
        else {
            trajectoryText = "";
        
        }
        foreach (TMP_Text text in textBoxes)
        {
            text.text = "";
        }
        CombineText();
    }

    public void SolutionReveal()
    {
        traj_solutionPanel.SetActive(true);
        vel_solutionPanel.SetActive(true);

        traj_solutionPanel.transform.GetChild(3).GetComponent<TMP_Text>().text = LevelManger.Instance.trajSolution;
        vel_solutionPanel.transform.GetChild(3).GetComponent<TMP_Text>().text = LevelManger.Instance.velSolution;

        trajectoryText = LevelManger.Instance.trajSolution;
        velocityText = LevelManger.Instance.velSolution;

        GetComponent<AudioSource>().Stop();
        captionPanel.SetActive(false);
        LevelManger.Instance.audioSource.Stop();
        int randomClip = Random.Range(0, revealButtonClips.Length);
        float delay = revealButtonClips[randomClip].length;
        GameManager.Instance.gameObject.GetComponent<AudioSource>().volume = 0.025f;
        GetComponent<AudioSource>().PlayOneShot(revealButtonClips[randomClip]);
        StartCoroutine(CloseCaption(delay));
    }

    IEnumerator CloseCaption(float delay)
    {
        yield return new WaitForSeconds(delay);
        /*        if (TextManager.Instance != null)
                {
                    TextManager.Instance.captionPanel.SetActive(false);
                }
                else if (TextManager_Velocity.Instance != null)
                {
                    TextManager_Velocity.Instance.captionPanel.SetActive(false);
                }*/
        GameManager.Instance.gameObject.GetComponent<AudioSource>().volume = 0.2f;
    }

    public void TrajectoryClicked()
    {
        for (int i = 0; i < textBoxes.Length; i++)
        {
            textBoxes[i] = trajText.transform.GetChild(i).GetComponent<TMP_Text>();
        }
        velocity = false;
    }

    public void VelocityClicked()
    {
        for (int i = 0; i < textBoxes.Length; i++)
        {
            textBoxes[i] = velText.transform.GetChild(i).GetComponent<TMP_Text>();
        }
        velocity = true;
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
}
