using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonSounds : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    [SerializeField]
    public AudioClip buttonEnter;
    public AudioClip buttonClick;
    public void OnPointerClick(PointerEventData eventData)
    {
        Managers.Audio.PlaySound(buttonClick);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Managers.Audio.PlaySound(buttonEnter);
    }
}
