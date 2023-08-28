using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarlicBehaviour : MeleeWepBehaviour
{

    // We create a list of marked enemies
    // The garlic only attacks enemies who have never touched it before once
    // Otherwise, enemies will repeatedly take damage from the garlic (Too OP)
    List<GameObject> markedEnemies;

    protected override void Start()
    {
        base.Start();
        //kc = FindObjectOfType<KnifeController>();
        markedEnemies = new List<GameObject>();
    }


    //By overriding, we can individually change the behaviour of each wep
    protected override void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Enemy") && !markedEnemies.Contains(col.gameObject))
        {
            EnemyStats enemy = col.GetComponent<EnemyStats>();
            enemy.TakeDamage(GetCurrentDamage()); // Use current damage, as we may add damage modifiers later, rather than weapondata.damage
                                                    // Updated with GetCurrentDamage, we accounts for modifiers
            
            markedEnemies.Add(col.gameObject); // Marked enemies will no longer take another instance of damage from current garlic
                                                // They will still take damage from the next spawn
        }
        else if(col.CompareTag("Prop") && !markedEnemies.Contains(col.gameObject))
        {
            if(col.gameObject.TryGetComponent(out BreakableProps breakable))
            {
                breakable.TakeDamage(GetCurrentDamage());
                
                markedEnemies.Add(col.gameObject);
            }

        }
    }
}
