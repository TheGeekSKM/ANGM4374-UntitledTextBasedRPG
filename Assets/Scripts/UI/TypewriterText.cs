using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TypewriterText : MonoBehaviour
{
    [Header("References")]
    [SerializeField] TextMeshProUGUI _text;

    [Header("Typing Settings")]
    public TypewriterSettings settings = new TypewriterSettings();
    private float originDelayBetweenChars;
    private bool lastCharPunctuation = false;
    private char charComma;
    private char charPeriod;
    [SerializeField] string story;

    [Header("Audio Settings")]
    [Tooltip("When true requires AudioSource on this object.")]
    public bool useAudio = true;
    [Range(0f, 2f)]
    public float volume = .3f;
    [Tooltip("GameObject with AudioSource component.")]
    public GameObject AudioTyping;
    private AudioSource TypingFX;


    [Header("Extra Settings")]
    [SerializeField] bool _clearOnStart = true;
    [SerializeField] bool _clearOnFinish = true;


    DialogueLineData _currentDialogueLineData;
    Coroutine _typingCoroutine;


    public UnityEngine.Events.UnityEvent OnTypingFinished;

    void Awake()
    {
        if (useAudio)
        {
            TypingFX = GetComponent<AudioSource>();
            TypingFX.clip = AudioTyping.GetComponent<AudioSource>().clip;
        }

        _text = GetComponent<TextMeshProUGUI>();
        originDelayBetweenChars = settings.delayBetweenChars;

        charComma = Convert.ToChar(44);
        charPeriod = Convert.ToChar(46);
        //charEmpty = Convert.ToChar(" ");//Convert.ToChar(255);

    }

    void Start()
    {
        if (_clearOnStart) _text.text = "";
    }

    public void Type(DialogueLineData dialogueLineData)
    {
        _currentDialogueLineData = dialogueLineData;
        story = _currentDialogueLineData.NPCData ? _currentDialogueLineData.NPCData.NPCName + ":\n" + _currentDialogueLineData.DialogueLine : _currentDialogueLineData.DialogueLine;
        if (_typingCoroutine != null) StopCoroutine(_typingCoroutine);
        _text.text = "";
        Invoke("StartTyping", settings.delayToStart);
    }

    public void SkipTyping()
    {
        if (_typingCoroutine != null) StopCoroutine(_typingCoroutine);
        _text.text = story;
        // Invoke("FinishedTyping", _currentDialogueLineData.DelayAfterTyping);
    }

    void FinishedTyping()
    {
        OnTypingFinished.Invoke();
        _currentDialogueLineData = null;
    }

    void StartTyping()
    {
        _typingCoroutine = StartCoroutine(PlayText());
    }

    IEnumerator PlayText()
    {
        yield return new WaitForSeconds(settings.delayToStart);
        foreach (char c in story)
        {
            settings.delayBetweenChars = originDelayBetweenChars;

            if (lastCharPunctuation)  //If previous character was a comma/period, pause typing
            {
                if (useAudio) TypingFX.Pause();
                yield return new WaitForSeconds(settings.delayBetweenChars = settings.delayAfterPunctuation);
                lastCharPunctuation = false;
            }

            if (c == charComma || c == charPeriod)
            {
                if (useAudio) TypingFX.Pause();
                lastCharPunctuation = true;
            }

            if (useAudio) TypingFX.PlayOneShot(TypingFX.clip, volume);
            _text.text += c;
            yield return new WaitForSeconds(settings.delayBetweenChars);
        }

        if (useAudio) TypingFX.Stop();

        yield return new WaitForSeconds(_currentDialogueLineData.DelayAfterTyping);
        if (_clearOnFinish) _text.text = "";
        FinishedTyping();
    }
}

[Serializable]
public class TypewriterSettings
{
    public float delayToStart;
    public float delayBetweenChars;
    public float delayAfterPunctuation;

    public TypewriterSettings()
    {
        delayToStart = 0f;
        delayBetweenChars = .125f;
        delayAfterPunctuation = .5f;
    }
    public TypewriterSettings(float delayToStart, float delayBetweenChars, float delayAfterPunctuation, float delayAfterTyping)
    {
        this.delayToStart = delayToStart;
        this.delayBetweenChars = delayBetweenChars;
        this.delayAfterPunctuation = delayAfterPunctuation;
    }
}
