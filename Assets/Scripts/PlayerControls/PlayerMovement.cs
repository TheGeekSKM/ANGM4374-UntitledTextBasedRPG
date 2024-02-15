using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody _rigidbody;
    public float moveSpeed = 5f;
    bool _move = false;
    public bool IsMoving => _move;

    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
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
        transform.Rotate(Vector3.up, -90);
    }

    public void TurnRight()
    {
        transform.Rotate(Vector3.up, 90);
    }

    void FixedUpdate()
    {
        if (_move)
        {
            _rigidbody.velocity = new Vector3(0, 0, moveSpeed);
        }
        else
        {
            _rigidbody.velocity = Vector3.zero;
        }
    }
}
