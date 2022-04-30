using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class BrawlAttack : MonoBehaviour
{
    [FormerlySerializedAs("damage")] [SerializeField] int punchDamage = 30;
    [SerializeField] int kickDamage = 30;
    [SerializeField] PowerMeter powerMeter;
    [SerializeField] Hitbox RHandHitbox;
    [SerializeField] Hitbox RFootHitbox;

    private bool isPlayer = false;
    private List<Hitbox> hitboxes = new List<Hitbox>();

    private void Awake()
    {
        hitboxes.Add(RHandHitbox);
        hitboxes.Add(RFootHitbox);
    }

    void Start()
    {
        isPlayer = gameObject.tag == "Player" ? true : false;

        foreach(Hitbox h in hitboxes)
        {
            h.enabled = false;
            h.hitbox.enabled = false;
            h.hitbox.isTrigger = true;
        }
    }

    public void OnTriggerEnterExternal(Collider trigger, Collider other)
    {
        GameObject obj = other.gameObject;

        if (obj.tag == (isPlayer ? "Enemy" : "Player") && obj.GetComponent<Health>() != null) // don't let gameobject punchDamage itself
        {
            obj.GetComponent<Health>().OnDamage(punchDamage);
        }
        else if (obj.layer == 7) // obj on ragdoll layer
        {
            obj.GetComponent<Rigidbody>().AddForce(trigger.transform.right * 20f, ForceMode.Impulse);
        }
    }

    public void TogglePunch()
    {
        RHandHitbox.enabled = !RHandHitbox.enabled;
        RHandHitbox.hitbox.enabled = !RHandHitbox.hitbox.enabled;
    }

    public void ToggleKick()
    {
        RFootHitbox.enabled = !RFootHitbox.enabled;
        RFootHitbox.hitbox.enabled = !RFootHitbox.hitbox.enabled;
    }

    public void NoAttack()
    {
        foreach (Hitbox h in hitboxes)
        {
            h.enabled = false;
            h.hitbox.enabled = false;
        }
    }
}
