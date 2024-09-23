using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class TitleSceneManager : MonoBehaviour
{
    public GameObject captionPanel;

    [TextArea(3,10)]
    public string dialogue;

    private AudioSource audioSource;

    [SerializeField] private PlayableDirector playableDirector;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        playableDirector.stopped += TimelineStop;
    }

    private void OnDisable()
    {
        playableDirector.stopped -= TimelineStop;
    }

    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(CharacterDialogue(dialogue));
        //StartCoroutine(AudioEnd(audioSource.clip.length + 2f));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartCaptions()
    {
        StartCoroutine(CharacterDialogue(dialogue));
        //StartCoroutine(AudioEnd(audioSource.clip.length + 2f));
    }

    IEnumerator CharacterDialogue(string dialogue)
    {
        captionPanel.transform.GetChild(0).GetComponent<TMP_Text>().text = "";
        foreach (char letter in dialogue.ToCharArray())
        {
            captionPanel.transform.GetChild(0).GetComponent<TMP_Text>().text += letter;
            yield return new WaitForSeconds(0.02f);
        }
    }

    IEnumerator AudioEnd(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(1);
    }

    public void Skip()
    {
        SceneManager.LoadScene(1);
    }

    public void TimelineStop(PlayableDirector obj)
    {
        Skip();
    }
}
