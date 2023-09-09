using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Necessary for all things UI

public class InventoryManager : MonoBehaviour
{
    // Lists of maximum 6 inventory slots (We can adjust this number whenever)
    // We also use an array to store levels instead of lists, as arrays are more efficient for storing numbers
    // We do this as Unity does not show dictionaries in the inspector by default
    public List<AutoWepController> weaponSlots = new List<AutoWepController>(6);
    public int[] weaponLevels = new int[6];
    public List<Image> weaponUISlots = new List<Image>(6);

    public List<PassiveItem> passiveItemSlots = new List<PassiveItem>(6);
    public int[] passiveItemLevels = new int[6];
    public List<Image> passiveItemUISlots = new List<Image>(6);

    [System.Serializable]
    public class WeaponUpgrade
    {
        public int weaponUpgradeIndex;
        public GameObject initialWeapon;
        public WeaponScriptableObject weaponData;
    }

    [System.Serializable]
    public class PassiveItemUpgrade
    {
        public int passiveItemUpgradeIndex;
        public GameObject initialPassiveItem;
        public PassiveItemScriptableObject passiveItemData;
    }

    [System.Serializable]
    public class UpgradeUI
    {
        public Text upgradeNameDisplay;
        public Text upgradeDescriptionDisplay;
        public Image upgradeIcon;
        public Button upgradeButton;
    }

    public List<WeaponUpgrade> weaponUpgradeOptions = new List<WeaponUpgrade>();
    public List<PassiveItemUpgrade> passiveItemUpgradeOptions = new List<PassiveItemUpgrade>();
    public List<UpgradeUI> upgradeUIOptions = new List<UpgradeUI>();

    PlayerStats player;

    void Start()
    {
        player = GetComponent<PlayerStats>();
    }

    // Method that assigns Weapon to a specified slot index
    // This will be useful when we implement wep evolution and passives (AddPassiveItem below)
    public void AddWeapon(int slotIndex, AutoWepController weapon)
    {
        weaponSlots[slotIndex] = weapon;
        weaponLevels[slotIndex] = weapon.weaponData.Level;
        weaponUISlots[slotIndex].enabled = true;    // Enables image component of UI, preventing unused slots from displaying
        weaponUISlots[slotIndex].sprite = weapon.weaponData.Icon;
    
        if (GameManager.instance != null && GameManager.instance.choosingUpgrade)
        {
            GameManager.instance.EndLevelUp();
        }
    }

    public void AddPassiveItem(int slotIndex, PassiveItem passiveItem)
    {
        passiveItemSlots[slotIndex] = passiveItem;
        passiveItemLevels[slotIndex] = passiveItem.passiveItemData.Level;
        passiveItemUISlots[slotIndex].enabled = true;    // Enables image component of UI, preventing unused slots from displaying
        passiveItemUISlots[slotIndex].sprite = passiveItem.passiveItemData.Icon;
    
        if (GameManager.instance != null && GameManager.instance.choosingUpgrade)
        {
            GameManager.instance.EndLevelUp();
        }
    }

    public void LevelUpWeapon(int slotIndex, int upgradeIndex)
    {
        // Check if inventory contains weapon in specified slot index
        // If yes
        if(weaponSlots.Count > slotIndex)
        {
            // Retrieve AutoWepController script attached to the wep game object
            // Store it in local variable "weapon" for reference
            AutoWepController weapon = weaponSlots[slotIndex];

            // Checks if wep has next level
            if(!weapon.weaponData.NextLevelPrefab)
            {
                Debug.LogError("No next level for current wep yet" + weapon.name);
                return;
            }

            GameObject upgradedWeapon = Instantiate(weapon.weaponData.NextLevelPrefab, transform.position, Quaternion.identity);
            // The SetParent method makes the weapon a child of the Player in the hierarchy
            upgradedWeapon.transform.SetParent(transform);

            AddWeapon(slotIndex, upgradedWeapon.GetComponent<AutoWepController>());
            // Destroy previous version of wep
            Destroy(weapon.gameObject);

            // Ensure we have the correct wep level
            weaponLevels[slotIndex] = upgradedWeapon.GetComponent<AutoWepController>().weaponData.Level;
        
            weaponUpgradeOptions[upgradeIndex].weaponData = upgradedWeapon.GetComponent<AutoWepController>().weaponData;
            
            if (GameManager.instance != null && GameManager.instance.choosingUpgrade)
            {
                GameManager.instance.EndLevelUp();
            }
        }
    }

