using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeChoice : MonoBehaviour
{
    [SerializeField]
    GameObject choiceCanvas;
    public static Action<UpgradeChoice> onOption1Selected;
    public static Action<UpgradeChoice> onOption2Selected;
    public static Action<UpgradeChoice> onOption3Selected;
    public float visionDecrease = 2.0f;
    public float destroyingSpeedIncrease=1.0f;
    public float sliderDecreaseSpeedDecreaser = 0.2f;
    private void Start()
    {
        Time.timeScale = 0;
    }
    public void option1()
    {
        onOption1Selected?.Invoke(this);
        cleanUp();
    }
    public void option2()
    {
        onOption2Selected?.Invoke(this);
        cleanUp();
    }
    public void option3()
    {
        onOption3Selected?.Invoke(this);
        cleanUp();
    }
    private void cleanUp()
    {
        Time.timeScale = 1;
        choiceCanvas.SetActive(false);
        this.enabled = false;
    }
}
