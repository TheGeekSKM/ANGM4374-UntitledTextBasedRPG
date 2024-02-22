using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SoundData", menuName = "Sound/SoundData")]
public class SoundData: ScriptableObject
{
    public AudioClip Sound;
    [Range(0, 1)] public float Volume;
    public bool Echo;
}
