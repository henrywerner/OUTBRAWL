using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimHelper : MonoBehaviour
{
    private Animator _animator;
    private Rigidbody _rb;

    private float vel;
    private Vector3 prevPos;

    private int _animIDSpeed, _animIDPunch;

    private bool inAttackCooldown = false;
    [SerializeField] private float attackCooldownDuration = 1.5f;

    private void Awake()
    {
        _animIDSpeed = Animator.StringToHash("Speed");
        _animIDPunch = Animator.StringToHash("Punch");
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody>();
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
                _animator.SetBool(_animIDPunch, true);
                StartCoroutine(AttackCooldown());
            }
        }
    }

    private IEnumerator AttackCooldown()
    {
        inAttackCooldown = true;

        yield return new WaitForSecondsRealtime(attackCooldownDuration);

        inAttackCooldown = false;
    }
}
