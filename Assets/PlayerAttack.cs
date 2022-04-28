using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] PowerMeter powerMeter;
    [SerializeField] Hitbox RHandHitbox;

    void Start()
    {
        RHandHitbox.enabled = false;
        RHandHitbox.hitbox.enabled = false;
        RHandHitbox.hitbox.isTrigger = true;
    }

    public void OnTriggerEnterExternal(Collider trigger, Collider other)
    {
        GameObject obj = other.gameObject;

        if (obj.tag == "Enemy" && obj.GetComponent<Health>() != null)
        {
            obj.GetComponent<Health>().OnDamage(30);
            
        }
        else if (obj.layer == 7) // obj on ragdoll layer
        {
            obj.GetComponent<Rigidbody>().AddForce(trigger.transform.right * 20f, ForceMode.Impulse);
        }
    }

    public void ToggleCrossPunch()
    {
        RHandHitbox.enabled = !RHandHitbox.enabled;
        RHandHitbox.hitbox.enabled = !RHandHitbox.hitbox.enabled;
    }
}
