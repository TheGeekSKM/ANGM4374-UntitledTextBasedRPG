using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody _rigidbody;
    public float moveSpeed = 5f;
    public bool Move = false;
    public bool Crouch = false;

    Vector3 _moveDirection;
    Coroutine MoveSoundsRoutine;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    void Start()
    {
        _moveDirection = new Vector3(0, 0, -1); // forward
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Walls"))
        {
            HitWall();
        }
    }

    void HitWall()
    {
        GameController.Instance.PlayerStopMoving();
        GameController.Instance.AddNotification("I hit a wall!\n");
    }

    public void MoveForward()
    {
        Move = true;
        if (MoveSoundsRoutine == null)
        {
            MoveSoundsRoutine = StartCoroutine(MoveSounds());
        }
        else
        {
            StopCoroutine(MoveSoundsRoutine);
            MoveSoundsRoutine = StartCoroutine(MoveSounds());
        }
    }

    public void StopMoving()
    {
        Debug.Log("Stopping movement");
        Move = false;
        if (MoveSoundsRoutine != null)
        {
            StopCoroutine(MoveSoundsRoutine);
        }
    }

    IEnumerator MoveSounds()
    {
        while (Move)
        {
            SoundManager.Instance.Sound(transform, SoundAtlas.Instance.PlayerFootstepSound);
            yield return new WaitForSeconds(0.5f);
        }
    }

    public void TurnLeft()
    {
        _moveDirection = Quaternion.Euler(0, -90, 0) * _moveDirection;
    }

    public void TurnRight()
    {
        _moveDirection = Quaternion.Euler(0, 90, 0) * _moveDirection;
    }

    public void BeginCrouch()
    {
        Crouch = true;
        GameController.Instance.AddNotification("I'm crouching now...Hopefully, I can stay quiet.\n");
        transform.localScale = Vector3.one;
    }

    public void EndCrouch()
    {
        Crouch = false;
        GameController.Instance.AddNotification("\nI stopped crouching...I gotta be careful now.\n");
        transform.localScale = Vector3.one * 2;

    }

    void FixedUpdate()
    {
        if (Move)
        {
            // adds velocity to the rigidbody in local space
            _rigidbody.velocity = _moveDirection * moveSpeed;
        }
        else
        {
            _rigidbody.velocity = Vector3.zero;
        }
    }
}
