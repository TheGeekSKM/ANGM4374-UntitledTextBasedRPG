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
        Move = false;
        GameController.Instance.ToggleMove();
        GameController.Instance.AddNotification("I hit a wall!");
    }

    public void MoveForward()
    {
        Move = true;
    }

    public void StopMoving()
    {
        Move = false;
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
        GameController.Instance.AddNotification("I'm crouching now...Hopefully, I can stay quiet.");
    }

    public void EndCrouch()
    {
        Crouch = false;
        GameController.Instance.AddNotification("I stopped crouching...I gotta be careful now.");
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
