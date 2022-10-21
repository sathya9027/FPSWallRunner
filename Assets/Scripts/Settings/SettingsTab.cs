using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TBOB
{
    public class SettingsTab : MonoBehaviour
    {
        [Header("Buttons")]
        public Button videoButton;
        public Button gameButton;
        public Button interfaceButton;
        public Button audioButton;

        [Header("Tabs")]
        public GameObject videoTab;
        public GameObject gameTab;
        public GameObject interfaceTab;
        public GameObject audioTab;

        private void Start()
        {
            ButtonEvents();
        }

        private void ButtonEvents()
        {
            CloseAllTabs();
            videoButton.onClick.AddListener
                (delegate
                {
                    CloseAllTabs();
                    videoTab.SetActive(true);
                });
            gameButton.onClick.AddListener
                (delegate
                {
                    CloseAllTabs();
                    gameTab.SetActive(true);
                });
            interfaceButton.onClick.AddListener
                (delegate
                {
                    CloseAllTabs();
                    interfaceTab.SetActive(true);
                });
            audioButton.onClick.AddListener
                (delegate
                {
                    CloseAllTabs();
                    audioTab.SetActive(true);
                });
        }

        private void CloseAllTabs()
        {
            videoTab.SetActive(false);
            gameTab.SetActive(false);
            interfaceTab.SetActive(false);
            audioTab.SetActive(false);
        }
    }
}
