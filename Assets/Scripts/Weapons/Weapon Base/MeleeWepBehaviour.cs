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

    // Updates current Damage with any modifiers to stat (e.g might)
    // Repalce currentDamage with this function GetCurrentDamage()
    public float GetCurrentDamage()
    {
        return currentDamage *= FindObjectOfType<PlayerStats>().currentMight;
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
            enemy.TakeDamage(GetCurrentDamage()); // Use current damage, as we may add damage modifiers later, rather than weapondata.damage
            //ReducePierce();
        }
        else if(col.CompareTag("Prop"))
        {
            if(col.gameObject.TryGetComponent(out BreakableProps breakable))
            {
                breakable.TakeDamage(GetCurrentDamage());
            }
            
        }
    }



}
