using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct SoundData
{
    public AudioClip Sound;
    [Range(0, 1)] public float Volume;
    public bool Echo;
}
