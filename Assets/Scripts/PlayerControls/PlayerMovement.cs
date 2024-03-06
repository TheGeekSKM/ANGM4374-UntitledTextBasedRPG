using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rB;
    float _moveSpeed;
    public bool Move = false;
    public bool Crouch = false;
    public GameObject Art;
    public AttributeData attributeData;

    Vector3 _moveDirection;
    Coroutine MoveSoundsRoutine;

    void Awake()
    {
        rB = GetComponent<Rigidbody>();
    }

    void Start()
    {
        _moveDirection = new Vector3(0, 0, -1); // forward
        _moveSpeed = attributeData.Agility;
    }

    void OnEnable()
    {
        attributeData.OnAttributeChange += OnAttributeChange;
    }

    void OnDisable()
    {
        attributeData.OnAttributeChange -= OnAttributeChange;
    }

    void OnAttributeChange(Attributes attribute)
    {
        switch (attribute)
        {
            case Attributes.Agility:
                _moveSpeed = attributeData.Agility;
                break;
        }
    }

    void Update()
    {
        // if (Crouch) Debug.Log("Crouching");
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
        SoundManager.Instance.Sound(transform, SoundAtlas.Instance.PlayerTurnSound);
    }

    public void TurnRight()
    {
        _moveDirection = Quaternion.Euler(0, 90, 0) * _moveDirection;
        SoundManager.Instance.Sound(transform, SoundAtlas.Instance.PlayerTurnSound);
    }

    public void BeginCrouch()
    {
        Crouch = true;
        GameController.Instance.AddNotification("I'm crouching now...Hopefully, I can stay quiet.\n");
        Art.transform.localScale = Vector3.one * 0.5f;
        GetComponent<BoxCollider>().enabled = false;
    }

    public void EndCrouch()
    {
        Crouch = false;
        GameController.Instance.AddNotification("\nI stopped crouching...I gotta be careful now.\n");
        transform.localScale = Vector3.one * 2;
        Art.transform.localScale = Vector3.one;
        GetComponent<BoxCollider>().enabled = true;
    }

    void FixedUpdate()
    {
        if (Move)
        {
            // adds velocity to the rigidbody in local space
            rB.velocity = _moveDirection * _moveSpeed;
        }
        else
        {
            rB.velocity = Vector3.zero;
        }
    }
}
