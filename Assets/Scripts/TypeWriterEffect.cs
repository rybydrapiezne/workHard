using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TypeWriterEffect : MonoBehaviour
{
    [SerializeField]
    string inputText;
    TMP_Text textfield;
    public bool finished = false;
    private int textIndex = 0;
    private WaitForSecondsRealtime _delay;
    private WaitForSecondsRealtime _interpunctuationDelay;
    Coroutine typewriterCoroutine;

    [SerializeField]
    private float charactersPerSecond = 10;
    [SerializeField]
    private float interpunctuationDelay = 1 / 5;

    private void Start()
    {

        textfield = GetComponent<TMP_Text>();
        _delay = new WaitForSecondsRealtime(1 / charactersPerSecond);
        _interpunctuationDelay = new WaitForSecondsRealtime(interpunctuationDelay);
        setText(inputText);
    }
    IEnumerator write()
    {
        TMP_TextInfo info = textfield.textInfo;
        while (textIndex < info.characterCount)
        {
            char character = info.characterInfo[textIndex].character;
            textfield.maxVisibleCharacters++;

            if (character == '?' || character == '.' || character == ',' ||
                character == ';' || character == '!' || character == ':' ||
                character == '-')
            {
                yield return _interpunctuationDelay;
            }
            else
            {
                yield return _delay;
            }

            textIndex++;
        }
        finished = true;
    }

    private void setText(string text)
    {
        if (typewriterCoroutine != null)
        {
            StopCoroutine(typewriterCoroutine);
        }
        textfield.text = text;
        textfield.ForceMeshUpdate();
        textfield.maxVisibleCharacters = 0;
        textIndex = 0;
        typewriterCoroutine = StartCoroutine(write());

    }

    public void forceEnd()
    {
        textfield.maxVisibleCharacters = 1000;
        textfield.ForceMeshUpdate();
        StopCoroutine(typewriterCoroutine);
        finished = true;
    }
}
