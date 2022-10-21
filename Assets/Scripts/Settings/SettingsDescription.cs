using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TBOB
{
    public class SettingsDescription : MonoBehaviour
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
        }

        private void DisplayOn(PointerEventData data)
        {
            settings.sfxAudioSource.clip = settings.onPointerEnterSFX;
            settings.sfxAudioSource.Play();
            settings.settingsTitle.text = title;
            settings.settingsDescription.text = description;
        }

        private void DisplayOff(PointerEventData data)
        {
            settings.settingsTitle.text = "";
            settings.settingsDescription.text = "";
        }
    }
}
