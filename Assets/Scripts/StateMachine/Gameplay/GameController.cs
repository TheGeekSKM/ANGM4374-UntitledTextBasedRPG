using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    public List<Button> playerButtonsToDisable;

    [Header("Notifications")]
    public GameObject notificationPanel;
    [SerializeField] GameObject notificationPrefab;
    [SerializeField] List<string> notifications = new List<string>();

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
        gamePlayPanelXPos = gamePlayPanel.GetComponent<RectTransform>().anchoredPosition.x;
        gamePlayPanel.GetComponent<RectTransform>().DOAnchorPosX(0, 1f).SetEase(Ease.OutCubic);
    }

    public void AnimateGamePlayPanelOutro()
    {
        if (!gamePlayPanel) return;
        gamePlayPanel.GetComponent<RectTransform>().DOAnchorPosX(gamePlayPanelXPos, 1f).SetEase(Ease.OutCubic);
    }

    public void ToggleMove()
    {
        if (playerMovement.IsMoving)
        {
            PlayerStopMoving();
            foreach (Button button in playerButtonsToDisable)
            {
                button.interactable = true;
            }
        }
        else
        {
            PlayerMoveForward();
            foreach (Button button in playerButtonsToDisable)
            {
                button.interactable = false;
            }
        }
    }

    public void AddNotification(string notification)
    {
        notifications.Add(notification);
        GameObject notificationGO = Instantiate(notificationPrefab, notificationPanel.transform);
        notificationGO.GetComponent<TextMeshProUGUI>().text = notification;
    }

    public void PlayerMoveForward()
    {
        playerMovement.MoveForward();
        playerMoveText.text = "Stop Moving";
        AddNotification("I started moving forward.");

    }

    public void PlayerStopMoving()
    {
        playerMovement.StopMoving();
        playerMoveText.text = "Move Forward";
        AddNotification("I stopped moving.");
    }

    public void PlayerTurnLeft()
    {
        playerMovement.TurnLeft();
        AddNotification("I turned left.");
    }

    public void PlayerTurnRight()
    {
        playerMovement.TurnRight();
        AddNotification("I turned right.");
    }

    public void MainMenu()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
