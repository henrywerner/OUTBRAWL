using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] int damage = 30;
    [SerializeField] PowerMeter powerMeter;
    [SerializeField] Hitbox RHandHitbox;

    private bool isPlayer = false;

    void Start()
    {
        isPlayer = gameObject.tag == "Player" ? true : false;

        RHandHitbox.enabled = false;
        RHandHitbox.hitbox.enabled = false;
        RHandHitbox.hitbox.isTrigger = true;
    }

    public void OnTriggerEnterExternal(Collider trigger, Collider other)
    {
        GameObject obj = other.gameObject;

        if (obj.tag == (isPlayer ? "Enemy" : "Player") && obj.GetComponent<Health>() != null) // don't let gameobject damage itself
        {
            obj.GetComponent<Health>().OnDamage(damage);
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
