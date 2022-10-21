using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button playButton;
    public Button settingsButton;
    public Button mainMenuButton;
    public Button tutorialButton;
    public Button quitButton;

    public GameObject settingsMenu;
    public GameObject mainMenu;

    private void ButtonEvent()
    {
        settingsButton.onClick.AddListener
            (delegate
            {
                settingsMenu.SetActive(true);
                mainMenu.SetActive(false);
            });
        mainMenuButton.onClick.AddListener
            (delegate
            {
                settingsMenu.SetActive(false);
                mainMenu.SetActive(true);
            });
        tutorialButton.onClick.AddListener
            (delegate
            {
                SceneManager.LoadScene(1);
            });
        quitButton.onClick.AddListener
            (delegate
            {
                Application.Quit();
            });
        playButton.onClick.AddListener
            (delegate
            {
                SceneManager.LoadScene(1);
            });
    }

    private void InitializeBeforeLoad()
    {
        settingsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    private void Start()
    {
        InitializeBeforeLoad();
        ButtonEvent();
    }
}
