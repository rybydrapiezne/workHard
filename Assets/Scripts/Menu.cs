using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
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
    Slider loadingBarFill;

    public void NewGame()
    {
        audioSourceOnClick.Play();
        loadingScreen.SetActive(true);
        mainMenuScreen.SetActive(false);
        StartCoroutine(LoadAsyncScene(1));
    }
    public void Continue()
    {
        audioSourceOnClick.Play();

    }
    public void Options()
    {
        audioSourceOnClick.Play();
        mainMenuScreen.SetActive(false);
        optionsScreen.SetActive(true);
        audioMixer.GetFloat("masterSound", out float masterVolume);
        masterSlider.value = Mathf.Pow(10f, masterVolume/20);
        Debug.Log(masterVolume);
        audioMixer.GetFloat("musicSound", out float musicVolume);
        musicSlider.value = Mathf.Pow(10f, musicVolume / 20);
        audioMixer.GetFloat("sfxSound", out float sfxVolume);
        sfxSlider.value = Mathf.Pow(10f, sfxVolume / 20);

    }
    public void Exit()
    {
        audioSourceOnClick.Play();
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
