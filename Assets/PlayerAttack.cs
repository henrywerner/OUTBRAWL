using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] PowerMeter powerMeter;
    [SerializeField] Hitbox RHandHitbox;

    void Awake()
    {
        //RHandHboxCollider = RHandHitbox.GetComponent<Collider>();
    }

    void Start()
    {
        RHandHitbox.enabled = false;
        RHandHitbox.hitbox.enabled = false;
        RHandHitbox.hitbox.isTrigger = true;
    }

    void FixedUpdate()
    {
        // check RHand

    }

    public void OnTriggerEnterExternal(Collider trigger, Collider other)
    {
        // get EnemyHealth reference from other collider
        // decrease enemy health based on power level and attack type (which trigger is attacking)
            // call onDamage(float dmg)
    }

    public void ToggleCrossPunch()
    {
        Debug.Log("doing a cross punch now");
        RHandHitbox.enabled = !RHandHitbox.enabled;
        RHandHitbox.hitbox.enabled = !RHandHitbox.hitbox.enabled;
    }
}
