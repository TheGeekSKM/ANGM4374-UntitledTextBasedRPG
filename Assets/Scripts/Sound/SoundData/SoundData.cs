using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SoundData", menuName = "Sound/SoundData")]
public class SoundData: ScriptableObject
{
    public AudioClip[] Sounds;
    public AudioClip Sound 
    { 
        get 
        {
            if (Sounds.Length == 1)
            {
                return Sounds[0];
            }
            return Sounds[Random.Range(0, Sounds.Length - 1)];
        } 
    }
    [Range(0, 1)] public float Volume;
    public bool Echo;
}
