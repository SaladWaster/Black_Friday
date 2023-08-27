using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{

    public CharacterScriptableObject characterData;


    // CURRENT stats
    // These are essential as we do not want to write anything to the variables of the actual ScriptableObjects
    // e.g changes to damage, health and movement speed due to skills should only exist CURRENTLY
    float currentMoveSpeed;
    float currentHealth;
    float currentRecovery;
    float currentMight;
    float currentProjectileSpeed;


    // Player Exp and Levels
    [Header("Experience/Level")]
    public int experience = 0;
    public int level = 1;
    public int experienceCap;



    // Nested class (Class within another class)
    // We can separate it into a different script
    // However, we can also leave it in the Playerstats script to help with encapsulation and readability
    // We will leave it in this script for now

    // Class for defining level range
    // System.Seiralizable allows the class to be serialised by unity
    // Its fields are visible and editable in the inspector, and can be saved or loaded from files
    [System.Serializable]
    public class LevelRange
    {
        public int startLevel;
        public int endLevel;
        public int experienceCapIncrease;
    }

    // SHADOW I-FRAMES????!!!
    [Header("I-Frames")]
    public float invincibilityDuration;
    float invincibilityTimer;
    bool isInvincible;

    // MAKE SURE WE ADD THE NECESSARY HEADERS ABOVE THE LISTS BELOW
    // e.g If I-Frames is below List<LevelRange> below, the invincibility Duration setting
    // Will not show on the inspector


    public List<LevelRange> levelRanges;
   
    void Awake()
    {
        // Calls the stat properties from the CharacterScriptableObject
        currentMoveSpeed = characterData.MoveSpeed;
        currentHealth = characterData.MaxHealth;
        currentRecovery = characterData.Recovery;
        currentMight = characterData.Might;
        currentProjectileSpeed = characterData.ProjectileSpeed;
    }
    

    void Start()
    {
        // Initialising exp cap
        // Starts player off with 0 exp cap, allowing them to level up immediately when they gain exp
        experienceCap = levelRanges[0].experienceCapIncrease;
    }

    // Repeatedly check invincibility timer
    void Update()
    {
        if(invincibilityTimer > 0)
        {
            invincibilityTimer -= Time.deltaTime;
        }
        // We use else if to double check if the player is invincible when timer hits 0
        // Before we set isInvincible back to false
        else if (isInvincible)
        {
            isInvincible = false;
        }
    }

    public void IncreaseExperience(int amount)
    {
        experience += amount;

        LevelUpChecker();
    }

    void LevelUpChecker()
    {
        // Check if player exp >= current exp cap
        // If so, increase level and reduce exp by current cap
        if(experience >= experienceCap)
        {
            level++;
            experience -= experienceCap;


            int experienceCapIncrease = 0;

            // For each level up, increase current level cap based on current exp range
            foreach (LevelRange range in levelRanges)
            {
                
                
                if(level >= range.startLevel && level <= range.endLevel)
                {
                    experienceCapIncrease = range.experienceCapIncrease;
                    break;
                }
            }
            experienceCap += experienceCapIncrease;
        }
    }


    public void TakeDamage(float dmg)
    {
        // If player is not invincible, take damage and gain i-frames
        if(!isInvincible)
        {
            currentHealth -= dmg;

            invincibilityTimer = invincibilityDuration;
            isInvincible = true;

            if(currentHealth <= 0)
            {
                Defeated();
            }
        }

        
    }

    public void Defeated()
    {
        //Destroy(gameObject);
        Debug.Log("Oooofff!!!");
    }
}
