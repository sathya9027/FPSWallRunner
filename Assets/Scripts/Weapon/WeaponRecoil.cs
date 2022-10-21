using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRecoil : MonoBehaviour
{
    public Transform recoilPosition;
    public Transform rotationPoint;

    public float positionalRecoilSpeed = 8f;
    public float rotationalRecoilSpeed = 8f;

    public float positionalReturnSpeed = 18f;
    public float rotationalReturnSpeed = 38f;

    public Vector3 recoilRotation = new Vector3(10f, 5f, 7f);
    public Vector3 recoilKickBack = new Vector3(0.015f, 0f, -0.2f);

    public Vector3 recoilRotationAim = new Vector3(10f, 4f, 6f);
    public Vector3 recoilKickBackAim = new Vector3(0.015f, 0f, -0.2f);

    public Gun gun;

    private Vector3 rotationalRecoil;
    private Vector3 positionalRecoil;
    private Vector3 rot;

    public bool aiming;

    private void Update()
    {
        if (!PauseMenu.instance.isPaused)
        {

            if (gun.gunData.isAutomatic && Input.GetMouseButton(0) && gun.gunData.currentAmmo > 0)
            {
                Fire();
            }

            if (!gun.gunData.isAutomatic && Input.GetMouseButtonDown(0) && gun.gunData.currentAmmo > 0)
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
        }
    }

    private void FixedUpdate()
    {
        rotationalRecoil = Vector3.Lerp(rotationalRecoil, Vector3.zero, rotationalReturnSpeed * Time.deltaTime);
        positionalRecoil = Vector3.Lerp(positionalRecoil, Vector3.zero, positionalReturnSpeed * Time.deltaTime);

        recoilPosition.localPosition = Vector3.Slerp(recoilPosition.localPosition, positionalRecoil, positionalRecoilSpeed * Time.fixedDeltaTime);
        rot = Vector3.Slerp(rot, rotationalRecoil, rotationalRecoilSpeed * Time.fixedDeltaTime);
        rotationPoint.localRotation = Quaternion.Euler(rot);
    }

    private void Fire()
    {
        if (aiming)
        {
            rotationalRecoil += new Vector3
                (-recoilRotationAim.x, 
                Random.Range(-recoilRotationAim.y, recoilRotationAim.y), 
                Random.Range(-recoilRotationAim.z, recoilRotationAim.z));
            positionalRecoil += new Vector3
                (Random.Range(-recoilKickBackAim.x, recoilKickBackAim.x), 
                Random.Range(-recoilKickBackAim.y, recoilKickBackAim.y), 
                recoilKickBackAim.z);
        }
        else
        {
            rotationalRecoil += new Vector3
                (-rotationalRecoil.x,
                Random.Range(-rotationalRecoil.y, rotationalRecoil.y),
                Random.Range(-rotationalRecoil.z, rotationalRecoil.z));
            positionalRecoil += new Vector3
                (Random.Range(-recoilKickBack.x, recoilKickBack.x),
                Random.Range(-recoilKickBack.y, recoilKickBack.y),
                recoilKickBack.z);
        }
    }

}
