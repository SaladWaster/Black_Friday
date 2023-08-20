using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{    

    public Collider2D swordCollider;
    Vector2 rightAttackOffset;

    // Attack Damage
    public float damage = 3; 
    // float timeToAttack = 2f;
    // float timer;


    // Start is called before the first frame update
    void Start()
    {
        swordCollider = GetComponent<Collider2D>();
        rightAttackOffset = transform.position;
        swordCollider.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        // timer -= Time.deltaTime;
        // if (timer< 0f)
        // {
        //     Attack();
        // }
    }

    // private void Attack()
    // {
    //     print("I am attacking!");
    //     timer = timeToAttack;
    // }

    public void AttackLeft()
    {
        print("Attack Left!");
        swordCollider.enabled = true; // Enable the collider when the player enters the attack state.
        transform.localPosition = new Vector2(rightAttackOffset.x * -1, rightAttackOffset.y); // Programmatically put the collider to the left when the Player is facing left
        
    }
    public void AttackRight()
    {
        print("Attack Right!");
        swordCollider.enabled = true; // Enable the collider when the player enters the attack state.
        transform.localPosition = rightAttackOffset;
    }
    
	public void StopAttack()
    {
        swordCollider.enabled = false;
    }



    // Deals damage to a colliding enemy.
    public void OnTriggerEnter2D(Collider2D collision)
    {

        print("ouch! 0"); // Prints upon ANY hitbox collision with sword (including obstacles)

        /// THE NEXT SECTION is meant to be when the collision is only against Enemies

        // This part below isnt working...
        //if (collision.tag == "Enemy")
        if (collision.gameObject.GetComponent<Enemy>())
        {

            print("ouch! 1");
            Enemy enemy = collision.GetComponent<Enemy>();
            if (enemy)
            {
                //enemy.currentHp -= damage;
                enemy.TakeDamage(damage);
                print("ouch! 2");
            }
        }

        if (collision.tag == "Obstacles")
        {

            print("My FOOT!!!");
            
        }
    }

}
