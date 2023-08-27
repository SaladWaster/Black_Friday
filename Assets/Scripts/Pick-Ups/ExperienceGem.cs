using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceGem : MonoBehaviour, ICollectable
{
    public int experienceGranted;

    public void Collect()
    {
        // Reference the PlayerStats script
        PlayerStats player = FindObjectOfType<PlayerStats>();
        player.IncreaseExperience(experienceGranted);

        // Destroy on pick-up to prevent multiple pick-ups
        Destroy(gameObject);
    }
}
