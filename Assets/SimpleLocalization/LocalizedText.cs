using UnityEngine;
using UnityEngine.UI;
using TamilUI;
using TBOB;

namespace Assets.SimpleLocalization
{
	/// <summary>
	/// Localize text component.
	/// </summary>
    [RequireComponent(typeof(Text))]
    public class LocalizedText : MonoBehaviour
    {
        public string LocalizationKey;

        public Font englishFont;
        public Font hindiFont;

        private Text languageText;

        public void Start()
        {
            languageText = GetComponent<Text>();
            Localize();
            LocalizationManager.LocalizationChanged += Localize;
        }

        public void OnDestroy()
        {
            LocalizationManager.LocalizationChanged -= Localize;
        }

        private void Localize()
        {
            if (LocalizationManager.Language == LanguageType.Hindi.ToString())
            {
                languageText.font = hindiFont;
                languageText.text = LocalizationManager.Localize(LocalizationKey);
            }
            else if (LocalizationManager.Language == LanguageType.Tamil.ToString())
            {
                GetComponent<TamilText>().m_Text = LocalizationManager.Localize(LocalizationKey);
                GetComponent<TamilText>().UpdateText();
            }
            else
            {
                languageText.font = englishFont;
                languageText.text = LocalizationManager.Localize(LocalizationKey);
            }
        }
    }
}