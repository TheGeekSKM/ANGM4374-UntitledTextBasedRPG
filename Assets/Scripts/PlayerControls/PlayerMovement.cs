using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody _rigidbody;
    public float moveSpeed = 5f;
    bool _move = false;
    public bool IsMoving => _move;

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
        _move = false;
        GameController.Instance.AddNotification("I hit a wall!");
    }

    public void MoveForward()
    {
        _move = true;
    }

    public void StopMoving()
    {
        _move = false;
    }

    public void TurnLeft()
    {
        _moveDirection = Quaternion.Euler(0, -90, 0) * _moveDirection;
    }

    public void TurnRight()
    {
        _moveDirection = Quaternion.Euler(0, 90, 0) * _moveDirection;
    }

    void FixedUpdate()
    {
        if (_move)
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
