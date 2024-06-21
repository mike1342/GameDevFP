using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float startingHealth = 100;
    public Slider healthSlider;
    float currentHealth;
    bool isInvulnerable = false;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = startingHealth;
        healthSlider.value = currentHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float damageAmount) {
        if(currentHealth > 0 && !isInvulnerable) {
            currentHealth -= damageAmount;
            healthSlider.value = currentHealth;
            isInvulnerable = true;
            Invoke("removeInvulnerability", 0.2f);
        }
        if(currentHealth <= 0) {
            PlayerDies();
        }
        Debug.Log("Health: " + currentHealth);
    }

        public void TakeHealing(int healthAmount) {
        if(currentHealth < 100) {
            currentHealth += healthAmount;
            currentHealth = Mathf.Clamp(currentHealth, 0, 100);
            healthSlider.value = currentHealth;
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
