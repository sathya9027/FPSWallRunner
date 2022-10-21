using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerShoot : MonoBehaviour
{
    public static Action shootInput;
    public static Action reloadInput;

    public Image crosshair;
    public float minSize;
    public float maxSize;
    public float sizeToIncement;

    private Vector3 currentSize = Vector3.one;

    private void Update()
    {
        if (!PauseMenu.instance.isPaused)
        {
            if (Input.GetMouseButton(0))
            {
                shootInput?.Invoke();
                if (currentSize.x < maxSize)
                {
                    currentSize += sizeToIncement * Time.deltaTime * Vector3.one;
                    crosshair.rectTransform.localScale = currentSize;
                }
            }
            else
            {
                if (currentSize.x > minSize)
                {
                    currentSize -= sizeToIncement * Time.deltaTime * Vector3.one;
                    crosshair.rectTransform.localScale = currentSize;
                }
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                reloadInput?.Invoke();
            }
        }
    }
}
