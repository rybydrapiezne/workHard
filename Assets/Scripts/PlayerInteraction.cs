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
        if (collision.tag == "Interactable")
        {
            interact.action.performed += timedInteraction;
            interact.action.canceled += cancelTimedInteraction;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        cancelTimedInteractionByExiting();

    }

    private void cancelTimedInteractionByExiting()
    {
        if (colisionObject != null)
        {
            if (coroutine != null)
                StopCoroutine(coroutine);
            colisionObject.GetComponent<SliderController>().stopSlider();
            colisionObject.GetComponent<SliderEnabler>().disableSlider();
            colisionObject = null;
            interact.action.performed -= timedInteraction;
            interact.action.canceled -= cancelTimedInteraction;
        }
    }

    private void cancelTimedInteraction(InputAction.CallbackContext context)
    {
        if (colisionObject != null)
        {
            colisionObject.GetComponent<SliderEnabler>().disableSlider();
            StopCoroutine(coroutine);
            colisionObject.GetComponent<SliderController>().stopSlider();
        }

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
                interact.action.performed -= timedInteraction;
                interact.action.canceled -= cancelTimedInteraction;
                yield break;
            }
        }

    }
}
