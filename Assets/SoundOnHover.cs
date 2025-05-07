using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SoundOnHover : MonoBehaviour, IPointerEnterHandler
{
    public AudioSource audioSource;
    public AudioClip clip;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (audioSource != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}