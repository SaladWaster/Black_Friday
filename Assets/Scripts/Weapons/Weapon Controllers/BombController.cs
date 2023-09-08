using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : AutoWepController
{
    protected override void Attack()
    {
        base.Attack();
        //Just plonk a bomb down lol
        GameObject spawnedBomb = Instantiate(weaponData.Prefab);
        spawnedBomb.transform.position = transform.position;
    }
}
