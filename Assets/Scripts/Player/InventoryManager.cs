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


    // Method that assigns Weapon to a specified slot index
    // This will be useful when we implement wep evolution and passives (AddPassiveItem below)
    public void AddWeapon(int slotIndex, AutoWepController weapon)
    {
        weaponSlots[slotIndex] = weapon;
        weaponLevels[slotIndex] = weapon.weaponData.Level;
        weaponUISlots[slotIndex].enabled = true;    // Enables image component of UI, preventing unused slots from displaying
        weaponUISlots[slotIndex].sprite = weapon.weaponData.Icon;
    }

    public void AddPassiveItem(int slotIndex, PassiveItem passiveItem)
    {
        passiveItemSlots[slotIndex] = passiveItem;
        passiveItemLevels[slotIndex] = passiveItem.passiveItemData.Level;
        passiveItemUISlots[slotIndex].enabled = true;    // Enables image component of UI, preventing unused slots from displaying
        passiveItemUISlots[slotIndex].sprite = passiveItem.passiveItemData.Icon;
    }

    public void LevelUpWeapon(int slotIndex)
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
        }
    }

    public void LevelUpPassiveItem(int slotIndex)
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
        }
    }
}
