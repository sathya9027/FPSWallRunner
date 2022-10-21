using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering.PostProcessing;
using ShadowQuality = UnityEngine.ShadowQuality;
using ShadowResolution = UnityEngine.ShadowResolution;
using Assets.SimpleLocalization;

namespace TBOB
{
    public enum AudioMixerEnum
    {
        Master,
        Music,
        SFX
    }

    public class Settings : MonoBehaviour
    {
        public SO_Settings settings;
        public SO_Settings defaultSettings;
        public SO_GameObject cameraGameObject;

        public AudioClip onClickSFX;
        public AudioClip onPointerEnterSFX;

        public AudioSource sfxAudioSource;
        public AudioSource musicAudioSource;

        public Volume volume;

        public GameObject resPopUp;

        public AudioMixer audioMixer;

        public Button save;
        public Button apply;
        public Button revert;

        public SliderSettings brightnessSlider;
        public SliderSettings renderDistanceSlider;
        public SliderSettings masterAudioSlider;
        public SliderSettings musicAudioSlider;
        public SliderSettings sfxAudioSlider;

        public TextMeshProUGUI textCountDown;
        public TextMeshProUGUI settingsTitle;
        public TextMeshProUGUI settingsDescription;
        public TextMeshProUGUI menuTitle;
        public TextMeshProUGUI menuDescription;

        public TMP_Dropdown ddVideoResolution;
        public TMP_Dropdown ddDisplay;
        public TMP_Dropdown ddLanguage;

        public Toggle toggleVSync;

        private List<Resolution> storeResolution;
        private FullScreenMode screenMode;

        private Camera cam;

        private int prevScreenModeIndex;
        private int prevResolutionIndex;
        private int maximumPopUpTimer = 15;
        private int resWidthMax = 600;
        private int resHeightMax = 600;
        private ColorAdjustments colorAdjustments;

        #region Resolution & Display
        private void AddResolution(Resolution[] res)
        {
            int countRes = 0;
            for (int i = 0; i < res.Length; i++)
            {
                if (res[i].width > resWidthMax && res[i].height > resHeightMax)
                {
                    if (Screen.currentResolution.refreshRate>=59 && Screen.currentResolution.refreshRate<=60 && res[i].refreshRate >= 59 && res[i].refreshRate <= 60)
                    {
                        if (countRes == 0)
                        {
                            storeResolution.Add(res[i]);
                            countRes++;
                        }
                        else if ((res[i].width != storeResolution[countRes - 1].width) && res[i].height != storeResolution[countRes - 1].height)
                        {
                            storeResolution.Add(res[i]);
                            countRes++;
                        }
                    }
                    else if (res[i].refreshRate == Screen.currentResolution.refreshRate)
                    {
                        storeResolution.Add(res[i]);
                        countRes++;
                    }
                }
            }

            for (int i = 0; i < countRes; i++)
            {
                ddVideoResolution.options.Add(new TMP_Dropdown.OptionData(ResolutionToString(storeResolution[i])));
            }
        }

        private string ResolutionToString(Resolution resolution)
        {
            return resolution.width + " x " + resolution.height;
        }

        private void ScreenOptions(string mode)
        {
            if (mode == "Exclusive Fullscreen")
            {
                ddDisplay.value = 0;
                screenMode = FullScreenMode.ExclusiveFullScreen;
            }
            else if (mode == "Windowed")
            {
                ddDisplay.value = 1;
                screenMode = FullScreenMode.Windowed;
            }
            else
            {
                ddDisplay.value = 2;
                screenMode = FullScreenMode.FullScreenWindow;
            }

            settings.displayTypeValue = mode;
            ddDisplay.RefreshShownValue();
        }
        #endregion

        private void InitializeBeforeLoad()
        {
            if (volume.profile.TryGet<ColorAdjustments>(out ColorAdjustments tmp))
            {
                colorAdjustments = tmp;
            }

            Resolution[] resolutions = Screen.resolutions;
            Array.Reverse(resolutions);
            storeResolution = new List<Resolution>();

            AddResolution(resolutions);

            cam = cameraGameObject.gameObject.GetComponent<Camera>();
        }

        private void InitializeAfterLoad()
        {
            resPopUp.SetActive(false);
            prevResolutionIndex = ddVideoResolution.value;
            prevScreenModeIndex = ddDisplay.value;
        }

