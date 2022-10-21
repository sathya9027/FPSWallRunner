using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace TBOB
{
    [System.Serializable]
    public class SliderSettings
    {
        public Slider slider;
        public TextMeshProUGUI textSlider;
        public float maxSettingsValue;
        public float minSettingsValue;
    }
}
