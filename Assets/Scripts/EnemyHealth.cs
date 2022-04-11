using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public Slider enemyHealthBar;
    // make sure the the varibles entered for max health also match with the max value under PlayerHealthBar
    public float maxHealth;
    public static float currentHealth;
    [SerializeField] GameObject enemyHealthBarUI;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        enemyHealthBar.value = currentHealth;

        // Ensures that the health bar is always facing the camera
        enemyHealthBarUI.transform.LookAt(Camera.main.transform);

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
        if (other.gameObject.tag == "Player")
        {
            onDamage(5);
            Debug.Log("Enemy got BUMPED!");
            Debug.Log("Enemy: " + currentHealth);
        }
    }


    public void onKilled()
    {
        Destroy(gameObject);
    }
}
