using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamRecoil : MonoBehaviour
{
    public WeaponRecoil[] weaponRecoils;

    public float rotationSpeed = 6f;
    public float returnSpeed = 25f;

    public Vector3 recoilRotation = new Vector3(2f, 2f, 2f);

    public Vector3 recoilRotationAiming = new Vector3(0.5f, 0.5f, 0.5f);

    public bool aiming;

    private Vector3 currentRotation;
    private Vector3 rot;

    private int currentGun = 0;

    private void Update()
    {
        if (!PauseMenu.instance.isPaused)
        {
            if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0))
            {
                Fire();
            }

            if (Input.GetMouseButton(1))
            {
                aiming = true;
            }
            else
            {
                aiming = false;
            }

            for (int i = 0; i < weaponRecoils.Length; i++)
            {
                if (weaponRecoils[i].gameObject.activeInHierarchy)
                {
                    currentGun = i;

                    rotationSpeed = weaponRecoils[currentGun].rotationalRecoilSpeed;
                    returnSpeed = weaponRecoils[currentGun].rotationalReturnSpeed;

                    recoilRotation = weaponRecoils[currentGun].recoilRotation;
                    recoilRotationAiming = weaponRecoils[currentGun].recoilRotationAim;
                }
            }
        }
    }

    private void FixedUpdate()
    {
        currentRotation = Vector3.Lerp(currentRotation, Vector3.zero, returnSpeed * Time.deltaTime);
        rot = Vector3.Slerp(rot, currentRotation, rotationSpeed * Time.fixedDeltaTime);
        transform.localRotation = Quaternion.Euler(rot);
    }

    public void Fire()
    {
        if (aiming)
        {
            currentRotation += new Vector3
                (-recoilRotationAiming.x, 
                Random.Range(-recoilRotationAiming.y, recoilRotationAiming.y), 
                Random.Range(-recoilRotationAiming.z, recoilRotationAiming.z));
        }
        else
        {
            currentRotation += new Vector3
                (-recoilRotation.x,
                Random.Range(-recoilRotation.y, recoilRotation.y),
                Random.Range(-recoilRotation.z, recoilRotation.z));
        }
    }
}
