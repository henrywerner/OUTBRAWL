using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PlayerHealth : MonoBehaviour
{
    public Slider healthBar;
    // make sure the the varibles entered for max health also match with the max value under PlayerHealthBar
    public float maxHealth;
    public static float currentHealth;

    //public gameObject GameOverScreen;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        //GameOverScreen.setActive(false);
    }


    // Update is called once per frame
    void Update()
    {
        healthBar.value = currentHealth;
    }


    public void onDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            onKilled();
        }
    }


    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            onDamage(5);
            Debug.Log("You got BUMPED!");
            Debug.Log("Player: " + currentHealth);
        }
    }


    public void onKilled()
    {
        Debug.Log("You got OUTBRAWLed!");
        //GameOverScreen.setActive(true);

        // Having a bit of issue setting up the game over screen
        // so it justs reloads the scene when the player dies
        SceneManager.LoadScene("Playground");
    }


    public void onHeal(float healing)
    {
        currentHealth += healing;
        // ensures that healing doesn't go beyond player's max health
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
    }

}
