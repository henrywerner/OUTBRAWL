using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemyNavMesh : MonoBehaviour
{
    [SerializeField] private Transform movePosTransform;
    private NavMeshAgent m_Agent;
    private Transform m_PlayerTransform;
    
    private void Awake()
    {
        m_Agent = GetComponent<NavMeshAgent>();
        m_PlayerTransform = GameObject.FindGameObjectsWithTag("Player")[0].transform; //FindObjectOfType<PlayerHealth>().transform;
        m_Agent.avoidancePriority = (int)Random.Range(1f, 50f);
    }

    private void Update()
    {
        //m_Agent.destination = movePosTransform.position;
        m_Agent.destination = m_PlayerTransform.position;
    }
}
