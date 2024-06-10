using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="Gun", menuName="Weapon/Gun")]
public class GunData : ScriptableObject
{

    public enum GunType {
        Manual,
        SemiAuto,
        Auto
    }

    public new string name;
    public GunType gunType;
    public float damage;
    public float maxDistance;


    public int currentAmmo;
    public int unusedAmmo;
    public int magSize;
    public float fireRate;
    public float reloadTime;
    public bool isReloading;
    public AudioClip reloadClip;
    public AudioClip fireClip;

}
