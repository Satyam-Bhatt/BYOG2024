using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ValueChangeSet : MonoBehaviour
{
    [SerializeField] private TMP_Text sliderValue;

    private void Start()
    {
        sliderValue.text = "0";
    }

    public void UpdateSliderValue(float value)
    {
        sliderValue.text = value.ToString("F2");
    }
}
