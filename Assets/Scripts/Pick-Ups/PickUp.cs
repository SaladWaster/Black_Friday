using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour, ICollectable
{

    //SFX audio Manager
    AudioManager audioManager;
    
    PlayerStats player;
    protected bool hasBeenCollected = false;

    public bool isAttracted = false;




   Rigidbody2D rb; 

    private void Awake()
    {
        player = FindObjectOfType<PlayerStats>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void FixedUpdate()
    {
        if(isAttracted)
        {
            
            // // Vector 2d that points to player direction from item position and normalize it
            Vector2 targetDirection = (player.transform.position - transform.position).normalized;
            //rb.AddForce(forceDirection * 300);
            rb.velocity = new Vector2(targetDirection.x, targetDirection.y) * 10f;
        }
    }

    public void SetTarget(Vector3 position)
    {
        player.transform.position = position;
        isAttracted = true;
    }

    public virtual void Collect()
    {
        audioManager.PlaySound(audioManager.pickUp);
        hasBeenCollected = true;
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        // If the exp gem collides with a hitbox under a "Player"
        if(col.CompareTag("Player"))
        {
            Collect();
            // Destroy on pick-up to prevent multiple pick-ups
            Destroy(gameObject);
        }

        
    }
}
