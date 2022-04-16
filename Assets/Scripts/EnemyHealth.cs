using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public Slider enemyHealthBar;
    // make sure the the varibles entered for max health also match with the max value under PlayerHealthBar
    public float maxHealth;
    public static float currentHealth;
    [SerializeField] GameObject enemyHealthBarUI;
    [SerializeField] StarterAssets.ThirdPersonController enemyController;

    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        enemyHealthBar.value = currentHealth;

        // Ensures that the health bar is always facing the camera
        enemyHealthBarUI.transform.LookAt(Camera.main.transform);

    }

    public void OnDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            StartCoroutine(OnKilled());
        }
    }

    public IEnumerator OnKilled()
    {
        enemyController.ToggleRagdoll();
        yield return new WaitForSeconds(4f);
        Destroy(gameObject);
    }
}
