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

    [Header("GameListen")]
    public GameObject gameListenPanel;
    float gameListenPanelYPos;

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

    [Header("Extra Actions")]
    public GameObject extraActionsPanel;
    float extraActionPanelYPos;

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


    #region AnimatePanels

    public void AnimateDialoguePanelIntro(float time = 0.5f)
    {
        if (!dialoguePanel) return;
        dialoguePanelXPos = dialoguePanel.GetComponent<RectTransform>().anchoredPosition.x;
        dialoguePanel.GetComponent<RectTransform>().DOAnchorPosX(0, time).SetEase(Ease.OutCubic).OnComplete(() => dialogueManager.StartCurrentDialogue());
        
    }

    public void AnimateDialoguePanelOutro(float time = 0.5f)
    {
        if (!dialoguePanel) return;
        dialoguePanel.GetComponent<RectTransform>().DOAnchorPosX(dialoguePanelXPos, time).SetEase(Ease.OutCubic);
        
    }

    public void AnimateGamePlayPanelIntro(float time = 0.5f)
    {
        if (!gamePlayPanel) return;
        gamePlayPanelXPos = gamePlayPanel.GetComponent<RectTransform>().anchoredPosition.x;
        gamePlayPanel.GetComponent<RectTransform>().DOAnchorPosX(0, time).SetEase(Ease.OutCubic);
    }

    public void AnimateGamePlayPanelOutro(float time = 0.5f)
    {
        if (!gamePlayPanel) return;
        gamePlayPanel.GetComponent<RectTransform>().DOAnchorPosX(gamePlayPanelXPos, time).SetEase(Ease.OutCubic);
    }

    public void AnimateGameListenPanelIntro(float time = 0.5f)
    {
        Debug.Log("Animating GameListenPanel Intro");
        if (!gameListenPanel) return;
        gameListenPanelYPos = gameListenPanel.GetComponent<RectTransform>().anchoredPosition.y;
        gameListenPanel.GetComponent<RectTransform>().DOAnchorPosY(0, time).SetEase(Ease.OutCubic);
    }

    public void AnimateGameListenPanelOutro(float time = 0.5f)
    {
        Debug.Log("Animating GameListenPanel Outro");
        if (!gameListenPanel) return;
        gameListenPanel.GetComponent<RectTransform>().DOAnchorPosY(gameListenPanelYPos, time).SetEase(Ease.OutCubic);
    }

    #endregion

    public void AddNotification(string notification)
    {
        notifications.Add(notification);
        GameObject notificationGO = Instantiate(notificationPrefab, notificationPanel.transform);
        notificationGO.GetComponent<TextMeshProUGUI>().text = notification;
    }

    #region Movement

    private void Update()
    {
        if (playerMovement.Move)
        {
            playerMoveText.text = "Stop Moving";

            foreach (Button button in playerButtonsToDisable)
            {
                button.interactable = false;
            }
        }
        else
        {
            playerMoveText.text = "Move Forward";
            foreach (Button button in playerButtonsToDisable)
            {
                button.interactable = true;
            }
        }
    }

    public void ToggleMove()
    {
        if (playerMovement.Move)
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
        AddNotification("I started moving forward.");
        

    }

    public void PlayerStopMoving()
    {
        playerMovement.StopMoving();
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

    #endregion

    public void MainMenu()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void ExtraActions(float time = 0.5f)
    {
        gameFSM.ChangeState(gameFSM.GameExtraActionState, time);
    }

    public void GamePlay(float time = 0.5f)
    {
        gameFSM.ChangeState(gameFSM.GamePlayState, time);
    }

    public void GameListen(float time = 0.5f)
    {
        gameFSM.ChangeState(gameFSM.GameListenState, time);
    }

    public void ExtraActionsIntro()
    {
        var rT = extraActionsPanel.GetComponent<RectTransform>();
        extraActionPanelYPos = rT.anchoredPosition.y;
        rT.DOAnchorPosY(787.2501f, 0.5f);

    }

    public void ExtraActionsOutro()
    {
        var rT = extraActionsPanel.GetComponent<RectTransform>();
        rT.DOAnchorPosY(extraActionPanelYPos, 0.5f);
    }
}
