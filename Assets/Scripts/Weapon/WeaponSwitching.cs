using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{
    public Transform[] weapons;
    public KeyCode[] keys;
    public float switchTimer;

    private int selectedWeapon;
    private float timeSinceLastSwitch;

    private void Start()
    {
        SetWeapons();
        Select(selectedWeapon);
    }

    private void Update()
    {
        if (!PauseMenu.instance.isPaused)
        {

            int previousSelectedWeapon = selectedWeapon;

            for (int i = 0; i < keys.Length; i++)
            {
                if (Input.GetKeyDown(keys[i]) && timeSinceLastSwitch >= switchTimer)
                {
                    selectedWeapon = i;
                }
            }

            if (previousSelectedWeapon != selectedWeapon)
            {
                Select(selectedWeapon);
            }

            timeSinceLastSwitch += Time.deltaTime;
        }
    }

    private void Select(int weaponIndex)
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].gameObject.SetActive(i == weaponIndex);
        }

        timeSinceLastSwitch = 0f;

        OnWeaponSelected();
    }

    private void OnWeaponSelected()
    {
        Debug.Log("Weapon Selected");
    }

    private void SetWeapons()
    {
        if (keys == null)
        {
            keys = new KeyCode[weapons.Length];
        }
    }
}