    public void LevelUpPassiveItem(int slotIndex, int upgradeIndex)
    {
        // Check if inventory contains weapon in specified slot index
        // If yes
        if(passiveItemSlots.Count > slotIndex)
        {
            // Retrieve AutoWepController script attached to the wep game object
            // Store it in local variable "weapon" for reference
            PassiveItem passiveItem = passiveItemSlots[slotIndex];

             // Checks if passive has next level
            if(!passiveItem.passiveItemData.NextLevelPrefab)
            {
                Debug.LogError("No next level for current passive yet" + passiveItem.name);
                return;
            }

            GameObject upgradedPassiveItem = Instantiate(passiveItem.passiveItemData.NextLevelPrefab, transform.position, Quaternion.identity);
            // The SetParent method makes the weapon a child of the Player in the hierarchy
            upgradedPassiveItem.transform.SetParent(transform);

            AddPassiveItem(slotIndex, upgradedPassiveItem.GetComponent<PassiveItem>());
            // Destroy previous version of wep
            Destroy(passiveItem.gameObject);

            // Ensure we have the correct passive level
            passiveItemLevels[slotIndex] = upgradedPassiveItem.GetComponent<PassiveItem>().passiveItemData.Level;
        
            passiveItemUpgradeOptions[upgradeIndex].passiveItemData = upgradedPassiveItem.GetComponent<PassiveItem>().passiveItemData;
            
            if (GameManager.instance != null && GameManager.instance.choosingUpgrade)
            {
                GameManager.instance.EndLevelUp();
            }
        
        }
    }

