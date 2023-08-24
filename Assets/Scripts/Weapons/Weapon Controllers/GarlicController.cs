using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarlicController : AutoWepController
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    protected override void Attack()
    {
        base.Attack();
        GameObject spawnedGarlic = Instantiate(weaponData.Prefab);
        spawnedGarlic.transform.position = transform.position; // Assigns position to be same as this object, which is parented to the player
        spawnedGarlic.transform.parent = transform; // Updated to lastMovedVector
    
    }
    
}
