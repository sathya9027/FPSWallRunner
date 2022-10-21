using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu instance;

    public Button resumeButton;
    public Button mainMenuButton;
    public Button settingsButton;
    public Button quitButton;
    public Button backToPauseMenuButton;

    public GameObject pauseMenu;
    public GameObject settingsMenu;
    public GameObject inGameUI;

    public bool isPaused;

    private void Awake()
    {
        instance = this;
        settingsMenu.SetActive(false);
        pauseMenu.SetActive(false);
    }

    private void Start()
    {
        ButtonEvents();
    }

    private void ButtonEvents()
    {
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(false);
        mainMenuButton.onClick.AddListener
            (delegate
            {
                SceneManager.LoadScene(0);
            });
        resumeButton.onClick.AddListener
            (delegate
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                pauseMenu.SetActive(false);
                inGameUI.SetActive(true);
                isPaused = false;
                Time.timeScale = 1;
            });
        settingsButton.onClick.AddListener
            (delegate
            {
                settingsMenu.SetActive(true);
                pauseMenu.SetActive(false);
            });
        quitButton.onClick.AddListener
            (delegate
            {
                Application.Quit();
            });
        backToPauseMenuButton.onClick.AddListener
            (delegate
            {
                settingsMenu.SetActive(false);
                pauseMenu.SetActive(true);
            });
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !settingsMenu.activeSelf)
        {
            if (pauseMenu.activeSelf)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                pauseMenu.SetActive(false);
                inGameUI.SetActive(true);
                isPaused = false;
                Time.timeScale = 1;
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                pauseMenu.SetActive(true);
                inGameUI.SetActive(false);
                isPaused = true;
                Time.timeScale = 0;
            }
        }
    }
}
