using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour, IDamageable
{
    public GameObject dynamite;
    public GameObject chestTop;

    public void PlaceDynamite()
    {
        dynamite.SetActive(true);
        StartCoroutine(BlastDynamite());
    }

    public void Damage(float damage)
    {
        throw new System.NotImplementedException();
    }

    private IEnumerator BlastDynamite()
    {
        yield return new WaitForSeconds(3);
        chestTop.transform.rotation = Quaternion.Euler(32.675f, 90, -90);
        Destroy(dynamite);
        yield return new WaitForSeconds(1);
        PlayerUI.instance.win.SetActive(true);
        Time.timeScale = 1;
        PauseMenu.instance.isPaused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
