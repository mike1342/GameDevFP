using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpAmmo : MonoBehaviour
{
    public AudioClip pickupSFX;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            AudioSource.PlayClipAtPoint(pickupSFX, transform.position);

            var unusedAmmo = other.GetComponent<Gun>();
            unusedAmmo.AmmoPickUp();

            Destroy(gameObject, 0.5f);
        }
    }
}
