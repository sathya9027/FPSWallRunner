using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GunData gunData;

    public float adsSpeed = 5f;
    public float adsZoom = 30f;
    public Transform adsOutPoint, adsInPoint;

    public GameObject bulletImpact;
    public GameObject muzzleFlash;

    public float muzzleDisplayTime;
    private float muzzleCounter;

    private Camera cam;
    private float timeSinceLastShot;
    private AudioSource audioSource;

    private void Start()
    {
        PlayerShoot.shootInput += Shoot;
        PlayerShoot.reloadInput += StartReload;
        cam = Camera.main;
        audioSource = cam.GetComponent<AudioSource>();
        gunData.currentAmmo = gunData.magSize;
    }

    private void Update()
    {
        if (!PauseMenu.instance.isPaused)
        {

            PlayerUI.instance.ammoText.text = gunData.currentAmmo + "/" + gunData.magSize;
            timeSinceLastShot += Time.deltaTime;

            if (Input.GetMouseButton(1))
            {
                cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, adsZoom, adsSpeed * Time.deltaTime);
                transform.position = Vector3.Lerp(transform.position, adsInPoint.position, adsSpeed * Time.deltaTime);
            }
            else
            {
                cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, 60f, adsSpeed * Time.deltaTime);
                transform.position = Vector3.Lerp(transform.position, adsOutPoint.position, adsSpeed * Time.deltaTime);
            }

            if (muzzleFlash.activeInHierarchy)
            {
                muzzleCounter -= Time.deltaTime;

                if (muzzleCounter <= 0)
                {
                    muzzleFlash.SetActive(false);
                }
            }
        }
    }

    private void OnDisable() => gunData.reloading = false;

    public void StartReload()
    {
        if (!gunData.reloading && this.gameObject.activeSelf)
        {
            StartCoroutine(Reload());
        }
        else if (!this.gameObject.activeSelf)
        {
            StopAllCoroutines();
        }
    }

    private IEnumerator Reload()
    {
        gunData.reloading = true;

        audioSource.clip = gunData.reloadSFX;
        audioSource.Play();

        yield return new WaitForSeconds(gunData.reloadTime);

        gunData.currentAmmo = gunData.magSize;
        gunData.reloading = false;
    }

    private bool CanShoot() => !gunData.reloading && timeSinceLastShot > 1f / (gunData.fireRate / 60f);

    public void Shoot()
    {
        if (gunData.currentAmmo > 0)
        {
            if (CanShoot())
            {
                if (cam == null)
                {
                    cam = Camera.main;
                }
                if (Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hitInfo, gunData.maxDistance))
                {
                    IDamageable damageable = hitInfo.transform.GetComponent<IDamageable>();
                    damageable?.Damage(gunData.damage);
                    Debug.Log("shot!");

                    if (!hitInfo.collider.isTrigger)
                    {
                        GameObject bulletImpactObject = Instantiate
                            (bulletImpact, 
                            hitInfo.point + (hitInfo.normal * .002f), 
                            Quaternion.LookRotation(hitInfo.normal, Vector3.up));
                        bulletImpactObject.transform.parent = hitInfo.transform;
                        Destroy(bulletImpactObject, 10f);
                    }
                }
                OnGunShot();
            }
        }
    }

    private void OnGunShot()
    {
        if (audioSource == null)
        {
            audioSource = cam.gameObject.GetComponent<AudioSource>();
        }
        audioSource.clip = gunData.fireSFX;
        audioSource.Play();
        if (muzzleFlash != null)
        {
            muzzleFlash.SetActive(true);
        }
        muzzleCounter = muzzleDisplayTime;
        gunData.currentAmmo--;
        timeSinceLastShot = 0;
    }
}
