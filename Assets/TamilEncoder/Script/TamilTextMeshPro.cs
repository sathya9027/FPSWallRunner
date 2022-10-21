using UnityEngine;
using TMPro;
using TamilEncoder;

namespace TamilUI
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class TamilTextMeshPro : MonoBehaviour
    {
        [TextArea(3, 10)][SerializeField] public string m_Text = string.Empty;

        [SerializeField] TMP_FontAsset m_FontAsset;

        public TMP_FontAsset TSCII_TMPFont { get => m_FontAsset; set => m_FontAsset = value; }

        public string Text
        {
            get { return m_Text; }
            set
            {
                m_Text = value;
                UpdateText();
            }
        }

        TextMeshProUGUI textMesh;

        protected virtual void Awake()
        {
            textMesh = GetComponent<TextMeshProUGUI>();
        }

        protected virtual void Start()
        {
            UpdateText();
        }

        public void UpdateText()
        {
            if (string.IsNullOrEmpty(m_Text))
                return;

            textMesh = GetComponent<TextMeshProUGUI>();
            textMesh.SetText(TamilEncoding.Convert(m_Text));

            if (textMesh.font != m_FontAsset)
                textMesh.font = m_FontAsset;
        }
    }
}