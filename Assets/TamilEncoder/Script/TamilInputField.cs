using UnityEngine;
using UnityEngine.UI;
using TamilEncoder;
using TMPro;

namespace TamilUI
{
    public class TamilInputField : MonoBehaviour
    {
        [SerializeField] InputField inputField;
        [SerializeField] TMP_InputField inputFieldTMP;

        private void Awake()
        {
            if (inputField == null && inputFieldTMP == null)
            {
                if (TryGetComponent(out inputField))
                {
                    return;
                }
                if (TryGetComponent(out inputFieldTMP))
                {
                    return;
                }
                Debug.LogWarning($"In {gameObject.name} Input Field or Input Field TMP component not found");
            }
        }

        public void RenderTextOnValueChanged()
        {
            string unicode = TamilEncoding.Convert
                (TamilFontEncoding.TSCII, TamilFontEncoding.Unicode_ISCII, inputField.text);
            inputField.text = TamilEncoding.Convert(unicode);
        }

        public void RenderTextOnEndEdit()
        {
            inputField.text = TamilEncoding.Convert(inputField.text);
        }

        public void RenderTextOnValueChangedTMP()
        {
            string unicode = TamilEncoding.Convert
                (TamilFontEncoding.TSCII, TamilFontEncoding.Unicode_ISCII, inputFieldTMP.text);
            inputFieldTMP.text = TamilEncoding.Convert(unicode);
        }

        public void RenderTextOnEndEditTMP()
        {
            inputFieldTMP.text = TamilEncoding.Convert(inputFieldTMP.text);
        }
    }
}