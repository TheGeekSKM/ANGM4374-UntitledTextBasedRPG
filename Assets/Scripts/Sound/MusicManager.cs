using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] musicClips;
    [SerializeField] AudioSource audioSource;
    int _currentIndex = 0;

    void OnValidate()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    void Start()
    {
        if (musicClips.Length == 0)
        {
            Debug.LogError("No music clips assigned to MusicManager");
            return;
        }
        _currentIndex = Random.Range(0, musicClips.Length);
        audioSource.clip = musicClips[_currentIndex];
        audioSource.Play();
    }

    void Update()
    {
        if (musicClips.Length == 0)
        {
            return;
        }
        if (!audioSource.isPlaying)
        {
            _currentIndex = (_currentIndex + 1) % musicClips.Length;

            audioSource.clip = musicClips[_currentIndex];
            audioSource.Play();
        }
    }
}
