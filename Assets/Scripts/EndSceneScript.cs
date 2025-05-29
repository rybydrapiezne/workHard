using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndSceneScript : MonoBehaviour
{
    public static int sceneId;
    public void returnToMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void restartLevel()
    {
        SceneManager.LoadScene(sceneId);
    }
}
