using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class TankAttack : MonoBehaviour
{
    [FormerlySerializedAs("damage")] [SerializeField] int swipeDamage = 30;
    [SerializeField] int slamDamage = 60;
    [SerializeField] Hitbox LArmHitbox;
    [SerializeField] Hitbox RFootHitbox;

    private bool isPlayer = false;
    private List<Hitbox> hitboxes = new List<Hitbox>();
    private StarterAssets.ThirdPersonController controller;

    private void Awake()
    {
        controller = gameObject.GetComponent<StarterAssets.ThirdPersonController>();
        hitboxes.Add(LArmHitbox);
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
            obj.GetComponent<Health>().OnDamage(swipeDamage);
        }
        else if (obj.layer == 7) // obj on ragdoll layer
        {
            obj.GetComponent<Rigidbody>().AddForce(trigger.transform.right * 20f, ForceMode.Impulse);
        }
    }

    public void ToggleSwipe()
    {
        controller.isAttacking = !controller.isAttacking;
        LArmHitbox.enabled = !LArmHitbox.enabled;
        LArmHitbox.hitbox.enabled = !LArmHitbox.hitbox.enabled;
    }

    public void ToggleSlam()
    {
        controller.isAttacking = !controller.isAttacking;
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
