using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

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

    [Header("Extra Actions")]
    public GameObject extraActionsPanel;
    float extraActionPanelYPos;

    [Header("Inventory")]
    public GameObject inventoryPanel;
    float inventoryPanelXPos;

    [Header("Room Inventory")]
    public GameObject roomInventoryPanel;
    float roomInventoryPanelXPos;

    [Header("Game FSM")]
    public GameFSM gameFSM;


    [Header("Player")]
    public PlayerMovement playerMovement;
    public TextMeshProUGUI playerMoveText;
    public List<Button> playerButtonsToDisable;

    [Header("Enemy")]
    public EnemyController enemyController;

    [Header("Notifications")]
    public GameObject notificationPanel;
    [SerializeField] GameObject notificationPrefab;
    [SerializeField] List<string> notifications = new List<string>();

    [Header("Events")]
    public UnityEvent OnPlayerStartMove;
    public UnityEvent OnPlayerStopMoving;


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

    public void AnimateRoomInventoryPanelIntro(float time = 0.5f)
    {
        if (!roomInventoryPanel) return;
        roomInventoryPanelXPos = roomInventoryPanel.GetComponent<RectTransform>().anchoredPosition.x;
        roomInventoryPanel.GetComponent<RectTransform>().DOAnchorPosX(0, time).SetEase(Ease.OutCubic).OnComplete(() => PauseGame());
    }

    public void AnimateRoomInventoryPanelOutro(float time = 0.5f)
    {
        if (!roomInventoryPanel) return;
        roomInventoryPanel.GetComponent<RectTransform>().DOAnchorPosX(roomInventoryPanelXPos, time).SetEase(Ease.OutCubic).OnComplete(() => ResumeGame());
    }

    public void AnimateInventoryPanelIntro(float time = 0.5f)
    {
        if (!inventoryPanel) return;
        inventoryPanelXPos = inventoryPanel.GetComponent<RectTransform>().anchoredPosition.x;
        inventoryPanel.GetComponent<RectTransform>().DOAnchorPosX(0, time).SetEase(Ease.OutCubic).OnComplete(() => PauseGame());
    }

    public void AnimateInventoryPanelOutro(float time = 0.5f)
    {
        if (!inventoryPanel) return;
        inventoryPanel.GetComponent<RectTransform>().DOAnchorPosX(inventoryPanelXPos, time).SetEase(Ease.OutCubic).OnComplete(() => ResumeGame());
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
        
    }

    public void UpdateMoveButtonVisuals(bool move)
    {
        if (move)
        {
            Debug.Log("Player is moving");
            playerMoveText.text = "Stop Moving";

            foreach (Button button in playerButtonsToDisable)
            {
                button.interactable = false;
            }
        }
        else
        {
            Debug.Log("Player is not moving");
            playerMoveText.text = "Move Forward" + "\n(-1 Health)";
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
            OnPlayerStopMoving?.Invoke();
            PlayerStopMoving();
            
        }
        else
        {
            OnPlayerStartMove?.Invoke();
            PlayerMoveForward();
            UpdateMoveButtonVisuals(true);
        }
    }

    

    public void PlayerMoveForward()
    {
        playerMovement.MoveForward();
        AddNotification("I started moving forward.\n");
        UpdateMoveButtonVisuals(true);
    }

    public void PlayerStopMoving()
    {
        playerMovement.StopMoving();
        AddNotification("I stopped moving.\n");
        UpdateMoveButtonVisuals(false);
    }

    public void PlayerTurnLeft()
    {
        playerMovement.TurnLeft();
        AddNotification("I turned left.\n");
    }

    public void PlayerTurnRight()
    {
        playerMovement.TurnRight();
        AddNotification("I turned right.\n");
    }

    #endregion


    public void PauseGame()
    {
        enemyController.CanWalk = false;
    }
    public void ResumeGame()
    {
        enemyController.CanWalk = true;
    }


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
        rT.DOAnchorPosY(0, 0.5f);

    }

    public void ExtraActionsOutro()
    {
        var rT = extraActionsPanel.GetComponent<RectTransform>();
        rT.DOAnchorPosY(extraActionPanelYPos, 0.5f);
    }
}
