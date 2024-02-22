using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundAtlas : MonoBehaviour
{
    public static SoundAtlas Instance;

    [Header("Sound References")]
    public AudioClip[] PlayerFootstepSounds;
    public AudioClip[] MonsterGrowlSounds;


    public AudioClip PlayerFootstepSound
    {
        get
        {
            return PlayerFootstepSounds[Random.Range(0, PlayerFootstepSounds.Length)];
        }
    }

    public AudioClip MonsterGrowlSound
    {
        get
        {
            return MonsterGrowlSounds[Random.Range(0, MonsterGrowlSounds.Length)];
        }
    }
 
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

}
