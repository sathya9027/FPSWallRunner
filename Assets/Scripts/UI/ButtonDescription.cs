using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TBOB;

public class ButtonDescription : MonoBehaviour
{
    public string title;
    [TextArea(10, 20)]
    public string description;

    private Settings settings;

    private void Start()
    {
        InitializeBeforeLoad();
        SetupEventTrigger();
    }

    private void InitializeBeforeLoad()
    {
        settings = FindObjectOfType<Settings>();
    }

    private void SetupEventTrigger()
    {
        EventTrigger eventTrigger = GetComponent<EventTrigger>();

        EventTrigger.Entry pointerEnter = new EventTrigger.Entry();
        pointerEnter.eventID = EventTriggerType.PointerEnter;
        pointerEnter.callback.AddListener((data) => { DisplayOn((PointerEventData)data); });
        eventTrigger.triggers.Add(pointerEnter);

        EventTrigger.Entry pointerExit = new EventTrigger.Entry();
        pointerExit.eventID = EventTriggerType.PointerExit;
        pointerExit.callback.AddListener((data) => { DisplayOff((PointerEventData)data); });
        eventTrigger.triggers.Add(pointerExit);

        EventTrigger.Entry pointerClick = new EventTrigger.Entry();
        pointerClick.eventID = EventTriggerType.PointerClick;
        pointerClick.callback.AddListener((data) => { PressButton((PointerEventData)data); });
        eventTrigger.triggers.Add(pointerClick);
    }
    private void DisplayOn(PointerEventData data)
    {
        settings.sfxAudioSource.clip = settings.onPointerEnterSFX;
        settings.sfxAudioSource.Play();
        settings.menuTitle.text = title;
        settings.menuDescription.text = description;
    }

    private void DisplayOff(PointerEventData data)
    {
        settings.menuTitle.text = "";
        settings.menuDescription.text = "";
    }

    private void PressButton(PointerEventData data)
    {
        settings.sfxAudioSource.clip = settings.onClickSFX;
        settings.sfxAudioSource.Play();
    }
}
