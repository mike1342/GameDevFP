using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpAmmo : MonoBehaviour
{
    public AudioClip pickupSFX;

    public float pickupDistance = 1.0f; 

    public GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");     
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) <= pickupDistance)
        {
            var gun = player.GetComponentInChildren<Gun>();
            gun.AmmoPickUp();
            AudioSource.PlayClipAtPoint(pickupSFX, transform.position);
            Destroy(gameObject);
        }
    }

}
