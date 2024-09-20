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
        if (textBoxes[0].text=="-"|| textBoxes[0].text == "+")
        {
            textBoxes[0].text ="0"+textBoxes[0].text ;
        }
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

    public void SquareButton()
    {
        foreach (TMP_Text text in textBoxes)
        {
            if (text.text == "")
            {
                text.text = "^2";
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
                text.text = "sin(";
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
    public void TwoButton()
    {
        foreach (TMP_Text text in textBoxes)
        {
            if (text.text == "")
            {
                text.text = "2";
                break;
            }
        }
        CombineText();
    }
    public void ThreeButton()
    {
        foreach (TMP_Text text in textBoxes)
        {
            if (text.text == "")
            {
                text.text = "3";
                break;
            }
        }
        CombineText();
    }
    public void FourButton()
    {
        foreach (TMP_Text text in textBoxes)
        {
            if (text.text == "")
            {
                text.text = "4";
                break;
            }
        }
        CombineText();
    }
    public void FiveButton()
    {
        foreach (TMP_Text text in textBoxes)
        {
            if (text.text == "")
            {
                text.text = "5";
                break;
            }
        }
        CombineText();
    }
    public void SixButton()
    {
        foreach (TMP_Text text in textBoxes)
        {
            if (text.text == "")
            {
                text.text = "6";
                break;
            }
        }
        CombineText();
    }
    public void SevenButton()
    {
        foreach (TMP_Text text in textBoxes)
        {
            if (text.text == "")
            {
                text.text = "7";
                break;
            }
        }
        CombineText();
    }
    public void EightButton()
    {
        foreach (TMP_Text text in textBoxes)
        {
            if (text.text == "")
            {
                text.text = "8";
                break;
            }
        }
        CombineText();
    }
    public void NineButton()
    {
        foreach (TMP_Text text in textBoxes)
        {
            if (text.text == "")
            {
                text.text = "9";
                break;
            }
        }
        CombineText();
    }

    public void MinusButton()
    {
        foreach (TMP_Text text in textBoxes)
        {
            if (text.text == "")
            {
                text.text = "-";
                break;
            }
        }
        CombineText();
    }
    public void DivideButton()
    {
        foreach (TMP_Text text in textBoxes)
        {
            if (text.text == "")
            {
                text.text = "/";
                break;
            }
        }
        CombineText();
    }
    public void MultiplyButton()
    {
        foreach (TMP_Text text in textBoxes)
        {
            if (text.text == "")
            {
                text.text = "*";
                break;
            }
        }
        CombineText();
    }
    public void ZeroButton()
    {
        foreach (TMP_Text text in textBoxes)
        {
            if (text.text == "")
            {
                text.text = "0";
                break;
            }
        }
        CombineText();
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

}
