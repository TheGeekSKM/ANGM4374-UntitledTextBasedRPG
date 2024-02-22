using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    [SerializeField] SoundData _soundData;
    [SerializeField] AudioSource _audioSource;
    [SerializeField] float _soundTriggerGrowthSpeed = 0.1f;

    public void SetSound(SoundData soundData)
    {
        _soundData = soundData;
        _audioSource.clip = _soundData.Sound;
        _audioSource.volume = _soundData.Volume;
        _audioSource.Play();

        StartCoroutine(DestroySound());
    }

    void Update()
    {
        if (_audioSource.isPlaying && _soundData.Echo)
        {
            // Fade out the sound trigger as the sound clip nears its end
            if (_audioSource.time >= _audioSource.clip.length - 0.5f)
            {
                transform.localScale -= Vector3.one * _soundTriggerGrowthSpeed * Time.deltaTime;
            }
            else
            {
                transform.localScale += Vector3.one * _soundTriggerGrowthSpeed * Time.deltaTime;
            }
        }
    }

    IEnumerator DestroySound()
    {
        yield return new WaitForSeconds(_audioSource.clip.length);
        Destroy(gameObject);
    }
}
