using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// BASE SCRIPT OF ALL MELEE BEHAVIOURS ///
// PLACE ON A PREFAB OF A WEP THAT IS MELEE //

public class MeleeWepBehaviour : MonoBehaviour
{

    public WeaponScriptableObject weaponData;
    
    public float destroyAfterSeconds;


    // Current stats
    protected float currentDamage;
    protected float currentSpeed;
    protected float currentCooldownDuration;
    protected int currentPierce;

    void Awake()
    {
        //Assign current stats to be the values of the ones set in the scriptable object upon awakening
        currentDamage = weaponData.Damage;
        currentSpeed = weaponData.Speed;
        currentCooldownDuration = weaponData.CooldownDuration;
        currentPierce = weaponData.Pierce;

    }


    // Start is called before the first frame update
    protected virtual void Start()
    {
        Destroy(gameObject, destroyAfterSeconds);
    }

    // virtual so we can overwrite if needed
    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
        // Reference script from collided collider, dealing damage using the TakeDamage function
        // TakeDamage function from EnemyStats script
        if(col.CompareTag("Enemy"))
        {
            EnemyStats enemy = col.GetComponent<EnemyStats>();
            enemy.TakeDamage(currentDamage); // Use current damage, as we may add damage modifiers later, rather than weapondata.damage
            //ReducePierce();
        }
        // else if(col.gameObject.TryGetComponent("Props"))
        // {
        //     breakable.TakeDamage(currentDamage);
        // }
    }


    // void ReducePierce() // Destroys projectiles when pierce is 0, rather than waiting for timer despawns
    // {
    //     currentPierce--;
    //     if(currentPierce <= 0 )
    //     {
    //         Destroy(gameObject);
    //     }
    // }
}
