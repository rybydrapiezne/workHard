using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSystem : MonoBehaviour
{
    [SerializeField]
    Constants.Type affectedType;
    [SerializeField]
    float workMeter;
    [SerializeField]
    float workDecreaser;
    [SerializeField]
    float workStationDecreaser;

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
            workMeter -= workDecreaser;
        }

        else if (type == Constants.Type.work)
        {
            if (slider.assignedNpc == this.gameObject)
            {
                workMeter -= workStationDecreaser;
            }
        }

        if (workMeter <= 0)
        {
            onWorkMeterDepleted.Invoke(this);
        }

    }
}