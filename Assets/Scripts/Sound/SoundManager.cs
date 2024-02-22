using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    [SerializeField] GameObject SoundPrefab;

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

    public void Sound(Transform soundSource, SoundData soundData)
    {
        GameObject sound = Instantiate(SoundPrefab, soundSource.position, Quaternion.identity);
        sound.GetComponent<Sound>().SetSound(soundData);
    }
}
