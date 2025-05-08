using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCSystem : MonoBehaviour
{
    public Constants.Type affectedType;
    public float workMeter;
    public float workDecreaser;
    public float workStationDecreaser;
    public float kitchenDecreaser;
    public float temperatureDecreaser;
    public float allDecreaser;
    [SerializeField]
    float speed=15f;
    [SerializeField]
    GameObject WorkMeterCanvas;
    [SerializeField]
    Slider slider;
    bool invoked = false;
    public static Action<NPCSystem> onWorkMeterDepleted;

    private void Start()
    {
        SliderController.OnSliderValueChanged += destroyedInteractable;
    }

    private void OnDestroy()
    {
        SliderController.OnSliderValueChanged -= destroyedInteractable;
    }

    
    private void destroyedInteractable(SliderController slider)
    {
        Constants.Type type = slider.type;
        if (affectedType == type || type == Constants.Type.all)
        {
            switch(type)
            {
                case(Constants.Type.all):
                    workMeter-=allDecreaser;
                    break;
                case (Constants.Type.work):
                    workMeter-=workDecreaser;
                    break;
                case (Constants.Type.kitchen):
                    workMeter-=kitchenDecreaser;
                    break;
                case (Constants.Type.temperature):
                    workMeter-=temperatureDecreaser;
                    break;
            }
        }

        else if (type == Constants.Type.workStation)
        {
            if (slider.assignedNpc == this.gameObject)
            {
                workMeter -= workStationDecreaser;
            }
        }
        StartCoroutine(depletingWorkMeter());
        if (workMeter <= 0 && !invoked)
        {
            onWorkMeterDepleted.Invoke(this);
            invoked = true;
        }

    }
    IEnumerator depletingWorkMeter()
    {
        WorkMeterCanvas.SetActive(true);

        while (slider.value > workMeter)
        {
            slider.value = Mathf.MoveTowards(slider.value, workMeter, Time.deltaTime*20);
            yield return null;
        }

        WorkMeterCanvas.SetActive(false);
    }
}