using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeController : AutoWepController
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    protected override void Attack()
    {
        base.Attack();
        GameObject spawnedKnife = Instantiate(weaponData.prefab);
        spawnedKnife.transform.position = transform.position; // Assigns position to be same as this object, which is parented to the player
        // spawnedKnife.GetComponent<KnifeBehaviour>().DirectionChecker(pm.movement); // movement from PlayerController, as reference to player movement direction
        spawnedKnife.GetComponent<KnifeBehaviour>().DirectionChecker(pm.lastMovedVector); // Updated to lastMovedVector
    
    }
    
    // // Update is called once per frame
    // void Update()
    // {
        
    // }
}
