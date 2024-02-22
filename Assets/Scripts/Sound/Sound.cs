using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class Sound : MonoBehaviour
{
    [SerializeField] SoundData _soundData;
    [SerializeField] AudioSource _audioSource;
    [SerializeField] float _soundTriggerGrowthSpeed = 0.1f;
    [SerializeField] SpriteRenderer _spriteRenderer;

    void OnValidate()
    {
        if (_audioSource == null)
        {
            _audioSource = GetComponent<AudioSource>();
        }
        if (_spriteRenderer == null)
        {
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }
    }

    public void SetSound(SoundData soundData)
    {
        _soundData = soundData;
        _audioSource.clip = _soundData.Sound;
        _audioSource.volume = _soundData.Volume;
        transform.localScale = Vector3.one * _soundData.Volume * 2;
        _audioSource.Play();

        StartCoroutine(DestroySound());
    }

    void OnTriggerEnter(Collider other)
    {
        // let the enemy know the position of this sound and set it as a target for the enemy navmesh
        
    }

    void Update()
    {
        if (_audioSource.isPlaying && _soundData.Echo)
        {
            // Fade out the sound trigger as the sound clip nears its end
            transform.localScale += Vector3.one * _soundTriggerGrowthSpeed * Time.deltaTime;
            
        }

        if (_audioSource.time >= _audioSource.clip.length - 0.5f)
            {
                //dims the alpha of the sprite renderer color
                _spriteRenderer.color = new Color(_spriteRenderer.color.r, _spriteRenderer.color.g, _spriteRenderer.color.b, _spriteRenderer.color.a - 0.1f);
            }
    }

    IEnumerator DestroySound()
    {
        yield return new WaitForSeconds(_audioSource.clip.length);
        Destroy(gameObject);
    }
}
