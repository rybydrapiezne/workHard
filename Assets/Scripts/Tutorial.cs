using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private GameObject cam;
    [SerializeField] private GameObject TextBox1;
    [SerializeField] private GameObject TextBox2;
    [SerializeField] private GameObject TextBox3;
    [SerializeField] private GameObject TextBox4;
    [SerializeField] private GameObject TextBox5;
    [SerializeField] private GameObject TextBox6;
    [SerializeField] private GameObject textboxPanel;
    [SerializeField] private GameObject endPoint;

    private bool finishedTyping=false;
    private bool finishedCoroutine=false;
    private int numberOfClicks;
    private TypeWriterEffect actTypeWriter;
    private void Awake()
    {
        Time.timeScale = 0;
    }
    private void Start()
    {
        StartCoroutine(cameraPanner());
        TextBox1.SetActive(true);
        actTypeWriter = TextBox1.GetComponent<TypeWriterEffect>();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

            if (actTypeWriter.finished == true)
            {
                numberOfClicks++;
                switch (numberOfClicks)
                {
                    case 1:
                        TextBox1.SetActive(false);
                        TextBox2.SetActive(true);
                        actTypeWriter = TextBox2.GetComponent<TypeWriterEffect>();
                        break;
                    case 2:
                        TextBox2.SetActive(false);
                        TextBox3.SetActive(true);
                        actTypeWriter = TextBox3.GetComponent<TypeWriterEffect>();
                        break;
                    case 3:
                        TextBox3.SetActive(false);
                        TextBox4.SetActive(true);
                        actTypeWriter = TextBox4.GetComponent<TypeWriterEffect>();
                        break;
                    case 4:
                        TextBox4.SetActive(false);
                        TextBox5.SetActive(true);
                        actTypeWriter = TextBox5.GetComponent<TypeWriterEffect>();
                        break;
                    case 5:
                        TextBox5.SetActive(false);
                        TextBox6.SetActive(true);
                        actTypeWriter = TextBox6.GetComponent<TypeWriterEffect>();
                        break;
                    case 6:
                        textboxPanel.SetActive(false);
                        finishedTyping = true;
                        break;
                }
            }
            else
            {
                actTypeWriter.forceEnd();
            }
        }
        if(finishedTyping && finishedCoroutine)
        {
            Time.timeScale = 1;
            Destroy(gameObject);
        }

    }
IEnumerator cameraPanner()
{
    Vector3 startPosition = cam.transform.position;
    Vector3 endPosition = new Vector3(startPosition.x, endPoint.transform.position.y, startPosition.z);

    float duration = 10f;
    float elapsedTime = 0f;

    while (elapsedTime < duration)
    {
        float t = elapsedTime / duration;
        cam.transform.position = Vector3.Lerp(startPosition, endPosition, t);
        elapsedTime += Time.unscaledDeltaTime;
        yield return null;
    }

    cam.transform.position = endPosition;
    finishedCoroutine = true;

}

}
