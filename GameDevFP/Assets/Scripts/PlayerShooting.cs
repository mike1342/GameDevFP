using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShooting : MonoBehaviour
{
    public delegate void ShootDelegate(bool isManual);
    public static event ShootDelegate shootAction;
    public static Action reloadAction;
    public static Action releaseAction;

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            shootAction?.Invoke(true);
        }

        if (Input.GetButton("Fire1")) {
            shootAction?.Invoke(false);
        }

        if (Input.GetButtonUp("Fire1"))
        {
            releaseAction?.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            reloadAction?.Invoke();
        }
    }


    //public GameObject projectilePrefab;
    //public float projectileSpeed = 100f;



    //// Start is called before the first frame update
    //void Start()
    //{

    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    if (Input.GetButtonDown("Fire1"))
    //    {
    //        GameObject projectile = Instantiate(projectilePrefab,
    //            transform.position + transform.forward, Quaternion.LookRotation(transform.forward)) as GameObject;

    //        projectile.transform.SetParent(
    //            GameObject.FindGameObjectWithTag("Bullets").transform);

    //        Rigidbody rb = projectile.GetComponent<Rigidbody>();

    //        rb.AddForce(transform.forward * projectileSpeed, ForceMode.VelocityChange);
    //    }
    //}
}
