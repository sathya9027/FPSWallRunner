using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Assets.SimpleLocalization;

namespace TBOB
{
    public enum LanguageType
    {
        English,
        Tamil,
        Hindi
    }

    public class MultiLanguage : MonoBehaviour
    {
        public TMP_Dropdown languageDropdown;

        private void Awake()
        {
            LocalizationManager.Read();
            languageDropdown.onValueChanged.AddListener
                (delegate
                {
                    SetLanguage(languageDropdown.value);
                });
        }

        public void SetLanguage(int value)
        {
            LanguageType languageType = (LanguageType)value;
            LocalizationManager.Language = languageType.ToString();
        }
    }
}
