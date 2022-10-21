using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TBOB
{
    [Serializable]
    public class SettingsFile
    {
        public int screenWidth;
        public int screenHeight;
        public string displayType;
        public float brightness;
        public float renderDistance;
        public bool vSync;
        public float masterVolume;
        public float musicVolume;
        public float sfxVolume;
        public int language;

        public SettingsFile(SO_Settings set)
        {
            screenWidth = set.widthValue;
            screenHeight = set.heightValue;

            displayType = set.displayTypeValue;
            brightness = set.brightnessValue;
            renderDistance = set.renderDistanceValue;
            vSync = set.vSyncValue;

            masterVolume = set.masterVolumeValue;
            musicVolume = set.musicVolumeValue;
            sfxVolume = set.sfxVolumeValue;

            language = set.languageValue;
        }
    }
}
