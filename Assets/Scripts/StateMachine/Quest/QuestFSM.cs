using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestFSM : StateMachineMB
{
    QuestController questController;

    public QuestIntroBandage QuestIntroBandage {get ; private set;}
    public QuestReturnBandage QuestReturnBandage {get ; private set;}

    void OnValidate()
    {
        if (questController == null)
        {
            questController = GetComponent<QuestController>();
        }
    }

    void Start()
    {
        ChangeState(QuestIntroBandage);
    
    }

    void Awake()
    {
        QuestIntroBandage = new QuestIntroBandage(questController, this);
        QuestReturnBandage = new QuestReturnBandage(questController, this);
    }

    
}
