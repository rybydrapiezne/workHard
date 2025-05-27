using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Localization.Settings;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField]
    AudioSource audioSourceOnClick;
    [SerializeField]
    AudioSource audioSourceOnHover;
    [SerializeField]
    GameObject loadingScreen;
    [SerializeField]
    GameObject mainMenuScreen;
    [SerializeField]
    GameObject optionsScreen;
    [SerializeField]
    Slider masterSlider;
    [SerializeField]
    Slider musicSlider;
    [SerializeField]
    Slider sfxSlider;
    [SerializeField]
    AudioMixer audioMixer;
    [SerializeField]
    Button applyButton;
    [SerializeField]
    GameObject newGameButton;
    [SerializeField]
    GameObject continueButton;
    [SerializeField]
    GameObject optionsButton;
    [SerializeField]
    GameObject exitButton;
    [SerializeField]
    Slider loadingBarFill;
    [SerializeField]
    float tweenTime = 0.1f;

    private void Start()
    {
        scaleAllButtons(5f);
    }

    private void scaleAllButtons(float direction)
    {
        LeanTween.scale(newGameButton, new Vector3(direction, direction, direction), tweenTime);
        LeanTween.scale(continueButton, new Vector3(direction, direction, direction), tweenTime).setDelay(tweenTime);
        LeanTween.scale(optionsButton, new Vector3(direction, direction, direction), tweenTime).setDelay(tweenTime*2);
        LeanTween.scale(exitButton, new Vector3(direction, direction, direction), tweenTime).setDelay(tweenTime*3);


    }
    public void NewGame()
    {
        StartCoroutine(newGameEnumerator());
    }
    private IEnumerator newGameEnumerator()
    {
        audioSourceOnClick.Play();
        scaleAllButtons(0f);
        yield return new WaitForSeconds(tweenTime * 4);
        loadingScreen.SetActive(true);
        mainMenuScreen.SetActive(false);
        StartCoroutine(LoadAsyncScene(1));
    }
    public void Continue()
    {
        audioSourceOnClick.Play();

    }
    public void ChangeLanguage(string localeIdentifier)
    {
        audioSourceOnClick.Play();
        var locale = LocalizationSettings.AvailableLocales.GetLocale(localeIdentifier);
        LocalizationSettings.SelectedLocale = locale;
    }
    public void Options()
    {
        StartCoroutine(optionsEnumerator());
    }
    private IEnumerator optionsEnumerator()
    {
        audioSourceOnClick.Play();
        scaleAllButtons(0);
        yield return new WaitForSeconds(tweenTime*4);
        mainMenuScreen.SetActive(false);
        optionsScreen.SetActive(true);
        audioMixer.GetFloat("masterSound", out float masterVolume);
        masterSlider.value = Mathf.Pow(10f, masterVolume / 20);
        Debug.Log(masterVolume);
        audioMixer.GetFloat("musicSound", out float musicVolume);
        musicSlider.value = Mathf.Pow(10f, musicVolume / 20);
        audioMixer.GetFloat("sfxSound", out float sfxVolume);
        sfxSlider.value = Mathf.Pow(10f, sfxVolume / 20);
    }
    public void Exit()
    {
        audioSourceOnClick.Play();
        scaleAllButtons(0f);
        Application.Quit();
    }
    IEnumerator LoadAsyncScene(int sceneId)
    {
        AsyncOperation operation=SceneManager.LoadSceneAsync(sceneId);

        while(!operation.isDone) 
        {
            float progressValue = Mathf.Clamp01(operation.progress / 0.9f);
            loadingBarFill.value = progressValue;
            yield return null;

        }
    }
    public void Apply()
    {
        SetAllVolumes();
        audioSourceOnClick.Play();
    }
    public void Back()
    {
        audioSourceOnClick.Play();
        optionsScreen.SetActive(false);
        mainMenuScreen.SetActive(true);
        scaleAllButtons(5f);
    }
    private void SetAllVolumes()
    {
        float volumeMaster = masterSlider.value;
        audioMixer.SetFloat("masterSound",Mathf.Log10(volumeMaster)*20);
        float volumeMusic = musicSlider.value;
        audioMixer.SetFloat("musicSound", Mathf.Log10(volumeMusic) * 20);
        float volumeSfx = sfxSlider.value;
        audioMixer.SetFloat("sfxSound", Mathf.Log10(volumeSfx) * 20);
    }
    public void setApplyButtonOn()
    {
        applyButton.interactable = true;
    }
}
