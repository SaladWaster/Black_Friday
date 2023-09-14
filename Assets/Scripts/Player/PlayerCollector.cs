using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollector : MonoBehaviour
{
    PlayerStats player;
    CircleCollider2D playerCollector;
    // public float pullSpeed;
    

    void Start()
    {
        player = FindObjectOfType<PlayerStats>();
        playerCollector = GetComponent<CircleCollider2D>();
    }

    void Update()
    {
        playerCollector.radius = player.CurrentMagnet;

       // transform.position = Vector2.MoveTowards(transform.position, player.transform.position, pullSpeed * Time.deltaTime);


    }

    // Having this script allows us to check for every other collectable pick-up item
    // Instead of having multiple scripts to to repeatedly check for onTriggerEnter2D
    // For each collectable pick-up item (such as exp gem)

    // We attach this script to the player character

    // ATM, movement of the object is dependent on the player in this script
    // It applies the force on the object TOWARDS the player
    // What we want instead is for the object to come to the player by ITSELF
    // This is prob dependent on the PICKUP script
    // We may have to call something from it, e.g collectable.FunctionName

    void OnTriggerEnter2D(Collider2D col)
    {
        // Check if other game object(s) have ICollectible interface
        //if(col.gameObject.TryGetComponent(out ICollectable collectable))
        if(col.gameObject.TryGetComponent<PickUp>(out PickUp pickup))
        {
            //collectable.

            // Magnet pull animation
            // Retrieves the Rigidbody2D component of the item
            Rigidbody2D rb = col.gameObject.GetComponent<Rigidbody2D>();

            pickup.SetTarget(transform.parent.position);
            // // Vector 2d that points to player direction from item position and normalize it
            // Vector2 forceDirection = (transform.position - col.transform.position).normalized;
            // rb.AddForce(forceDirection * pullSpeed);

            //col.gameObject.GetComponent<MoveItemToPlayer>().MoveToPlayer(transform);



            // If yes, call Collect method
            //collectable.Collect();
        }
    }

}
