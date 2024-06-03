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

    private void Start()
    {
        PlayerShooting.shootAction += Shoot;
        PlayerShooting.reloadAction += StartReload;

        currAmmoText.text = gunData.currentAmmo.ToString();
        unusedAmmoText.text = gunData.unusedAmmo.ToString();
    }

    private void Update()
    {
        timeSinceLastShot += Time.deltaTime;
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

    public void Shoot()
    {
        if (gunData.currentAmmo > 0)
        {
            if (CanShoot())
            {
                if (Physics.Raycast(muzzle.position, transform.forward, out RaycastHit hitInfo, gunData.maxDistance))
                {
                    IEnemy enemy = hitInfo.transform.GetComponent<IEnemy>();
                    enemy?.Damage(gunData.damage);
                }

                gunData.currentAmmo--;
                currAmmoText.text = gunData.currentAmmo.ToString();
                timeSinceLastShot = 0f;
                GunShot();
            }
        }
    }

    private void GunShot()
    {
        Debug.Log("Gun Shot, ammo: " + gunData.currentAmmo);
    }
}
