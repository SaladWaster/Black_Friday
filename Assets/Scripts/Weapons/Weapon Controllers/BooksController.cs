using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BooksController : AutoWepController
{
    private bool attacking = false; //Flag for if the controller is mid-attack. Set to true when books are active.
    private GameObject spawnedBooks;
    private float attackDuration = 3f;
    private float currentAttackDuration;

    // Start is called before the first frame update
    protected override void Start()
    {
        //base start func will retrieve player controller and set the cooldown
        base.Start();
    }

    protected override void Update()
    {
        if (!attacking)
        {
            //if the books are not active run all the base cooldown management code
            base.Update();
        }
        else
        {
            currentAttackDuration -= Time.deltaTime;
            if(currentAttackDuration <= 0)
            {
                Destroy(spawnedBooks);
                attacking = false;
            }
        }
    }

    protected override void Attack()
    {
        //prints a thing and resets cooldown (might need to delay cd until when the books expire.)
        base.Attack();
        spawnedBooks = Instantiate(weaponData.Prefab, pm.transform);

        //TODO: begin counting down duration of the books
        attacking = true;
        currentAttackDuration = attackDuration;
    }
}
