using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceGem : PickUp, ICollectable
{
    public int experienceGranted;

    public void Collect()
    {
        // Reference the PlayerStats script
        PlayerStats player = FindObjectOfType<PlayerStats>();
        player.IncreaseExperience(experienceGranted);

        // We have moved the Destroy function to the PickUp parent script

    }


}
