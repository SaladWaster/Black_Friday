using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public EnemyScriptableObject enemyData;
    Transform player;
    public float despawnDistance;


    // CURRENT stats
    // These are essential as we do not want to write anything to the variables of the actual ScriptableObjects
    // e.g changes to damage, health and movement speed due to skills should only exist CURRENTLY
    [HideInInspector]
    public float currentMoveSpeed;
    [HideInInspector]
    public float currentHealth;
    [HideInInspector]
    public float currentDamage;

    [SerializeField] private DamageFlash flashEffect;

    
    // Awake calls before start, is more reliable
    void Awake()
    {
        // Calls the stat properties from the EnemyScriptableObject
        currentMoveSpeed = enemyData.MoveSpeed;
        currentHealth = enemyData.MaxHealth;
        currentDamage = enemyData.Damage;
    }

    void Start()
    {
        player = FindObjectOfType<PlayerStats>().transform;
    }
    
    void Update()
    {
        if(Vector2.Distance(transform.position, player.position)>=despawnDistance)
        {
            ReturnEnemy();
        }
    }

    public void TakeDamage(float dmg)
    {
        currentHealth -= dmg;

        flashEffect.Flash();

        if(currentHealth <= 0)
        {
            Defeated();
        }
    }


    public void Defeated()
    {
        Destroy(gameObject);
    }


    // Use OnCollision STAY 2D, so that the contact only triggers the event once until the player moves away
    public void OnCollisionStay2D(Collision2D col)
    {
        
        // References the script from collided collider and deal damage using TakeDamage() from PlayerStats script
        
        // MAKE SURE TO ASSIGN TAGS TO COLLISION OBJECTS ***
        // CLICK THE OBJECT IN THE HIERACHY, THEN IN THE INSPECTOR TOP SET THE TAG e.g Tag "Player"
        if(col.gameObject.CompareTag("Player"))
        {
            // Debug.Log("Collision test");
            PlayerStats player = col.gameObject.GetComponent<PlayerStats>();
            player.TakeDamage(currentDamage); // Use current damage, as we may add damage modifiers later, rather than weapondata.damage
        }
    }

    private void OnDestroy()
    {
        EnemySpawner st = FindObjectOfType<EnemySpawner>();
        if(st) st.OnEnemyDefeated();

    }

    void ReturnEnemy()
    {
        // Remove the unnecessary line that references relativeSpawnPoints
        EnemySpawner st = FindObjectOfType<EnemySpawner>();
        
        transform.position = player.position + st.relativeSpawnPoints[Random.Range(0, st.relativeSpawnPoints.Count)].position;
    }

    
    
}
