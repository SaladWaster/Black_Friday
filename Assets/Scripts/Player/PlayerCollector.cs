using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollector : MonoBehaviour
{

    // Having this script allows us to check for every other collectable pick-up item
    // Instead of having multiple scripts to to repeatedly check for onTriggerEnter2D
    // For each collectable pick-up item (such as exp gem)

    // We attach this script to the player character

    void OnTriggerEnter2D(Collider2D col)
    {
        // Check if other game object(s) have ICollectible interface
        if(col.gameObject.TryGetComponent(out ICollectable collectable))
        {
            // If yes, call Collect method
            collectable.Collect();
        }
    }

}
