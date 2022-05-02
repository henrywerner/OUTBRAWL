using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    [SerializeField] BrawlAttack BAttack;
    [SerializeField] TankAttack TAttack;

    public Collider hitbox;

    private void Awake()
    {
        hitbox = gameObject.GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (BAttack != null)
        {
            BAttack.OnTriggerEnterExternal(hitbox, other);
        }
        else if (TAttack != null)
        {
            TAttack.OnTriggerEnterExternal(hitbox, other);
        }
    }
}
