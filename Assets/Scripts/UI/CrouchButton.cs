using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CrouchButton : MonoBehaviour
{
    [SerializeField] bool crouching = false;
    [SerializeField] Button _crouchButton;
    [SerializeField] TextMeshProUGUI _crouchButtonText;

    [TextArea(3, 10)]
    [SerializeField] string originalText;

    void OnValidate()
    {
        if (_crouchButton == null)
        {
            _crouchButton = GetComponent<Button>();
        }
    }

    void Start()
    {
        _crouchButton.onClick.AddListener(Crouch);
        _crouchButtonText = _crouchButton.GetComponentInChildren<TextMeshProUGUI>();
        originalText = _crouchButtonText.text;
    }

    public void Crouch()
    {
        crouching = !crouching;
        if (crouching)
        {
            GameController.Instance.playerMovement.BeginCrouch();
            GameController.Instance.playerMovement.GetComponent<PlayerHealth>().TakeContinuousDamage(3);
            _crouchButtonText.text = "Stand Back Up";
        }
        else
        {
            GameController.Instance.playerMovement.EndCrouch();
            GameController.Instance.playerMovement.GetComponent<PlayerHealth>().StopTakingDamage();
            _crouchButtonText.text = originalText;
            Debug.Log("Standing");
        }
    }
}
