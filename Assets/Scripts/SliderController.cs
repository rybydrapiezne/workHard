using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    [SerializeField]
    Slider slider;
    public bool finished= false;
    public bool increaseSlider(float value)
    {
        slider.value += value;
        return slider.value >= 1;
    }
    public void stopSlider()
    { 
        slider.value = 0;
    }
    public void finalizeInteraction()
    {
        finished = true;
        this.GetComponent<SpriteRenderer>().color = Color.red;
    }
}
