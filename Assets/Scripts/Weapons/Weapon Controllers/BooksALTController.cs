using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BooksALTController : AutoWepController
{
    private GameObject spawnedBooks;

    // Start is called before the first frame update
    protected override void Start()
    {
        //base start func will retrieve player controller and set the cooldown
        base.Start();
    }

    

    protected override void Attack()
    {
        //prints a thing and resets cooldown (might need to delay cd until when the books expire.)
        base.Attack();
        spawnedBooks = Instantiate(weaponData.Prefab, pm.transform);

    }
}
