using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TBOB
{
    public static class LoadSettingsFile
    {
        public static SO_Settings settings;
        public static SO_Settings defaultSettings;

        public static void InitialeSettings()
        {
            SettingsFile data = SaveAndLoadSettings.LoadSettings();

            if (data != null)
            {
                settings.widthValue = data.screenWidth;
                settings.heightValue = data.screenHeight;

                settings.displayTypeValue = data.displayType;
                settings.brightnessValue = data.brightness;
                settings.renderDistanceValue = data.renderDistance;
                settings.vSyncValue = data.vSync;
                settings.masterVolumeValue = data.masterVolume;
                settings.musicVolumeValue = data.musicVolume;
                settings.sfxVolumeValue = data.sfxVolume;

                settings.languageValue = data.language;
            }
            else
            {
                settings.widthValue = Screen.width;
                settings.heightValue = Screen.height;
                settings.displayTypeValue = Screen.fullScreenMode.ToString();
                settings.brightnessValue = defaultSettings.brightnessValue;
                settings.renderDistanceValue = defaultSettings.renderDistanceValue;
                settings.vSyncValue = defaultSettings.vSyncValue;
                settings.masterVolumeValue = defaultSettings.masterVolumeValue;
                settings.musicVolumeValue = defaultSettings.musicVolumeValue;
                settings.sfxVolumeValue = defaultSettings.sfxVolumeValue;
                settings.languageValue = defaultSettings.languageValue;
                SaveAndLoadSettings.SaveSettings(settings);
            }
        }
    }
}
