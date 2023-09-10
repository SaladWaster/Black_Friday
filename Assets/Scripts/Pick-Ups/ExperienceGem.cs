using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceGem : PickUp
{
    public int experienceGranted;

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
        player.IncreaseExperience(experienceGranted);

        // We have moved the Destroy function to the PickUp parent script

    }


}
