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
}
