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
        story = dialogueLineData.NPCData ? dialogueLineData.NPCData.NPCName + ":\n" + dialogueLineData.DialogueLine : dialogueLineData.DialogueLine;
        StopCoroutine(PlayText());
        _text.text = "";
        Invoke("StartTyping", settings.delayToStart);
    }

    void StartTyping()
    {
        StartCoroutine(PlayText());
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

        yield return new WaitForSeconds(settings.delayAfterTyping);
        if (_clearOnFinish) _text.text = "";
        OnTypingFinished.Invoke();
    }
}

[Serializable]
public class TypewriterSettings
{
    public float delayToStart;
    public float delayBetweenChars;
    public float delayAfterPunctuation;
    public float delayAfterTyping;

    public TypewriterSettings()
    {
        delayToStart = 0f;
        delayBetweenChars = .125f;
        delayAfterPunctuation = .5f;
        delayAfterTyping = .2f;
    }
    public TypewriterSettings(float delayToStart, float delayBetweenChars, float delayAfterPunctuation, float delayAfterTyping)
    {
        this.delayToStart = delayToStart;
        this.delayBetweenChars = delayBetweenChars;
        this.delayAfterPunctuation = delayAfterPunctuation;
        this.delayAfterTyping = delayAfterTyping;
    }
}
