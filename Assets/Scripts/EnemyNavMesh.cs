using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNavMesh : MonoBehaviour
{
    [SerializeField] private Transform movePosTransform;
    private NavMeshAgent m_Agent;
    private Transform m_PlayerTransform;
    
    private void Awake()
    {
        m_Agent = GetComponent<NavMeshAgent>();
        m_PlayerTransform = FindObjectOfType<PlayerHealth>().transform;
    }

    private void Update()
    {
        //m_Agent.destination = movePosTransform.position;
        m_Agent.destination = m_PlayerTransform.position;
    }
}
