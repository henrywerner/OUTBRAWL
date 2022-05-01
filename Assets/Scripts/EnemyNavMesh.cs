using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemyNavMesh : MonoBehaviour
{
    [SerializeField] private float navModRange_quad = 0.05f;
    [SerializeField] private float navModRange_under = 0.05f;
    [SerializeField] private float navModRange_upper = 0.05f;
    [SerializeField] private float navModRange_stairs = 0.05f;
    [SerializeField] private float navModRange_platform = 0.05f;
    
    [SerializeField] private Transform movePosTransform;
    private NavMeshAgent m_Agent;
    private Transform m_PlayerTransform;

    public bool inAttackPause = false;
    
    private void Awake()
    {
        m_Agent = GetComponent<NavMeshAgent>();
        m_PlayerTransform = GameObject.FindGameObjectsWithTag("Player")[0].transform; //FindObjectOfType<PlayerHealth>().transform;
        m_Agent.avoidancePriority = (int)Random.Range(1f, 50f);
    }

    private void Start()
    {
        SetMeshWeights();
    }

    private void Update()
    {
        //m_Agent.destination = movePosTransform.position;
        
        if (!inAttackPause)
            m_Agent.destination = m_PlayerTransform.position;
    }

    public IEnumerator EnterAttackPause(float duration)
    {
        inAttackPause = true;

        yield return new WaitForSecondsRealtime(duration);

        inAttackPause = false;
    }

    private void SetMeshWeights()
    {
        // Quadrant modifiers
        int SW_id = NavMesh.GetAreaFromName("SouthWest");
        int NW_id = NavMesh.GetAreaFromName("NorthWest");
        int NE_id = NavMesh.GetAreaFromName("NorthEast");
        int SE_id = NavMesh.GetAreaFromName("SouthEast");

        float modifier = 1 + Random.Range(-navModRange_quad, navModRange_quad);
        m_Agent.SetAreaCost(SW_id, m_Agent.GetAreaCost(SW_id) * modifier); // SouthWest
        
        modifier = 1 + Random.Range(-navModRange_quad, navModRange_quad);
        m_Agent.SetAreaCost(NW_id, m_Agent.GetAreaCost(NW_id) * modifier); // NorthWest
        
        modifier = 1 + Random.Range(-navModRange_quad, navModRange_quad);
        m_Agent.SetAreaCost(NE_id, m_Agent.GetAreaCost(NE_id) * modifier); // NorthEast
        
        modifier = 1 + Random.Range(-navModRange_quad, navModRange_quad);
        m_Agent.SetAreaCost(SE_id, m_Agent.GetAreaCost(SE_id) * modifier); // SouthEast
        
        // Under modifier
        int underID = NavMesh.GetAreaFromName("Under");
        modifier = 1 + Random.Range(-navModRange_under, navModRange_under);
        m_Agent.SetAreaCost(underID, m_Agent.GetAreaCost(underID) * modifier);
        
        // Upper modifier
        int upperID = NavMesh.GetAreaFromName("Upper");
        modifier = 1 + Random.Range(-navModRange_upper, navModRange_upper);
        m_Agent.SetAreaCost(upperID, m_Agent.GetAreaCost(upperID) * modifier);
        
        // Stairs modifier
        int stairsID = NavMesh.GetAreaFromName("Stairs");
        modifier = 1 + Random.Range(-navModRange_stairs, navModRange_stairs);
        m_Agent.SetAreaCost(stairsID, m_Agent.GetAreaCost(stairsID) * modifier);
        
        // Platform modifier
        int platformID = NavMesh.GetAreaFromName("Platform");
        modifier = 1 + Random.Range(-navModRange_platform, navModRange_platform);
        m_Agent.SetAreaCost(platformID, m_Agent.GetAreaCost(platformID) * modifier);
        
        // Debug msg
        string msg = "";
        msg += " SW:" + m_Agent.GetAreaCost(SW_id);
        msg += " NW:" + m_Agent.GetAreaCost(NW_id);
        msg += " NE:" + m_Agent.GetAreaCost(NE_id);
        msg += " SE:" + m_Agent.GetAreaCost(SE_id);
        msg += " Under:" + m_Agent.GetAreaCost(underID);
        msg += " Upper:" + m_Agent.GetAreaCost(upperID);
        msg += " Stairs:" + m_Agent.GetAreaCost(stairsID);
        msg += " Platform:" + m_Agent.GetAreaCost(platformID);
        
        Debug.Log("" + gameObject.name + " navmesh adjusted:" + msg);
    }
}
