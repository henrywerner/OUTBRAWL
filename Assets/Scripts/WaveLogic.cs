using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveLogic : MonoBehaviour
{
    public static WaveLogic Instance { get; private set; }
    
    [SerializeField] private Spawner[] _spawners;
    private int EnemiesActive = 0;
    private int currentWave = 0;

    public void Awake()
    {
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        } 
    }

    public void Start()
    {
        StartNextWave();
    }

    public void Update()
    {
        // if (Input.GetKeyDown(KeyCode.K))
        // {
        //     StartNextWave();
        // }
    }

    public void AddAliveEnemy() => EnemiesActive++;
    public void AddAliveEnemy(int amount) => EnemiesActive += amount;

    public void RemoveAliveEnemy()
    {
        EnemiesActive--;
        if (EnemiesActive <= 0)
            StartNextWave();
    }

    public void StartNextWave()
    {
        //// update wave value
        currentWave++;
        
        //// despawn all enemy corpses?
        
        //// enemy count logic
        int[][] waveQueues = GetWaveEnemyQueues(currentWave);
        
        // I'm not sure if I want have a strict 4 spawners or allow for an indeterminate number
        int currentSpawner = 0;
        foreach (var enemyQueue in waveQueues)
        {
            SpawnEnemies(_spawners[currentSpawner], enemyQueue);
            AddAliveEnemy(enemyQueue.Length); // add number of enemies to the Active pool
            currentSpawner++;
        }
        
        //// update hud?
        Debug.Log("New Wave :: " + currentWave);
    }

    private void SpawnEnemies(Spawner spawner, int[] enemyQueue)
    {
        //// call spawner and queue enemy spawns
        spawner.SpawnEnemiesFromQueue(new Queue<int>(enemyQueue));
    }

    private int[][] GetWaveEnemyQueues(int wave)
    {
        int[] s1 = new int[1]; // I just picked an arbitrary value for the size
        int[] s2 = new int[1];
        int[] s3 = new int[1];
        int[] s4 = new int[1];
        
        switch (wave)
        {
            case 1: // TODO: Figure out why wave one is skipped
                // 4 brawlers
                s1 = new int[] {1, 1};
                s2 = s1;
                s3 = new int[] { }; // I have to set s3 as empty, otherwise it bugs out
                s4 = s3;
                break;
            case 2:
                // 4 brawlers, all separate spawners
                s1 = new int[] {1};
                s2 = s1;
                s3 = s1;
                s4 = s1;
                break;
            case 3:
                // 5 brawlers, 1 tank
                // S1: bb, S2: bb, S3: b, S4: tb
                s1 = new int[] { 1, 1 };
                s2 = s1;
                s3 = new int[] { 1 };
                s4 = new int[] { 3, 1 };
                break;
            case 4:
                // 6 brawlers, 2 tanks
                // S1: bb, S2: bb, S3: tb, S4: tb
                s1 = new int[] {1, 1};
                s2 = s1;
                s3 = new int[] {3, 1};
                s4 = s3;
                break;
            default:
                // default wave logic
                int totalEnemies = (int) (0.1793 * Math.Pow(wave, 2) + 0.0405 * wave) + 6; 
                    // formula stolen from COD zombies. This is just for testing purposes
                
                int rangers = (int) (totalEnemies / 0.2); // 20% rangers
                int tanks = (int) (totalEnemies / 0.1); // 10% tanks

                totalEnemies -= rangers + tanks;

                // oh god, what have I done?
                List<int> s1l = new List<int>();
                List<int> s2l = new List<int>();
                List<int> s3l = new List<int>();
                List<int> s4l = new List<int>();
                
                s1l.Add(totalEnemies % 16);
                s2l.Add(totalEnemies % 16);
                totalEnemies -= totalEnemies % 16;
                
                s1l.Add(totalEnemies/8);
                s1l.Add(tanks/4);
                s1l.Add(totalEnemies/8);
                
                s2l.Add(totalEnemies/8);
                s2l.Add(tanks/4);
                s2l.Add(totalEnemies/8);
                
                s3l.Add(rangers/2);
                s3l.Add(totalEnemies/8);
                s3l.Add(tanks/4);
                s3l.Add(totalEnemies/8);
                
                s4l.Add(rangers/2);
                s4l.Add(totalEnemies/8);
                s4l.Add(tanks/4);
                s4l.Add(totalEnemies/8);

                s1 = s1l.ToArray();
                s2 = s2l.ToArray();
                s3 = s3l.ToArray();
                s4 = s4l.ToArray();
                break;
        }

        return new int[][] { s1, s2, s3, s4 };
    }
}
