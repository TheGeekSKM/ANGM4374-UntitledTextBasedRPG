using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestController : MonoBehaviour
{
    QuestFSM questFSM;
    public RoomData currentQuestRoom;
    public ItemData currentQuestItem;

    [Header("QuestIntroBandage")]
    public RoomData bandageRoom;
    public ItemData bandageItem;

    void OnValidate()
    {
        if (questFSM == null)
        {
            questFSM = GetComponent<QuestFSM>();
        }
    }

    public void QuestIntroBandageStarted()
    {
        currentQuestRoom = bandageRoom;
        currentQuestItem = bandageItem;
    }
}
