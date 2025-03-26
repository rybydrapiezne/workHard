using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField]
    InputActionReference interact;
    Coroutine coroutine;
    GameObject colisionObject;
    float interactionSpeed = 1.0f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        colisionObject = collision.gameObject;
        interact.action.performed += timedInteraction;
        interact.action.canceled += cancelTimedInteraction;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        cancelTimedInteractionByExiting();

    }

    private void cancelTimedInteractionByExiting()
    {
        StopCoroutine(coroutine);
        colisionObject.GetComponent<SliderController>().stopSlider();
        colisionObject.GetComponent<SliderEnabler>().disableSlider();
        colisionObject = null;
    }

    private void cancelTimedInteraction(InputAction.CallbackContext context)
    {
        colisionObject.GetComponent<SliderEnabler>().disableSlider();
        StopCoroutine(coroutine);
        colisionObject.GetComponent<SliderController>().stopSlider();

    }

    private void timedInteraction(InputAction.CallbackContext context)
    {
        if (!colisionObject.GetComponent<SliderController>().finished)
        {
            colisionObject.GetComponent<SliderEnabler>().enableSlider();
            coroutine = StartCoroutine(interacted(colisionObject.GetComponent<SliderController>()));
        }
    }
    IEnumerator interacted(SliderController sliderController)
    {
        for (; ; )
        {
            if (!sliderController.increaseSlider(interactionSpeed * Time.deltaTime))
            {
                yield return null;
            }
            else
            {
                sliderController.finalizeInteraction();
                sliderController.enabled = false;
                yield break;
            }
        }

    }
}
