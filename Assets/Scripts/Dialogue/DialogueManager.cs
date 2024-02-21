using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] TypewriterText typewriterText;
    public DialogueMomentData CurrentDialogueMoment;
    [SerializeField] int dialogueIndex = 0;
    [SerializeField] int clickIndex = 0;
    [SerializeField] bool dialogueFinished;

    public void StartCurrentDialogue()
    {
        StartDialogue(CurrentDialogueMoment);
        dialogueFinished = false;
    }

    public void StartDialogue(DialogueMomentData dialogueMoment)
    {
        dialogueFinished = false;

        CurrentDialogueMoment = dialogueMoment;
        dialogueIndex = 0;

        TypeNextLine();
        
        typewriterText.OnTypingFinished.AddListener(TypeNextLine);
    }

    public void SkipLine()
    {
        if (dialogueIndex >= CurrentDialogueMoment.DialogueLines.Count)
        {
            typewriterText.SkipTyping();
            Invoke("FinishedDialogueMoment", CurrentDialogueMoment.DialogueLines[dialogueIndex-1].DelayAfterTyping);
            return;
        }

        if (clickIndex == 0)
        {
            
            clickIndex++;
            typewriterText.SkipTyping();
            Invoke("TypeNextLine", CurrentDialogueMoment.DialogueLines[dialogueIndex].DelayAfterTyping);
        }
        else
        {
            CancelInvoke();
            TypeNextLine();
            clickIndex = 0;
        }
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
        if (dialogueFinished) return;

        typewriterText.OnTypingFinished.RemoveListener(TypeNextLine);
        GameController.Instance.gameFSM.ChangeState(GameController.Instance.gameFSM.GamePlayState, 0.5f);
        Debug.Log("Finished dialogue moment");
        dialogueFinished = true;
    }
}
