using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerStats : MonoBehaviour
{

    //Audio stuff
    AudioManager audioManager;
    // For loading the correct character (and their data) into the scene
    CharacterScriptableObject characterData;


// CURRENT stats
    // These are essential as we do not want to write anything to the variables of the actual ScriptableObjects
    // e.g changes to damage, health and movement speed due to skills should only exist CURRENTLY
        // Feel free to unhide any stat so we can see if recovery works outside of debug inspector
    float currentMoveSpeed;
    float currentHealth;
    float currentRecovery;
    float currentMight;
    float currentProjectileSpeed;
    float currentMagnet;

    // Using properites to check stats
    #region  Current Stat Properties
    public float CurrentMoveSpeed
    {
        get { return currentMoveSpeed ; }
        // Whenever a new value is assigned to current health, the set block is executed
        set
        {
            // Checks if value of currentHealth is changing/has changed
            if(currentMoveSpeed != value)
            {
                currentMoveSpeed = value;

                if(GameManager.instance != null)
                {
                    GameManager.instance.currentMoveSpeedDisplay.text = "Move Speed: " + currentMoveSpeed;
                }

                // Updates the real time value of the stat in the UI
                // Add any additional logic to be executed when value changes
            }
        }
    }

    public float CurrentHealth
    {
        get { return currentHealth ; }
        // Whenever a new value is assigned to current health, the set block is executed
        set
        {
            // Checks if value of currentHealth is changing/has changed
            if(currentHealth != value)
            {
                currentHealth = value;

                if(GameManager.instance != null)
                {
                    GameManager.instance.currentHealthDisplay.text = "Health: " + currentHealth;
                }
                // Updates the real time value of the stat in the UI
                // Add any additional logic to be executed when value changes
            }
        }
    }

    public float CurrentRecovery
    {
        get { return currentRecovery ; }
        // Whenever a new value is assigned to current health, the set block is executed
        set
        {
            // Checks if value of currentHealth is changing/has changed
            if(currentRecovery != value)
            {
                currentRecovery = value;

                if(GameManager.instance != null)
                {
                    GameManager.instance.currentRecoveryDisplay.text = "Recovery: " + currentRecovery;
                }
                // Updates the real time value of the stat in the UI
                // Add any additional logic to be executed when value changes
            }
        }
    }

    public float CurrentMight
    {
        get { return currentMight ; }
        // Whenever a new value is assigned to current health, the set block is executed
        set
        {
            // Checks if value of currentHealth is changing/has changed
            if(currentMight != value)
            {
                currentMight = value;

                if(GameManager.instance != null)
                {
                    GameManager.instance.currentMightDisplay.text = "Might: " + currentMight;
                }
                // Updates the real time value of the stat in the UI
                // Add any additional logic to be executed when value changes
            }
        }
    }

    public float CurrentMagnet
    {
        get { return currentMagnet ; }
        // Whenever a new value is assigned to current health, the set block is executed
        set
        {
            // Checks if value of currentHealth is changing/has changed
            if(currentMagnet != value)
            {
                currentMagnet = value;

                if(GameManager.instance != null)
                {
                    GameManager.instance.currentMagnetDisplay.text = "Magnet: " + currentMagnet;
                }
                // Updates the real time value of the stat in the UI
                // Add any additional logic to be executed when value changes
            }
        }
    }

    public float CurrentProjectileSpeed
    {
        get { return currentProjectileSpeed ; }
        // Whenever a new value is assigned to current health, the set block is executed
        set
        {
            // Checks if value of currentHealth is changing/has changed
            if(currentProjectileSpeed != value)
            {
                currentProjectileSpeed= value;

                if(GameManager.instance != null)
                {
                    GameManager.instance.currentProjectileSpeedDisplay.text = "Projectile Speed: " + currentProjectileSpeed;
                }
                // Updates the real time value of the stat in the UI
                // Add any additional logic to be executed when value changes
            }
        }
    }

    #endregion

    // // Starting Weapon
    // public List<GameObject> spawnedWeapons;


    // Player Exp and Levels
    [Header("Experience/Level")]
    public int experience = 0;
    public int level = 1;
    public int experienceCap;

    public int levelCap = 9;



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

    [Header("UI")]
    public Image healthBar;
    public Image expBar;
    public TMP_Text levelText;



    // MAKE SURE WE ADD THE NECESSARY HEADERS ABOVE THE LISTS BELOW
    // e.g If I-Frames is below List<LevelRange> below, the invincibility Duration setting
    // Will not show on the inspector


    public List<LevelRange> levelRanges;

    // Inventory Item slots
    InventoryManager inventory;
    public int weaponIndex;
    public int passiveItemIndex;

    // // Only use these to test weapon/passive spawns for multiple weapons (Check PlayerStats script to add)
    public GameObject secondWeaponTest;

    public GameObject firstPassiveItemTest, secondPassiveItemTest;
   
    void Awake()
    {
        //Audio Stuff / sfx
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        // Be sure to set characterData before the characterStats, 
        // or it may cause a Null reference otherwise
        // Now, the characterScriptableObject is automatically assigned to PlayerStats component of player when the button is clicked from the Menu scene 
        // If starting from Game scene, be sure to assign it still
        characterData = CharacterSelector.GetData();
        CharacterSelector.instance.DestroySingleton();

        // Must call the inventory before spawning starter weapon (SpawnWeapon below),
        // or it may cause a Null reference otherwise
        inventory = GetComponent<InventoryManager>();

        // Calls the stat properties from the CharacterScriptableObject
        CurrentMoveSpeed = characterData.MoveSpeed;
        CurrentHealth = characterData.MaxHealth;
        CurrentRecovery = characterData.Recovery;
        CurrentMight = characterData.Might;
        CurrentProjectileSpeed = characterData.ProjectileSpeed;
        CurrentMagnet = characterData.Magnet;

        // Spawning in the starter weapon
        SpawnWeapon(characterData.StartingWeapon);
        // // Only use this to test weapon spawns for multiple weapons
        // SpawnWeapon(secondWeaponTest);
        // SpawnPassiveItem(firstPassiveItemTest);
        // SpawnPassiveItem(secondPassiveItemTest);
    }
    

    void Start()
    {
        // Initialising exp cap
        // Starts player off with 0 exp cap, allowing them to level up immediately when they gain exp
        experienceCap = levelRanges[0].experienceCapIncrease;


        GameManager.instance.currentMoveSpeedDisplay.text = "Move Speed: " + currentMoveSpeed;
        GameManager.instance.currentHealthDisplay.text = "Health: " + currentHealth;
        GameManager.instance.currentRecoveryDisplay.text = "Recovery: " + currentRecovery;
        GameManager.instance.currentMightDisplay.text = "Might: " + currentMight;
        GameManager.instance.currentMagnetDisplay.text = "Magnet: " + currentMagnet;
        GameManager.instance.currentProjectileSpeedDisplay.text = "Projectile Speed: " + currentProjectileSpeed;
    
        UpdateHealthBar();
        UpdateExpBar();
        UpdateLevelText();
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

        Recover();
    }

    public void IncreaseExperience(int amount)
    {
        if(level >= levelCap)
        {
            return;
        } 

        experience += amount;

        LevelUpChecker();

        UpdateExpBar();


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

            UpdateLevelText();

            // Enable level up screen on level up
            GameManager.instance.StartLevelUp();
        }
    }

    void UpdateExpBar()
    {
        // float allows decimals when filling exp bar
        expBar.fillAmount = (float)experience / experienceCap;
    }

    void UpdateLevelText()
    {
        levelText.text = "LV: " + level.ToString();
    }


    public void TakeDamage(float dmg)
    {
        // If player is not invincible, take damage and gain i-frames
        if(!isInvincible)
        {
            CurrentHealth -= dmg;

            invincibilityTimer = invincibilityDuration;
            isInvincible = true;

            if(CurrentHealth <= 0)
            {
                Defeated();
            }

            UpdateHealthBar();
        }

        
    }

    void UpdateHealthBar()
    {
        healthBar.fillAmount = currentHealth / characterData.MaxHealth;
    }

    public void Defeated()
    {
        //Destroy(gameObject);
        Debug.Log("Oooofff!!!");

        //Audio Stuff, SFX
        audioManager.PlaySound(audioManager.death);

        if(!GameManager.instance.isGameOver)
        {
            GameManager.instance.GameOver();

            audioManager.Music.clip = audioManager.gameOverMusic;
            audioManager.Music.Play();
        }
    }

    ///// ***STAT INCREASES DUE TO ITEM PICK-UPS HERE BELOW*** /////
    ///

    public void RestoreHealth(float amount)
    {
        //Only causes heal if not at max health
        if(CurrentHealth < characterData.MaxHealth)
        {
            CurrentHealth += amount;

            // If the health healed casuses current health to exceed the Max, set the health to Max
            if(CurrentHealth > characterData.MaxHealth)
            {
                CurrentHealth = characterData.MaxHealth;
            }

        }
        
    }

    public void Recover()
    {
        //Only causes heal if not at max health
        if(CurrentHealth < characterData.MaxHealth)
        {
            Debug.Log("Recovering Health");
            CurrentHealth += CurrentRecovery * Time.deltaTime;

            // If the health healed casuses current health to exceed the Max, set the health to Max
            if(CurrentHealth > characterData.MaxHealth)
            {
                CurrentHealth = characterData.MaxHealth;
            }

        }
        
    }

    public void SpawnWeapon(GameObject weapon)
    {

        // Checks for available slots, return if so
        // -1 due to index 0 of array/list
        if(weaponIndex >= inventory.weaponSlots.Count - 1)
        {
            Debug.LogError("Inventory slots already full");
            return;
        }

        // Spawns the starter weapon
        GameObject spawnedWeapon = Instantiate(weapon, transform.position, Quaternion.identity);
        // The SetParent method makes the weapon a child of the Player in the hierarchy
        spawnedWeapon.transform.SetParent(transform);
        // // Then, we add it to the list ( notice spawnedWeapon(s) )
        // spawnedWeapons.Add(spawnedWeapon);

        // Add weapon to its inventory slot
        inventory.AddWeapon(weaponIndex, spawnedWeapon.GetComponent<AutoWepController>());

        // Increment wep index, so each wep is assigned a diff slot
        // We must increment only after adding the wep
        // As the Array/List starts from index 0
        weaponIndex++;

    }

    public void SpawnPassiveItem(GameObject passiveItem)
    {

        // Checks for available slots, return if so
        // -1 due to index 0 of array/list
        if(passiveItemIndex >= inventory.passiveItemSlots.Count - 1)
        {
            Debug.LogError("Inventory slots already full");
            return;
        }

        // Spawns the starter weapon
        GameObject spawnedPassiveItem = Instantiate(passiveItem, transform.position, Quaternion.identity);
        // The SetParent method makes the weapon a child of the Player in the hierarchy
        spawnedPassiveItem.transform.SetParent(transform);
        // // Then, we add it to the list ( notice spawnedWeapon(s) )
        // spawnedWeapons.Add(spawnedWeapon);

        // Add weapon to its inventory slot
        inventory.AddPassiveItem(passiveItemIndex, spawnedPassiveItem.GetComponent<PassiveItem>());

        // Increment wep index, so each wep is assigned a diff slot
        // We must increment only after adding the wep
        // As the Array/List starts from index 0
        passiveItemIndex++;

    }
}
