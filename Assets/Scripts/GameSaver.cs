using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSaver : MonoBehaviour
{
    public void SaveGame()
    {
        int sceneIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
        PlayerPrefs.SetInt("SavedScene", sceneIndex);
        PlayerPrefs.Save();
    }
    public int LoadGame()
    {
        int savedSceneIndex = PlayerPrefs.GetInt("SavedScene", 0);
        return savedSceneIndex;
    }
    public void SaveSettings(string language, float masterVolume, float musicVolume, float sfxVolume)
    {
        PlayerPrefs.SetString("Language", language);
        PlayerPrefs.SetFloat("MasterVolume", masterVolume);
        PlayerPrefs.SetFloat("MusicVolume", musicVolume);
        PlayerPrefs.SetFloat("SFXVolume", sfxVolume);
        PlayerPrefs.Save();
    }
    public void LoadSettings(out string language, out float masterVolume, out float musicVolume, out float sfxVolume)
    {
        language = PlayerPrefs.GetString("Language", "en");
        masterVolume = PlayerPrefs.GetFloat("MasterVolume", 1.0f);
        musicVolume = PlayerPrefs.GetFloat("MusicVolume", 1.0f);
        sfxVolume = PlayerPrefs.GetFloat("SFXVolume", 1.0f);
    }
}
