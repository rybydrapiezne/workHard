using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.Localization;

public class TypeWriterEffect : MonoBehaviour
{
    [SerializeField] private LocalizedString localizedString;
    private TMP_Text textfield;
    public bool finished = false;
    private int textIndex = 0;
    private WaitForSecondsRealtime _delay;
    private WaitForSecondsRealtime _interpunctuationDelay;
    private Coroutine typewriterCoroutine;

    [SerializeField] private float charactersPerSecond = 10;
    [SerializeField] private float interpunctuationDelay = 0.2f;

    void OnEnable()
    {
        if (localizedString != null)
        {
            localizedString.StringChanged += UpdateString;
        }
    }

    void OnDisable()
    {
        if (localizedString != null)
        {
            localizedString.StringChanged -= UpdateString;
        }
    }

    void Start()
    {
        textfield = GetComponent<TMP_Text>();
        _delay = new WaitForSecondsRealtime(1f / charactersPerSecond);
        _interpunctuationDelay = new WaitForSecondsRealtime(interpunctuationDelay);

        if (localizedString != null)
        {
            UpdateString(localizedString.GetLocalizedString());
        }
    }

    void UpdateString(string s)
    {
        setText(s);
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
        finished = false;
        typewriterCoroutine = StartCoroutine(write());
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

    public void forceEnd()
    {
        if (typewriterCoroutine != null)
        {
            StopCoroutine(typewriterCoroutine);
        }
        textfield.maxVisibleCharacters = textfield.textInfo.characterCount; // Use characterCount instead of hardcoding 1000
        textfield.ForceMeshUpdate();
        finished = true;
    }
}