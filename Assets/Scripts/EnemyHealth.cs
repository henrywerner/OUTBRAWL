using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private Slider enemyHealthBar;
    // make sure the the varibles entered for max health also match with the max value under PlayerHealthBar
    public float maxHealth;
    private float currentHealth;
    [SerializeField] GameObject enemyHealthBarUI;
    [SerializeField] StarterAssets.ThirdPersonController enemyController;

    private bool isAlive = true;

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
        if (currentHealth <= 0 && isAlive)
        {
            currentHealth = 0;
            StartCoroutine(OnKilled());
        }
    }

    public IEnumerator OnKilled()
    {
        isAlive = false;
        Debug.Log("Enemy Killed: " + gameObject.name);
        WaveLogic.Instance.RemoveAliveEnemy();
        
        enemyController.ToggleRagdoll();
        
        // yes, this is stupid
        enemyController.gameObject.GetComponent<NavMeshAgent>().enabled = false;
        enemyController.gameObject.GetComponent<EnemyNavMesh>().enabled = false;
        
        yield return new WaitForSeconds(4f);
        Destroy(gameObject);
    }
}
