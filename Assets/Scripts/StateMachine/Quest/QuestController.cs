using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuestController : MonoBehaviour
{
    public static QuestController Instance { get; set; }
    QuestFSM questFSM;
    public RoomTrigger currentQuestRoom;
    public ItemData currentQuestItem;
    public Inventory inventory;
    public RoomManager roomManager;
    public SoundData pingQuestItemSound;

    [Header("QuestIntroBandage")]
    public RoomTrigger bandageRoom;
    public ItemData bandageItem;

    [Header("QuestReturnBandage")]
    public RoomTrigger startingRoom;
    public DialogueMomentData completedDialogue;


    void OnValidate()
    {
        if (questFSM == null)
        {
            questFSM = GetComponent<QuestFSM>();
        }
    }

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
    }

    void OnEnable()
    {
        inventory.OnItemAdded.AddListener(OnItemAdded);
        inventory.OnItemRemoved.AddListener(OnItemRemoved);
        roomManager.OnRoomEnter += OnRoomEntered;
    }

    void OnDisable()
    {
        inventory.OnItemAdded.RemoveListener(OnItemAdded);
        inventory.OnItemRemoved.RemoveListener(OnItemRemoved);
        roomManager.OnRoomEnter -= OnRoomEntered;
    }

    public void QuestIntroBandageStarted()
    {
        currentQuestRoom = bandageRoom;
        currentQuestItem = bandageItem;
        GameController.Instance.AddNotification("I need to find the bandages.\n");
    }

    public void QuestIntroBandageCompleted()
    {
        GameController.Instance.AddNotification("I found the bandages.");
        questFSM.ChangeState(questFSM.QuestReturnBandage);
    }

    public void QuestReturnBandageStarted()
    {
        currentQuestRoom = startingRoom;
        GameController.Instance.AddNotification("I should bring them back to the man in the room.\n");
    }

    public void QuestReturnBandageCompleted()
    {
        DialogueManager.Instance.StartNewDialogue(completedDialogue, 0.5f);
        GameController.Instance.gameFSM.ChangeState(GameController.Instance.gameFSM.GameDialogueState);
        DialogueManager.Instance.OnDialogueFinished.AddListener(OnQuestCompleted);
    }

    void OnQuestCompleted()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }

    public void LocateQuestItem()
    {
        Debug.Log($"Locating quest item {currentQuestRoom.name}");
        if (currentQuestRoom == null) return;
        SoundManager.Instance.Sound(currentQuestRoom.transform.position, pingQuestItemSound);
    }
        

    void OnItemAdded(ItemData item)
    {
        if (item == currentQuestItem)
        {
            QuestIntroBandageCompleted();
        }
    }

    void OnItemRemoved(ItemData item)
    {
        
    }

    void OnRoomEntered(RoomData room)
    {
        if (room == currentQuestRoom.roomData)
        {
            if (currentQuestRoom == startingRoom)
            {
                Debug.Log("QuestReturnBandageCompleted");
                QuestReturnBandageCompleted();
            }
        }
    }
}
