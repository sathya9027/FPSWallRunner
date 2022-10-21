using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    public static PlayerUI instance;

    public TextMeshPro ammoText;
    public TextMeshPro timerText;
    public Image healthCircle;
    public GameObject uiHolder;

    public GameObject interactText;

    public GameObject win;
    public GameObject loss;

    public int health = 100;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        win.SetActive(false);
        loss.SetActive(false);
    }

    private void Update()
    {
        var timeToDisplay = System.TimeSpan.FromSeconds(Time.time);

        timerText.text = timeToDisplay.Minutes.ToString("00") + ":" + timeToDisplay.Seconds.ToString("00") + ":" + timeToDisplay.Milliseconds.ToString("00");

        uiHolder.SetActive(!PauseMenu.instance.isPaused);

        if (health <= 0)
        {
            loss.SetActive(true);
            Time.timeScale = 0;
            PauseMenu.instance.isPaused = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void DamagePlayer()
    {
        health -= 10;
        healthCircle.fillAmount -= 0.1f;
    }
}
