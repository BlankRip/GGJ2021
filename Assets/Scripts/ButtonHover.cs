using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    [SerializeField] GameObject effectToPlay;
    [SerializeField] AudioClip hoverAudio;
    [SerializeField] AudioClip clickAudio;

    AudioSource audioSource;

    Button button;

    private void Start()
    {
        button = GetComponent<Button>();
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        if (button && button.interactable)
        {
            if (effectToPlay != null)
            {
                effectToPlay.transform.parent = transform;
                effectToPlay.transform.SetAsFirstSibling();
                effectToPlay.transform.position = transform.position;
                effectToPlay.SetActive(true);
            }
            audioSource.PlayOneShot(hoverAudio);
        }
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        if (button && button.interactable && effectToPlay != null)
            effectToPlay.SetActive(false);
    }

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        if (button.interactable)
        {
            audioSource.PlayOneShot(clickAudio);
            if (effectToPlay != null)
                effectToPlay.SetActive(false);
        }
    }
}