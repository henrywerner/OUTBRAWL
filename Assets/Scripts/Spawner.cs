using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject _brawler, _ranger, _tank;
    [SerializeField] private Canvas _spawnerCanvas;
    [SerializeField] private GameObject _spawnInfo;
    

    public void SpawnEnemiesFromQueue(Queue<int> enemyQueue)
    {
        Queue<int> q = enemyQueue;
        var spawnDelay = 3;
        var spawnDelayIncrement = 3;
        
        while (q.Count != 0)
        {
            StartCoroutine(SpawnEnemy(q.Dequeue(), spawnDelay));
            spawnDelay += spawnDelayIncrement;
        }
    }

    IEnumerator SpawnEnemy(int type, float spawnDelay)
    {
        var startTime = Time.time;
        var spawnTime = Time.time + spawnDelay;
        GameObject newEnemy;
        
        // create ui for enemy spawn timer
        GameObject spawnUI = Instantiate(_spawnInfo, _spawnerCanvas.transform);
        EnemySpawnInfo info = spawnUI.GetComponent<EnemySpawnInfo>();
        info.SetName("gaming");
        
        // find enemy type & set ui name
        switch (type)
        {
            case 2:
                // spawn ranger
                newEnemy = _ranger;
                info.SetName("ranger");
                break;
            case 3:
                newEnemy = _tank;
                info.SetName("tank");
                break;
            default:
                newEnemy = _brawler;
                info.SetName("brawler");
                break;
        }

        newEnemy = _tank;
        info.SetName("tank");

        while (Time.time <= spawnTime)
        {
            // update timer ui
            info.UpdateProgBar(startTime, spawnTime);
            yield return null;
        }
        
        // spawn the enemy
        Instantiate(newEnemy, this.transform.position, Quaternion.Euler(Vector3.forward));
        //Debug.Log("Spawned enemy: " + newEnemy.name + " @ " + newEnemy.transform.position);

        // destroy timer ui
        Destroy(spawnUI);
    }
}
