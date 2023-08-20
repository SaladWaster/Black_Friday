using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    Animator animator;

    public float currentHp = 200;



    [SerializeField] Transform targetDestination;
    [SerializeField] float speed;
    GameObject targetGameObject;
    Rigidbody2D rgbd2d;

    // FOR COLLISIONS ///////////
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>(); // Create ray casts (RaycastHit2D) from the rigidbody to check for collisions between the Enemy collider and other colliders


    private void Awake()
    {
        rgbd2d = GetComponent<Rigidbody2D>();
        targetGameObject = targetDestination.gameObject;
    }

    // Update called by time, instead of by frames
    private void FixedUpdate()
    {
        if (currentHp <=0)
        {
            print("Enemy is dead");
            Defeated();
        }


        Vector2 direction = (targetDestination.position - transform.position).normalized;
        rgbd2d.velocity = direction * speed;

    }


    private void OnCollisionStay2D(Collision2D collision)
    {
        //if (collision.gameObject.GetComponent<Character>())
        if (collision.gameObject == targetGameObject)
        {
            Attack();
        }
    }

    private void Attack()
    {
        //print("Attacking the character!");
    }



    
    public void TakeDamage(float hitvalue)
    {
        currentHp -= hitvalue;

        if (currentHp <=0)
        {
            print("Enemy is dead");
            Defeated();
        }
    }


    

    public void Defeated()
    {
        animator.SetTrigger("defeated");
        // Dieded(gameObject);

    }

    public void Dieded()
    {
        Destroy(gameObject);
    }



}
