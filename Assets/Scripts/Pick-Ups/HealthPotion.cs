using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : PickUp
{
    public int healthRestored;

    public override void Collect()
    {
        if(hasBeenCollected)
        {
            return;
        }
        else
        {
            // Executes the base implementation of the Collect method from PickUp class
            base.Collect();
        }

        // Reference the PlayerStats script
        PlayerStats player = FindObjectOfType<PlayerStats>();
        player.RestoreHealth(healthRestored);

    }
}
