using UnityEngine;
using UnityEngine.UI;
using TamilEncoder;

namespace TamilUI
{
    [RequireComponent(typeof(Text))]
    public class TamilText : MonoBehaviour
    {
        [TextArea(3, 10)][SerializeField] public string m_Text = defaultText;
        [SerializeField] Font m_TSCII_TamilFont;

        public Font TSCII_TamilFont { get => m_TSCII_TamilFont; set => m_TSCII_TamilFont = value; }

        [SerializeField] int fontSize = 0;
        [SerializeField] float lineSpacing = 1;

        const string defaultText = "Your Tamil Text Here...";

        public string Text
        {
            get
            {
                return m_Text;
            }
            set
            {
                m_Text = TamilEncoding.Convert(value);
                UpdateText();
            }
        }

        Text attachedText;
        //string Encoded_TSCII;

        protected virtual void Awake()
        {
            attachedText = GetComponent<Text>();
            if (m_Text == defaultText)
                m_Text = null;
        }

        protected virtual void Start()
        {
            if (string.IsNullOrEmpty(m_Text))
                return;

            UpdateText();
        }

        public void UpdateText()
        {
            attachedText.text = TamilEncoding.Convert(m_Text);
            attachedText.font = TSCII_TamilFont;
            attachedText.lineSpacing = lineSpacing;

            if (fontSize == 0)
                return;

            attachedText.fontSize = fontSize;
            if (attachedText.resizeTextForBestFit)
                attachedText.resizeTextMaxSize = fontSize;
        }


        public void RenderText()
        {
            if (string.IsNullOrEmpty(m_Text))
                return;

            attachedText = GetComponent<Text>();
            attachedText.text = TamilEncoding.Convert(m_Text);

            if (attachedText.font != m_TSCII_TamilFont)
                attachedText.font = m_TSCII_TamilFont;
        }
    }
}
