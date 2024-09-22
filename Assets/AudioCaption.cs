using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AudioCaption
{
    public AudioClip audioClip;
    [TextArea(3, 10)]
    public string caption;
}
