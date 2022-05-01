using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject _brawler, _ranger, _tank;
    [SerializeField] private Canvas _spawnerCanvas, _countdownCanvas;
    [SerializeField] private GameObject _spawnInfo;
    [SerializeField] private TMP_Text _countdown;

    //private Queue<int> _enemyQueueGaming;
    private List<int> _enemyGaming = new List<int>(32);
    private List<GameObject> _enemyNameplates = new List<GameObject>(32);

    private bool isSpawning = false;

    public void Start()
    {
        //_enemyQueueGaming = new Queue<int>();
        //_enemyNameplates = new List<GameObject>(32);
        //_enemyQueueGaming.Enqueue(0);
        //_enemyGaming = new List<int>(32);
        //_countdownCanvas.enabled = false;
        _spawnerCanvas.enabled = true;
    }

    public void SpawnEnemiesFromQueue(List<int> enemyQueue)
    {
        _countdownCanvas.enabled = true;
        
        //print(gameObject.name);
        List<int> q = enemyQueue;

        foreach (var e in q)
        {
            //_enemyQueueGaming.Enqueue(e);
            _enemyGaming.Add(e);

            // add enemy nameplate to nameplate queue
            GameObject spawnUI = Instantiate(_spawnInfo, _spawnerCanvas.transform);
            EnemySpawnInfo info = spawnUI.GetComponent<EnemySpawnInfo>();
            info.SetName("gaming");
        
            // find enemy type & set ui name
            switch (e)
            {
                case 2:
                    info.SetName("ranger");
                    break;
                case 3:
                    info.SetName("tank");
                    break;
                default:
                    info.SetName("brawler");
                    break;
            }
            
            //_enemyNameplates.Enqueue(spawnUI);
            _enemyNameplates.Add(spawnUI);
        }
        
        // spawn queue is empty, start the coroutine
        if (!isSpawning)
        {
            StartCoroutine(SpawnFromQueue());
        }

    }

    IEnumerator SpawnFromQueue()
    {
        isSpawning = true;
        //_countdownCanvas.enabled = true;
        
        float spawnDelay = 5;
        
        while (_enemyGaming.Count != 0)
        {
            _countdownCanvas.enabled = true;
            float t = 0;
            
            while (t <= spawnDelay)
            {
                // update timer ui
                TimeSpan timeSpan = TimeSpan.FromSeconds(t);
                string timeText = string.Format("{0:D2}", timeSpan.Seconds);
                _countdown.text = "" + (int)(5-t);

                t += Time.deltaTime;
                yield return null;
            }
            
            
            
            // find enemy's type
            GameObject newEnemy;

            switch (_enemyGaming[0])
            {
                case 2:
                    // spawn ranger
                    newEnemy = _ranger;
                    break;
                case 3:
                    newEnemy = _tank;
                    break;
                default:
                    newEnemy = _brawler;
                    break;
            }
            _enemyGaming.RemoveAt(0);
            
            // spawn the enemy
            Instantiate(newEnemy, this.transform.position, Quaternion.Euler(Vector3.forward));
            
            // destroy the corresponding nameplate
            Destroy(_enemyNameplates[0]);
            _enemyNameplates.RemoveAt(0);
        }

        isSpawning = false;
        _countdownCanvas.enabled = false;
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

        // newEnemy = _tank;
        // info.SetName("tank");

        while (Time.time <= spawnTime)
        {
            // update timer ui
            //info.UpdateProgBar(startTime, spawnTime);
            yield return null;
        }
        
        // spawn the enemy
        Instantiate(newEnemy, this.transform.position, Quaternion.Euler(Vector3.forward));
        //Debug.Log("Spawned enemy: " + newEnemy.name + " @ " + newEnemy.transform.position);

        // destroy timer ui
        Destroy(spawnUI);
    }
}
