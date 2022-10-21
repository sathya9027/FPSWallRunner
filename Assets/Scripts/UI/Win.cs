using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Win : MonoBehaviour
{
    public Button retryButton;
    public Button mainMenuButton;
    public Button quitButton;
    private void Start()
    {
        ButtonEvents();
    }

    private void ButtonEvents()
    {
        retryButton.onClick.AddListener
            (delegate
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                Time.timeScale = 1;
                SceneManager.LoadScene(1);
            });
        mainMenuButton.onClick.AddListener
            (delegate
            {
                Time.timeScale = 1;
                SceneManager.LoadScene(0);
            });
        quitButton.onClick.AddListener
            (delegate
            {
                Application.Quit();
            });
    }
}
