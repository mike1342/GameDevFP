using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{
    public GunData gunData;
    public Transform muzzle;

    public Text currAmmoText;
    public Text unusedAmmoText;

    float timeSinceLastShot = 0f;

    bool canShootSemi = true;
    int semiShotCount = 0;

    private void Start()
    {
        currAmmoText.text = gunData.currentAmmo.ToString();
        unusedAmmoText.text = gunData.unusedAmmo.ToString();

    
        gunData.unusedAmmo = gunData.magSize * 4;
        gunData.currentAmmo = gunData.magSize;
        gunData.isReloading = false;
    }

    private void OnEnable() {
        PlayerShooting.shootAction += Shoot;
        PlayerShooting.reloadAction += StartReload;
        PlayerShooting.releaseAction += ReleaseFire;
    }

    private void OnDisable() {
        PlayerShooting.shootAction -= Shoot;
        PlayerShooting.reloadAction -= StartReload;
        PlayerShooting.releaseAction -= ReleaseFire;
    }

    private void Update()
    {
        timeSinceLastShot += Time.deltaTime;

        currAmmoText.text = gunData.currentAmmo.ToString();
        unusedAmmoText.text = gunData.unusedAmmo.ToString();
    }

    private bool CanShoot()
    {
        return !gunData.isReloading &&
            timeSinceLastShot > (60f / gunData.fireRate);
    }

    public void StartReload()
    {
        if (!gunData.isReloading && gunData.unusedAmmo > 0)
        {
            StartCoroutine(Reload());
        }
    }

    public void ReleaseFire()
    {
        canShootSemi = true;
        semiShotCount = 0;
    }

    private IEnumerator Reload()
    {
        Debug.Log("Reloading");
        gunData.isReloading = true;
        AudioSource.PlayClipAtPoint(gunData.reloadClip, Camera.main.transform.position);

        yield return new WaitForSeconds(gunData.reloadTime);

        int bulletsToAdd = Mathf.Min(gunData.magSize - gunData.currentAmmo, gunData.unusedAmmo);

        gunData.currentAmmo += bulletsToAdd;
        gunData.unusedAmmo -= bulletsToAdd;

        currAmmoText.text = gunData.currentAmmo.ToString();
        unusedAmmoText.text = gunData.unusedAmmo.ToString();

        gunData.isReloading = false;
        Debug.Log("Finished");
    }

    public void Shoot(bool isManual)
    {
        if (gunData.currentAmmo > 0)
        {
            switch (gunData.gunType) 
            {
                case GunData.GunType.Manual:
                    if (CanShoot() && isManual)
                    {
                        FireBullet();
                    }
                    break;
                case GunData.GunType.SemiAuto:
                    if (CanShoot() && canShootSemi)
                    {
                        FireBullet();
                        semiShotCount++;
                        if (semiShotCount >= 3)
                        {
                            canShootSemi = false;
                        }
                    }
                    break;
                case GunData.GunType.Auto:
                    if (CanShoot())
                    {
                        FireBullet();
                    }
                    break;
            }
            
        }
    }

    private void FireBullet() 
    {
        if (Physics.Raycast(muzzle.position, transform.forward, out RaycastHit hitInfo, gunData.maxDistance))
        {
            IEnemy enemy = hitInfo.transform.GetComponent<IEnemy>();
            enemy?.Damage(gunData.damage);
        }
        AudioSource.PlayClipAtPoint(gunData.fireClip, Camera.main.transform.position);
        gunData.currentAmmo--;
        currAmmoText.text = gunData.currentAmmo.ToString();
        timeSinceLastShot = 0f;
        GunShot();
    }

    private void GunShot()
    {
        Debug.Log("Gun Shot, ammo: " + gunData.currentAmmo);
    }

    public void AmmoPickUp()
    {
        gunData.unusedAmmo += (gunData.magSize + (gunData.magSize)/2);

        Debug.Log("Ammo Increased");
    }
}