        private void ButtonEvents()
        {
            ddDisplay.onValueChanged.AddListener
                (delegate
                {
                    PopUpHandler
                    (ddDisplay.value,
                    ddDisplay);
                });
            ddVideoResolution.onValueChanged.AddListener
                (delegate
                {
                    PopUpHandler
                    (ddVideoResolution.value,
                    ddVideoResolution);
                });
            brightnessSlider.slider.onValueChanged.AddListener(
                delegate
                {
                    Brightness(brightnessSlider.slider.value);
                });
            renderDistanceSlider.slider.onValueChanged.AddListener(
                delegate
                {
                    RenderDistance(renderDistanceSlider.slider.value);
                });
            toggleVSync.onValueChanged.AddListener
                (delegate
                {
                    VerticalSync(toggleVSync.isOn);
                });
            ddLanguage.onValueChanged.AddListener(
                delegate
                {
                    Language(ddLanguage.value);
                });
            masterAudioSlider.slider.onValueChanged.AddListener
                 (delegate
                 {
                     AudioVolume(masterAudioSlider.slider.value, AudioMixerEnum.Master);
                 });
            musicAudioSlider.slider.onValueChanged.AddListener
                (delegate
                {
                    AudioVolume(musicAudioSlider.slider.value, AudioMixerEnum.Music);
                });
            sfxAudioSlider.slider.onValueChanged.AddListener
                (delegate
                {
                    AudioVolume(sfxAudioSlider.slider.value, AudioMixerEnum.SFX);
                });
            save.onClick.AddListener(
                delegate
                {
                    SaveAndLoadSettings.SaveSettings(settings);
                });
        }

        private void Language(int value)
        {
            switch (value)
            {
                case 0:
                    LocalizationManager.Language = LanguageType.English.ToString();
                    break;
                case 1:
                    LocalizationManager.Language = LanguageType.Tamil.ToString();
                    break;
                case 2:
                    LocalizationManager.Language = LanguageType.Hindi.ToString();
                    break;
                default:
                    break;
            }
            ddLanguage.SetValueWithoutNotify(value);
            settings.languageValue = value;
        }

        private void VerticalSync(bool isOn)
        {
            if (isOn)
            {
                QualitySettings.vSyncCount = 1;
            }
            else
            {
                QualitySettings.vSyncCount = 0;
            }

            toggleVSync.SetIsOnWithoutNotify(isOn);
            settings.vSyncValue = isOn;
        }

        private void RenderDistance(float value)
        {
            renderDistanceSlider.slider.value = value;
            float finalValue = SliderConversion(renderDistanceSlider, value);
            cam.farClipPlane = finalValue;
            settings.renderDistanceValue = value;
        }

        private void AudioVolume(float value, AudioMixerEnum audioMixerEnum)
        {
            float finalValue;
            switch (audioMixerEnum)
            {
                case AudioMixerEnum.Master:
                    masterAudioSlider.slider.value = value;
                    finalValue = SliderConversion(masterAudioSlider, value);
                    audioMixer.SetFloat(audioMixerEnum.ToString(), finalValue);
                    settings.masterVolumeValue = value;
                    break;
                case AudioMixerEnum.Music:
                    musicAudioSlider.slider.value = value;
                    finalValue = SliderConversion(musicAudioSlider, value);
                    audioMixer.SetFloat(audioMixerEnum.ToString(), finalValue);
                    settings.musicVolumeValue = value;
                    break;
                case AudioMixerEnum.SFX:
                    sfxAudioSlider.slider.value = value;
                    finalValue = SliderConversion(sfxAudioSlider, value);
                    audioMixer.SetFloat(audioMixerEnum.ToString(), finalValue);
                    settings.sfxVolumeValue = value;
                    break;
                default:
                    break;
            }
        }

        private void Start()
        {
            InitializeBeforeLoad();
            LoadSettings();
            InitializeAfterLoad();

            ButtonEvents();
        }

        private void Brightness(float value)
        {
            brightnessSlider.slider.value = value;
            float finalValue = SliderConversion(brightnessSlider, value);
            colorAdjustments.postExposure.Override(finalValue);
            settings.brightnessValue = value;
        }

