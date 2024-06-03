using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float startingHealth = 100;
    float currentHealth;
    bool isInvulnerable = false;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = startingHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float damageAmount) {
        if(currentHealth > 0 && !isInvulnerable) {
            currentHealth -= damageAmount;
            isInvulnerable = true;
            Invoke("removeInvulnerability", 0.2f);
        }
        if(currentHealth <= 0) {
            PlayerDies();
        }
        Debug.Log("Health: " + currentHealth);
    }

    void PlayerDies() {
        Debug.Log("Player is dead.");
        GameObject.FindObjectOfType<LevelManager>().LevelLost();
        gameObject.SetActive(false);
    }

    void removeInvulnerability() {
        isInvulnerable = false;
    }

}
