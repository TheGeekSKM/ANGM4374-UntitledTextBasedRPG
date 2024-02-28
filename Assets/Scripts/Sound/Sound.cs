using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class Sound : MonoBehaviour
{
    [SerializeField] SoundData _soundData;
    [SerializeField] AudioSource _audioSource;
    [SerializeField] float _soundTriggerGrowthSpeed = 5f;
    [SerializeField] SpriteRenderer _spriteRenderer;

    [SerializeField] float _clipLength;

    PlayerMovement _player;

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
        _player = GameController.Instance.playerMovement;
        if (_player && _player.Crouch) _soundTriggerGrowthSpeed = 10f;

        _soundData = soundData;
        _audioSource.clip = _soundData.Sound;
        if (_audioSource.clip) _clipLength = _audioSource.clip.length * 5f;
        else _clipLength = 10f;
        _audioSource.volume = _soundData.Volume;
        transform.localScale = Vector3.one * _soundData.Volume * 2;
        _audioSource.Play();

        //rotate image randomly around the y axis
        transform.Rotate(Vector3.up, Random.Range(0, 360));
        

        StartCoroutine(DestroySound());
    }

    void OnTriggerEnter(Collider other)
    {
        // let the enemy know the position of this sound and set it as a target for the enemy navmesh
        var enemyController = other.GetComponent<EnemyController>();

        if (!enemyController)
        {
            //Debug.Log("No Enemy Controller Found");
            return;
        }

        enemyController.EnemyTarget(transform);
    }

    void Update()
    {
        if (_soundData.Echo)
        {
            // Fade out the sound trigger as the sound clip nears its end
            transform.localScale += Vector3.one * _soundTriggerGrowthSpeed * Time.deltaTime;
            
        }

        if (!_soundData.Sound) return;

        if (_audioSource.time >= _clipLength - 1f)
        {
            //dims the alpha of the sprite renderer color
            _spriteRenderer.color = new Color(_spriteRenderer.color.r, _spriteRenderer.color.g, _spriteRenderer.color.b, _spriteRenderer.color.a - 0.1f);
        }
    }

    IEnumerator DestroySound()
    {
        yield return new WaitForSeconds(_clipLength);
        Destroy(gameObject);
    }
}
