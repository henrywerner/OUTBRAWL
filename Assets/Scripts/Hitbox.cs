using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    [SerializeField] BrawlAttack attack;

    public Collider hitbox;

    private void Awake()
    {
        hitbox = gameObject.GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        attack.OnTriggerEnterExternal(hitbox, other);
    }
}
