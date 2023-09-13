using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BooksBehavior : ProjectileWepBehaviour
{
    private new void Start()
    {
        //Note: Since the book projectiles are parented under a prefab root that has no behavior,
        //      the destroying of the gameobject is handled by BookController instead.

        //Do nothing, don't call Destroy()
    }


    // protected override void Start()
    // {
    //     audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    //     audioManager.PlaySound(audioManager.garlic);
    //     base.Start();
    //     //kc = FindObjectOfType<KnifeController>();
    //     markedEnemies = new List<GameObject>();
    // }
}
