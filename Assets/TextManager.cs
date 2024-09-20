using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

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

    [SerializeField] private TMP_Text[] textBoxes;

    private void Update()
    {
    }
    private void CombineText()
    {
        combinedText = "";
        foreach (TMP_Text text in textBoxes)
        {
            combinedText += text.text;
        }
    }

    public void ModButton()
    {
        foreach (TMP_Text text in textBoxes)
        { 
            if(text.text == "")
            {
                text.text = "x%2";
                break;
            }
        }
        CombineText();
    }

    public void XButton() 
    {
        foreach (TMP_Text text in textBoxes)
        {
            if (text.text == "")
            {
                text.text = "x";
                break;
            }
        }
        CombineText();
    }

    public void PlusButton()
    {
        foreach (TMP_Text text in textBoxes)
        {
            if (text.text == "")
            {
                text.text = "+";
                break;
            }
        }
        CombineText();
    }

    public void SinButton()
    {
        foreach (TMP_Text text in textBoxes)
        {
            if (text.text == "")
            {
                text.text = "sin(x)";
                break;
            }
        }
        CombineText();
    }

    public void OneButton()
    {
        foreach (TMP_Text text in textBoxes)
        {
            if (text.text == "")
            {
                text.text = "1";
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

}
