using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using TamilEncoder;
using TMPro;

namespace TamilUI
{
    public class TamilDropdown : MonoBehaviour
    {

        [SerializeField] TMP_Dropdown dropdownTMP;
        [SerializeField] Dropdown dropdown;


        public void Start()
        {
            if (dropdown == null && dropdownTMP == null)
            {
                if (TryGetComponent(out dropdown))
                {
                    UpdateDropdownOptions();
                    return;
                }
                if (TryGetComponent(out dropdownTMP))
                {
                    UpdateDropdownTMPOptions();
                    return;
                }
                Debug.LogWarning($"In {gameObject.name} Dropdown or Dropdown TMP component not found");
                return;
            }

            if (dropdown != null)
            {
                UpdateDropdownOptions();
                return;
            }

            UpdateDropdownTMPOptions();
            return;
        }

        void UpdateDropdownOptions()
        {
            var options = dropdown.options;
            foreach (var option in options)
                option.text = TamilEncoding.Convert(option.text);
				
        }

        void UpdateDropdownTMPOptions()
        {
            var options = dropdownTMP.options;
            foreach (var option in options)
                option.text = TamilEncoding.Convert(option.text);
				
        }
    }
}