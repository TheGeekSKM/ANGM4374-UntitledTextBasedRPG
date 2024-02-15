using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameController : MonoBehaviour
{
    public static GameController Instance {get; private set;}
    public DialogueManager dialogueManager; 
    public GameObject dialoguePanel;
    float dialoguePanelXPos;

    public GameFSM gameFSM;

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
}
