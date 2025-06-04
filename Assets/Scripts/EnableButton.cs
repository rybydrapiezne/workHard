using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnableButton : MonoBehaviour
{
    void Start()
    {

        if (PlayerPrefs.GetInt("SavedScene", 0) != 0)
        {
            gameObject.GetComponent<Button>().interactable = true;        }
        else
        {
            gameObject.GetComponent<Button>().interactable = false;
        }
    }
}