        private void PopUpHandler(int value, TMP_Dropdown display)
        {
            if (!resPopUp.activeSelf)
            {

                resPopUp.SetActive(true);
                apply.onClick.RemoveAllListeners();
                revert.onClick.RemoveAllListeners();

                if (display == ddVideoResolution)
                {
                    SetResolution(value);
                    apply.onClick.AddListener(delegate
                    {
                        Apply(display,
                            value);
                    });
                    revert.onClick.AddListener(delegate
                    {
                        Revert(display,
                            prevResolutionIndex);
                    });
                }
                else
                {
                    SetDisplay(value);
                    apply.onClick.AddListener(delegate
                    {
                        Apply(display,
                            value);
                    });

                    revert.onClick.AddListener(delegate
                    {
                        Revert(display,
                               prevScreenModeIndex);
                    });
                }
                StartCoroutine(Timer(display));
            }
        }

        private void LoadSettings()
        {
            LoadSettingsFile.settings = settings;
            LoadSettingsFile.defaultSettings = defaultSettings;
            LoadSettingsFile.InitialeSettings();

            for (int i = 0; i < storeResolution.Count; i++)
            {
                if (storeResolution[i].width == settings.widthValue && storeResolution[i].height == settings.heightValue)
                {
                    ddVideoResolution.value = i;
                }
            }

            ScreenOptions(settings.displayTypeValue);
            Screen.SetResolution(settings.widthValue, settings.heightValue, screenMode);

            Brightness(settings.brightnessValue);
            RenderDistance(settings.renderDistanceValue);
            VerticalSync(settings.vSyncValue);

            AudioVolume(settings.masterVolumeValue, AudioMixerEnum.Master);
            AudioVolume(settings.musicVolumeValue, AudioMixerEnum.Music);
            AudioVolume(settings.sfxVolumeValue, AudioMixerEnum.SFX);

            Language(settings.languageValue);
        }

        private void Apply(TMP_Dropdown drop, int newIndex)
        {
            if (drop == ddVideoResolution)
            {
                prevResolutionIndex = newIndex;
            }
            else
            {
                prevScreenModeIndex = newIndex;
            }
            ClosePopUp();
        }
        private void Revert(TMP_Dropdown drop, int newIndex)
        {
            if (drop == ddVideoResolution)
            {
                SetResolution(newIndex);
            }
            else
            {
                SetDisplay(newIndex);
            }
            ClosePopUp();
        }

        private IEnumerator Timer(TMP_Dropdown display)
        {
            int currentTimer = maximumPopUpTimer;
            while (currentTimer >= 0)
            {
                textCountDown.text = currentTimer.ToString();
                yield return new WaitForSeconds(1);
                currentTimer--;

                if (currentTimer < 0)
                {
                    if (display == ddVideoResolution)
                    {
                        SetResolution(prevResolutionIndex);
                        ClosePopUp();   
                    }
                    else
                    {
                        SetDisplay(prevScreenModeIndex);
                        ClosePopUp();
                    }
                }
            }
        }

        private void SetDisplay(int value)
        {
            ScreenOptions(ddDisplay.options[value].text);
            StartCoroutine(SetDisplayAtEnd());
            SaveAndLoadSettings.SaveSettings(settings);
        }

        private IEnumerator SetDisplayAtEnd()
        {
            yield return new WaitForFixedUpdate();
            Screen.SetResolution(settings.widthValue, settings.heightValue, screenMode);
        }

        private void ClosePopUp()
        {
            resPopUp.SetActive(false);
            StopAllCoroutines();
        }

        private void SetResolution(int value)
        {
            Screen.SetResolution(storeResolution[value].width, storeResolution[value].height, screenMode);

            settings.widthValue = storeResolution[value].width;
            settings.heightValue = storeResolution[value].height;
            ddVideoResolution.value = value;
            ddVideoResolution.RefreshShownValue();

            SaveAndLoadSettings.SaveSettings(settings);
        }

        #region Helper Function

        private float ConvertValue(float minValue, float maxValue, float minSettingsValue, float maxSettingsValue, float value)
        {
            float ratio = (maxSettingsValue - minSettingsValue) / (maxValue - minValue);
            float returnValue = ((value * ratio) - (minValue * ratio)) + minSettingsValue;
            return returnValue;
        }

        private float SliderConversion(SliderSettings set, float currentValue)
        {
            float conversion = ConvertValue
                (set.slider.minValue,
                set.slider.maxValue,
                set.minSettingsValue,
                set.maxSettingsValue,
                currentValue);

            set.textSlider.text = Mathf.RoundToInt(currentValue).ToString();
            return conversion;
        }

        #endregion
    }
}
