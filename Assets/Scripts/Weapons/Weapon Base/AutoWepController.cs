using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoWepController : MonoBehaviour
{
    [Header("Weapon Stats")]
    public WeaponScriptableObject weaponData;
    // public GameObject prefab;
    // public float damage;
    // public float speed;
    // public float cooldownDuration;
    // public int pierce; // Pierce is hits before a wep breaks
    float currentCooldown;

    protected PlayerController pm;

    // float cooldownDuration= 2f;
    // float currentCooldown;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        pm = FindObjectOfType<PlayerController>();
        currentCooldown = weaponData.CooldownDuration;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        currentCooldown -= Time.deltaTime;
        if (currentCooldown < 0f)
        {
            Attack();
        }
    }

    protected virtual void Attack()
    {
        print("I am auto attacking!");
        currentCooldown  = weaponData.CooldownDuration;
        
    }
}
