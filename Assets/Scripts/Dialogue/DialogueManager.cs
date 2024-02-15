using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] TypewriterText typewriterText;
    public DialogueMomentData CurrentDialogueMoment;
    int dialogueIndex = 0;

    public void StartCurrentDialogue()
    {
        StartDialogue(CurrentDialogueMoment);
    }

    public void StartDialogue(DialogueMomentData dialogueMoment)
    {
        CurrentDialogueMoment = dialogueMoment;
        dialogueIndex = 0;

        TypeNextLine();
        
        typewriterText.OnTypingFinished.AddListener(TypeNextLine);
    }

    public void SkipLine()
    {
        if (dialogueIndex >= CurrentDialogueMoment.DialogueLines.Count) return;
        typewriterText.SkipTyping();
        Invoke("TypeNextLine", CurrentDialogueMoment.DialogueLines[dialogueIndex].DelayAfterTyping);
    }

    public void TypeNextLine()
    {
        if (dialogueIndex < CurrentDialogueMoment.DialogueLines.Count)
        {
            typewriterText.Type(CurrentDialogueMoment.DialogueLines[dialogueIndex]);
            dialogueIndex++;
        }
        else
        {
            FinishedDialogueMoment();
        }
    }

    public void FinishedDialogueMoment()
    {
        typewriterText.OnTypingFinished.RemoveListener(TypeNextLine);
        GameController.Instance.gameFSM.ChangeState(GameController.Instance.gameFSM.GamePlayState, 1f);
        Debug.Log("Finished dialogue moment");
    }
}
