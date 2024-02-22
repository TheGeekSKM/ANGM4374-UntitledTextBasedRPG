using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;
    [SerializeField] TypewriterText typewriterText;
    public DialogueMomentData CurrentDialogueMoment;
    [SerializeField] int dialogueIndex = 0;
    [SerializeField] int clickIndex = 0;
    [SerializeField] bool dialogueFinished;
    Coroutine _startDialogueCoroutine;

    public UnityEvent OnDialogueStarted;
    public UnityEvent OnDialogueFinished;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void StartNewDialogue(DialogueMomentData dialogueMoment, float dialogueStartDelay = 0.5f)
    {
        if (!dialogueFinished) return;
        StartCurrentDialogue(dialogueStartDelay);
    }

    public void StartCurrentDialogue(float dialogueStartDelay = 0.5f)
    {
        if (_startDialogueCoroutine != null) StopCoroutine(_startDialogueCoroutine);
        _startDialogueCoroutine = StartCoroutine(StartDialogueCoroutine(dialogueStartDelay));
        
    }

    IEnumerator StartDialogueCoroutine(float dialogueStartDelay)
    {
        GameController.Instance.gameFSM.ChangeState(GameController.Instance.gameFSM.GameDialogueState);
        dialogueFinished = false;
        yield return new WaitForSeconds(dialogueStartDelay);
        StartDialogue(CurrentDialogueMoment);
    }

    public void StartDialogue(DialogueMomentData dialogueMoment)
    {
        OnDialogueStarted?.Invoke();
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
        OnDialogueFinished?.Invoke();
        if (dialogueFinished) return;

        typewriterText.OnTypingFinished.RemoveListener(TypeNextLine);
        GameController.Instance.gameFSM.ChangeState(GameController.Instance.gameFSM.GamePlayState, 0.5f);
        // Debug.Log("Finished dialogue moment");
        dialogueFinished = true;
    }
}