    void ApplyUpgradeOptions()
    {

        List<WeaponUpgrade> availableWeaponUpgrades = new List<WeaponUpgrade>(weaponUpgradeOptions);
        List<PassiveItemUpgrade> availablePassiveItemUpgrades = new List<PassiveItemUpgrade>(passiveItemUpgradeOptions);

        foreach (var upgradeOption in upgradeUIOptions)
        {

            // If no avail upgrades left, return nothing
            if(availableWeaponUpgrades.Count == 0 && availablePassiveItemUpgrades.Count == 0)
            {
                return;
            }

            int upgradeType;

            // If no avail wep/passive upgrades left, offer the other's upgrades
            if(availableWeaponUpgrades.Count == 0)
            {
                upgradeType = 2;
            }
            else if (availablePassiveItemUpgrades.Count == 0)
            {
                upgradeType = 1;
            }
            else
            {
                // First value is included, 2nd value is the excluded from the range
                upgradeType = Random.Range(1,3);
            }

            if(upgradeType == 1)
            {
                WeaponUpgrade chosenWeaponUpgrade = availableWeaponUpgrades[Random.Range(0, availableWeaponUpgrades.Count)];

                availableWeaponUpgrades.Remove(chosenWeaponUpgrade);

                if(chosenWeaponUpgrade != null)
                {

                    EnableUpgradeUI(upgradeOption);

                    bool newWeapon = false;

                    for (int i = 0; i < weaponSlots.Count; i++)
                    {
                        if(weaponSlots[i] != null && weaponSlots[i].weaponData == chosenWeaponUpgrade.weaponData)
                        {
                            newWeapon = false;

                            if(!newWeapon)
                            {

                                // If no further upgrades, do not make the UI show and break
                                if(!chosenWeaponUpgrade.weaponData.NextLevelPrefab)
                                {
                                    DisableUpgradeUI(upgradeOption);
                                    break;
                                }


                                // Apply button functionality
                                upgradeOption.upgradeButton.onClick.AddListener(() => LevelUpWeapon(i, chosenWeaponUpgrade.weaponUpgradeIndex));
                                
                                // Sets description and name to be the upgraded one
                                upgradeOption.upgradeDescriptionDisplay.text = chosenWeaponUpgrade.weaponData.NextLevelPrefab.GetComponent<AutoWepController>().weaponData.Description;
                                upgradeOption.upgradeNameDisplay.text = chosenWeaponUpgrade.weaponData.NextLevelPrefab.GetComponent<AutoWepController>().weaponData.Name;
                            }

                            break; // Exits the loop, allowing upgrade option to be applied
                        }
                        else
                        {
                            newWeapon = true;
                        }
                    
                    }

                    if(newWeapon) // Spawns new wep
                    {
                        // Apply button functionality
                        upgradeOption.upgradeButton.onClick.AddListener(() => player.SpawnWeapon(chosenWeaponUpgrade.initialWeapon));
                        
                        // Sets description and name of base form
                        upgradeOption.upgradeDescriptionDisplay.text = chosenWeaponUpgrade.weaponData.Description;
                        upgradeOption.upgradeNameDisplay.text = chosenWeaponUpgrade.weaponData.Name;
                    }

                    upgradeOption.upgradeIcon.sprite = chosenWeaponUpgrade.weaponData.Icon;
                }
            }
            else if(upgradeType == 2)
            {
                PassiveItemUpgrade chosenPassiveItemUpgrade = availablePassiveItemUpgrades[Random.Range(0, availablePassiveItemUpgrades.Count)];

                availablePassiveItemUpgrades.Remove(chosenPassiveItemUpgrade);

                if(chosenPassiveItemUpgrade != null)
                {

                    EnableUpgradeUI(upgradeOption);

                    bool newPassiveItem = false;

                    for (int i = 0; i < passiveItemSlots.Count; i++)
                    {
                        if(passiveItemSlots[i] != null && passiveItemSlots[i].passiveItemData == chosenPassiveItemUpgrade.passiveItemData)
                        {
                            newPassiveItem = false;

                            if(!newPassiveItem)
                            {

                                // If no further upgrades, do not make the UI show and break
                                if(!chosenPassiveItemUpgrade.passiveItemData.NextLevelPrefab)
                                {
                                    DisableUpgradeUI(upgradeOption);
                                    break;
                                }


                                // Apply button functionality
                                upgradeOption.upgradeButton.onClick.AddListener(() => LevelUpPassiveItem(i, chosenPassiveItemUpgrade.passiveItemUpgradeIndex));
                            
                                // Sets description and name to be the upgraded one
                                upgradeOption.upgradeDescriptionDisplay.text = chosenPassiveItemUpgrade.passiveItemData.NextLevelPrefab.GetComponent<PassiveItem>().passiveItemData.Description;
                                upgradeOption.upgradeNameDisplay.text = chosenPassiveItemUpgrade.passiveItemData.NextLevelPrefab.GetComponent<PassiveItem>().passiveItemData.Name;
                            
                            }

                            break; // Exits the loop, allowing upgrade option to be applied
                        }
                        else
                        {
                            newPassiveItem = true;
                        }
                    
                    }

                    if(newPassiveItem) // Spawns new passive
                    {
                        // Apply button functionality
                        upgradeOption.upgradeButton.onClick.AddListener(() => player.SpawnPassiveItem(chosenPassiveItemUpgrade.initialPassiveItem));
                    
                        // Sets description and name of base form
                        upgradeOption.upgradeDescriptionDisplay.text = chosenPassiveItemUpgrade.passiveItemData.Description;
                        upgradeOption.upgradeNameDisplay.text = chosenPassiveItemUpgrade.passiveItemData.Name;
                    }

                    upgradeOption.upgradeIcon.sprite = chosenPassiveItemUpgrade.passiveItemData.Icon;
                }
            }
        }
    }

    void RemoveUpgradeOptions()
    {
        foreach (var upgradeOption in upgradeUIOptions)
        {
            // Remove button functionality
            upgradeOption.upgradeButton.onClick.RemoveAllListeners();
            DisableUpgradeUI(upgradeOption);
        }
    }

    void RemoveAndApplyUpgrades()
    {
        RemoveUpgradeOptions();
        ApplyUpgradeOptions();
    }

    void DisableUpgradeUI(UpgradeUI ui)
    {
        ui.upgradeNameDisplay.transform.parent.gameObject.SetActive(false);
    }

    void EnableUpgradeUI(UpgradeUI ui)
    {
        ui.upgradeNameDisplay.transform.parent.gameObject.SetActive(true);
    }


}
