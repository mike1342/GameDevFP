using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpHealing : MonoBehaviour
{
    public AudioClip pickupSFX;

    public float pickupDistance = 1.0f;

    public int healAmount = 20;

    public GameObject player;

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");     
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) <= pickupDistance)
        {
            var playerHealth = player.GetComponent<PlayerHealth>();
            playerHealth.TakeHealing(healAmount);
            AudioSource.PlayClipAtPoint(pickupSFX, transform.position);
            Destroy(gameObject);
        }
    }

}
