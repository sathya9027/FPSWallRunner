using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TBOB
{
    [CreateAssetMenu(menuName = "SO/Settings")]
    public class SO_Settings : ScriptableObject
    {
        [HideInInspector] public int widthValue;
        [HideInInspector] public int heightValue;
        [HideInInspector] public string displayTypeValue;
        public float brightnessValue;
        public float renderDistanceValue;
        public bool vSyncValue;
        public float masterVolumeValue;
        public float musicVolumeValue;
        public float sfxVolumeValue;
        public int languageValue;
    }
}
