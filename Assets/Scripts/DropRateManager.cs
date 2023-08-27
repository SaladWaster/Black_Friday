using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropRateManager : MonoBehaviour
{

    [System.Serializable]
    public class Drops 
    {
        public string name;
        public GameObject itempPrefab;
        public float dropRate;

    }

    // List to store drop rates for all droppable items
    public List<Drops> drops;


    // This causes the items to drop/spawn just before an object is destroyed
    // The script is thus self-contained and does not have any dependencies
    // We can attach this script to any object/prefab easily and modify rates in the inspector
    // e.g Slime prefab
    void OnDestroy()
    {

        // Specify UnityEngine to prevent use of other random number generators
        // Range of 100%
        float randomNumber = UnityEngine.Random.Range(0f, 100f);
        List<Drops> possibleDrops = new List<Drops>();

        foreach (Drops rate in drops)
        {
            
            // If the number generated/rolled is below the dropRate, it will spawn
            if(randomNumber <= rate.dropRate)
            {
                possibleDrops.Add(rate);
            }

            // Checks for how many possible drops can appear, and RNG to choose
            // If we want priority, we can select the item with highest dropRate instead
            if(possibleDrops.Count >0)
            {
                Drops drops = possibleDrops[UnityEngine.Random.Range(0, possibleDrops.Count)];
                Instantiate(drops.itempPrefab, transform.position, Quaternion.identity);
            }
            

        }
    }
}
