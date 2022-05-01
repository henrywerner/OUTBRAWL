using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] private Slider healthBar;
    // make sure the the varibles entered for max health also match with the max value under PlayerHealthBar
    public float maxHealth;
    private float currentHealth;
    [SerializeField] GameObject healthBarUI;
    [SerializeField] StarterAssets.ThirdPersonController controller;
    [SerializeField] float invincibilityDuration = 0.75f;
    private float invincibilityTimer = 0f; // current amount of invincibility remaining

    private bool isAlive = true;
    private bool isPlayer = false;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.maxValue = maxHealth;
        isPlayer = gameObject.tag == "Player" ? true : false;
    }

    void Update()
    {
        if (healthBar != null)
        {
            healthBar.value = currentHealth;
        }

        // Ensures that the health bar is always facing the camera
        if (healthBarUI != null)
        {
            healthBarUI.transform.LookAt(Camera.main.transform);
        }

        if (invincibilityTimer > 0)
        {
            invincibilityTimer -= Time.deltaTime;

            if(invincibilityTimer <= 0)
            {
                invincibilityTimer = 0;
            }
        }
    }

    public void OnDamage(float damage)
    {
        if (invincibilityTimer == 0)
        {
            invincibilityTimer = invincibilityDuration;
            currentHealth -= damage;
            if (currentHealth <= 0 && isAlive)
            {
                currentHealth = 0;
                StartCoroutine(OnKilled());
            }
        }
    }

    public IEnumerator OnKilled()
    {
        isAlive = false;
        controller.ToggleRagdoll();

        if (!isPlayer)
        {
            Debug.Log("Enemy Killed: " + gameObject.name);
            WaveLogic.Instance.RemoveAliveEnemy();

            // yes, this is stupid
            controller.gameObject.GetComponent<NavMeshAgent>().enabled = false;
            controller.gameObject.GetComponent<EnemyNavMesh>().enabled = false;

            yield return new WaitForSeconds(4f);
            Destroy(gameObject);
        }
        else
        {
            controller.inputAllowed = false;
            yield return new WaitForSeconds(6f);
            GameOver.Restart();
        }
    }
}
