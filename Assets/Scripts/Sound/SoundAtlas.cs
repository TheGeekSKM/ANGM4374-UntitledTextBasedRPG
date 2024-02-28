using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundAtlas : MonoBehaviour
{
    public static SoundAtlas Instance;

    [Header("Sound References")]
    public SoundData[] PlayerFootstepSounds;
    public SoundData[] PlayerTurnSounds;
    public SoundData[] MonsterFootstepSounds;
    public SoundData[] MonsterGrowlSounds;


    public SoundData PlayerFootstepSound
    {
        get
        {
            return PlayerFootstepSounds[Random.Range(0, PlayerFootstepSounds.Length)];
        }
    }

    public SoundData PlayerTurnSound
    {
        get
        {
            return PlayerTurnSounds[Random.Range(0, PlayerTurnSounds.Length)];
        }
    }

    public SoundData MonsterFootstepSound
    {
        get
        {
            return MonsterFootstepSounds[Random.Range(0, MonsterFootstepSounds.Length)];
        }
    }

    public SoundData MonsterGrowlSound
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
