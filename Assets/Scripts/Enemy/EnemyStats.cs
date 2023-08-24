using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public EnemyScriptableObject enemyData;

    // CURRENT stats
    // These are essential as we do not want to write anything to the variables of the actual ScriptableObjects
    // e.g changes to damage, health and movement speed due to skills should only exist CURRENTLY
    float currentMoveSpeed;
    float currentHealth;
    float currentDamage;

    
    // Awake calls before start, is more reliable
    void Awake()
    {
        // Calls the stat properties from the EnemyScriptableObject
        currentMoveSpeed = enemyData.MoveSpeed;
        currentHealth = enemyData.MaxHealth;
        currentDamage = enemyData.Damage;
    }

    public void TakeDamage(float dmg)
    {
        currentHealth -= dmg;

        if(currentHealth <= 0)
        {
            Defeated();
        }
    }


    public void Defeated()
    {
        Destroy(gameObject);
    }
    
}
