using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderEnabler : MonoBehaviour
{
    [SerializeField]
    GameObject slider;
    public void enableSlider()
    {
        slider.SetActive(true);
    }
    public void disableSlider() 
    { 
        slider.SetActive(false);
    }
}
