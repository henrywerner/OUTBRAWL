using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TankAnimHelper : MonoBehaviour
{
    private Animator _animator;
    private Rigidbody _rb;
    private EnemyNavMesh m_NavAgent;

    private float vel;
    private Vector3 prevPos;

    private int _animIDSpeed, _animIDPunch, _animIDKick;

    private bool inAttackCooldown = false;
    [SerializeField] private float attackCooldownDuration = 1.5f, attackDelay = 0.5f;
    [SerializeField] private float punchPauseDuration = 1f, kickPauseDuration = 1.2f;
    private void Awake()
    {
        _animIDSpeed = Animator.StringToHash("Speed");
        _animIDPunch = Animator.StringToHash("Punch");
        _animIDKick = Animator.StringToHash("Kick");
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody>();
        m_NavAgent = GetComponent<EnemyNavMesh>();
    }

    void Update()
    {
        // Compare previous pos to current pos to find enemy's speed
        vel = ((transform.position - prevPos).magnitude) / Time.deltaTime;
        prevPos = transform.position;
        
        _animator.SetFloat(_animIDSpeed, vel);
        //print("" + gameObject.name + ": speed = " + vel);
    }

    private void FixedUpdate()
    {
        if (inAttackCooldown)
        {
            //_animator.SetBool(_animIDPunch, false);
            return;
        }
        
        // If player in range and not on attack cooldown

        int layerMask = 1 << 6;
        Vector3 offset = new Vector3(0, 1, 0);
        
        // raycast forward with fixed range
        RaycastHit hit;
        if (Physics.Raycast(transform.position + offset, transform.TransformDirection(Vector3.forward), out hit, 1.5f, layerMask))
        {
            Debug.DrawRay(transform.position + offset, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            if (hit.collider.CompareTag("Player"))
            {
                StartCoroutine(Attack());
            }
        }
    }

    private IEnumerator Attack()
    {
        // calculate kick or punch
        float f = Random.Range(0, 100);
        bool isPunch = f >= 35; // used as a percent chance to punch
        
        // tell the nav agent to stop moving
        StartCoroutine(m_NavAgent.EnterAttackPause(isPunch ? punchPauseDuration : kickPauseDuration));

        // wait for attack delay
        yield return new WaitForSecondsRealtime(attackDelay);
        
        // attack
        _animator.SetBool(isPunch ? _animIDPunch : _animIDKick, true);
        
        // start attack cooldown
        StartCoroutine(AttackCooldown());
        
        // wait enough time for the animator to detect the attack
        yield return new WaitForSecondsRealtime(0.2f);  
        // then flag the attack as done so we don't spam punch 3 times
        _animator.SetBool(isPunch ? _animIDPunch : _animIDKick, false);

        yield return null;
    }

    private IEnumerator AttackCooldown()
    {
        inAttackCooldown = true;

        yield return new WaitForSecondsRealtime(attackCooldownDuration);

        inAttackCooldown = false;
        
    }
}
