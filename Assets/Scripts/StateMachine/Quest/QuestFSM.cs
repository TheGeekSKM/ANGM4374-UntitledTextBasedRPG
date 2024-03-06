using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestFSM : StateMachineMB
{
    QuestController questController;

    public QuestIntroBandage QuestIntroBandage {get ; private set;}

    void OnValidate()
    {
        if (questController == null)
        {
            questController = GetComponent<QuestController>();
        }
    }

    void Awake()
    {
        QuestIntroBandage = new QuestIntroBandage(questController, this);
    }

    
}
