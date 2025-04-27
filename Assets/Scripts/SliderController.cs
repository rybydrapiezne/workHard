using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    [SerializeField]
    Slider slider;
    private Animator animator;
    private AudioSource audioSource;
    public static event Action<SliderController> OnSliderValueChanged;
    public bool finished= false;
    public GameObject? assignedNpc;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }
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
        OnSliderValueChanged?.Invoke(this);
        animator.SetBool("Destroyed", true);
        audioSource.Play();
    }
}
