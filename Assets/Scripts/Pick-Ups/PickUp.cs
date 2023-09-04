using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D col)
    {
        // If the exp gem collides with a hitbox under a "Player"
        if(col.CompareTag("Player"))
        {
            // Destroy on pick-up to prevent multiple pick-ups
            Destroy(gameObject);
        }

        
    }
}
