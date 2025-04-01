using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{    
    public bool fired { private set; get; } = false;

    [SerializeField]
    float movementSpeed = 1f;
    [SerializeField]
    InputActionReference movement, sprint;
    [SerializeField]
    Slider slider;
    private Rigidbody2D rb;
    private Vector2 direction;
    private float baseSpeed;
    private float speed=0.5f;
    private float recoverySpeed = 0.1f;
    private bool recovered = true;
    private void Start()
    {
        rb= GetComponent<Rigidbody2D>();
        baseSpeed=movementSpeed;
    }
    private void Update()
    {
        direction= movement.action.ReadValue<Vector2>();
        if(sprint.action.IsPressed() && slider.value>0 && recovered)
        {
            movementSpeed = baseSpeed * 2;
            slider.value -= speed*Time.deltaTime;
        }
        else
        {

            StartCoroutine(SliderRecovery());
            movementSpeed=baseSpeed;
        }
        if(slider.value<=0)
        {
            recovered = false;
        }

    }
    private void FixedUpdate()
    {
        rb.velocity=direction*movementSpeed;
    }
    public void OnSprint()
    {

    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag=="Boss")
        {
            fired= true;
        }
    }
    IEnumerator SliderRecovery()
    {
        yield return new WaitForSeconds(1);
        slider.value += recoverySpeed * Time.deltaTime;
        if(slider.value>=1)
        {
            recovered = true;
        }
    }
}
