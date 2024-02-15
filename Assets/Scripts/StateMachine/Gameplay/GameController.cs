using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class GameController : MonoBehaviour
{
    public static GameController Instance {get; private set;}

    [Header("Dialogue")]
    public DialogueManager dialogueManager; 
    public GameObject dialoguePanel;
    float dialoguePanelXPos;

    [Header("GamePlay")]
    public GameObject gamePlayPanel;
    float gamePlayPanelXPos;

    [Header("Game FSM")]
    public GameFSM gameFSM;


    [Header("Player")]
    public PlayerMovement playerMovement;
    public TextMeshProUGUI playerMoveText;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        gameFSM = GetComponent<GameFSM>();
    }

    public void AnimateDialoguePanelIntro()
    {
        if (!dialoguePanel) return;
        dialoguePanelXPos = dialoguePanel.GetComponent<RectTransform>().anchoredPosition.x;
        dialoguePanel.GetComponent<RectTransform>().DOAnchorPosX(0, 1f).SetEase(Ease.OutCubic).OnComplete(() => dialogueManager.StartCurrentDialogue());
        
    }

    public void AnimateDialoguePanelOutro()
    {
        if (!dialoguePanel) return;
        dialoguePanel.GetComponent<RectTransform>().DOAnchorPosX(dialoguePanelXPos, 1f).SetEase(Ease.OutCubic);
        
    }

    public void AnimateGamePlayPanelIntro()
    {
        if (!gamePlayPanel) return;
        gamePlayPanel.SetActive(true);
        gamePlayPanelXPos = gamePlayPanel.GetComponent<RectTransform>().anchoredPosition.x;
        gamePlayPanel.GetComponent<RectTransform>().DOAnchorPosX(0, 1f).SetEase(Ease.OutCubic);
    }

    public void AnimateGamePlayPanelOutro()
    {
        if (!gamePlayPanel) return;
        gamePlayPanel.GetComponent<RectTransform>().DOAnchorPosX(gamePlayPanelXPos, 1f).SetEase(Ease.OutCubic).OnComplete(() => gamePlayPanel.SetActive(false));
    }

    public void ToggleMove()
    {
        if (playerMovement.IsMoving)
        {
            PlayerStopMoving();
        }
        else
        {
            PlayerMoveForward();
        }
    }

    public void PlayerMoveForward()
    {
        playerMovement.MoveForward();
        playerMoveText.text = "Stop Moving";
    }

    public void PlayerStopMoving()
    {
        playerMovement.StopMoving();
        playerMoveText.text = "Move Forward";
    }

    public void PlayerTurnLeft()
    {
        playerMovement.TurnLeft();
    }

    public void PlayerTurnRight()
    {
        playerMovement.TurnRight();
    }
}
