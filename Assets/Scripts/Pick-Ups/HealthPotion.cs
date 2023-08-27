using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : MonoBehaviour, ICollectable
{
    public int healthRestored;

    public void Collect()
    {
        // Reference the PlayerStats script
        PlayerStats player = FindObjectOfType<PlayerStats>();
        player.RestoreHealth(healthRestored);

        // Destroy on pick-up to prevent multiple pick-ups
        Destroy(gameObject);
    }
}
