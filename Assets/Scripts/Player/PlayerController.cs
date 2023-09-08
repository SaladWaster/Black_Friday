using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; // Import the InputSystem

public class PlayerController : MonoBehaviour
{
    [HideInInspector] public Vector2 movement;

    //Rigidbody2D rb;

    public ContactFilter2D movementFilter;
    //public float moveSpeed;
    public float collisionOffset = 0.05f;

    [HideInInspector] public float lastHorizontalVector;
    [HideInInspector] public float lastVerticalVector;
    [HideInInspector] public Vector2 lastMovedVector;

    //References the player character Scriptable Object for stats
    Rigidbody2D rb;
    // public CharacterScriptableObject characterData;
    PlayerStats player;


    // FOR COLLISIONS ///////////
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>(); // Create ray casts (RaycastHit2D) from the rigidbody to check for collisions between the Player's collider and other colliders

    // FOR ANIMATIONS ///////////
    Animator animator;
    SpriteRenderer spriteRenderer;


    // FOR ATTACKING ///////////
    public bool canMove = true;
    
    public SwordAttack swordAttack; // declare a public SwordAttack instance.

    public AutoWep1 autoAttack; // public AutoWep1 instance


    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<PlayerStats>();
        rb = GetComponent<Rigidbody2D>();
        lastMovedVector = new Vector2(1, 0f);   // Startup auto attack direction, facing right
        // If we dont initialise, the auto attacks wont have momentum unless the player moves

        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    

    // Update called by time, instead of by frames
    void FixedUpdate()
    {

        // CHECKS IF PLAYER CAN MOVE AFTER AN ATTACK
        if (!canMove){return;}



        // FLIPS MOVEMENT SPRITE DEPENDING ON X-axis DIRECTION
        // AVOIDS HAVING TO MAKE SPRITES FOR BOTH SIDES
        if (movement == Vector2.zero)
        {
            animator.SetBool("isMoving", false);
        } 
        else 
        {
            // Flip the sprite if we're moving left
            spriteRenderer.flipX = movement.x < 0;
        }



        // COLLISION BEHAVIOUR BELOW

        /////// FOR EACH TIME.DELTATIME * X, X is the current modifier for speed...
        ///

        // calling checkCollisions function created
        int count = checkCollisions(movement);

        // Only let the player move if there isn't any collision detected
        if (count == 0)
        {
            //transform.Translate(movement * Time.deltaTime * characterData.MoveSpeed);
            transform.Translate(movement * Time.deltaTime * player.CurrentMoveSpeed);
            return; // If no collisions, move the player and exit from the function
        }
        // If we hit something, try moving in the x direction
        count = checkCollisions(new Vector2(movement.x, 0));
        if (count == 0)
        {
            // transform.Translate(new Vector2(movement.x, 0) * Time.deltaTime * characterData.MoveSpeed);
            transform.Translate(new Vector2(movement.x, 0) * Time.deltaTime * player.CurrentMoveSpeed);
            return; // If no collisions in current X direction, move the player and exit from the function
        }
        // If we hit something, try moving in the y direction
        count = checkCollisions(new Vector2(0, movement.y));
        if (count == 0)
        {
            // transform.Translate(new Vector2(0, movement.y) * Time.deltaTime * characterData.MoveSpeed);
            transform.Translate(new Vector2(0, movement.y) * Time.deltaTime * player.CurrentMoveSpeed);
            return; // If no collisions in current Y direction, move the player and exit from the function
        }


        // This allows the player to slide along the obstacles. 
    }


    // new function checkCollisions to check number of collisions
    // Casts a ray in the direction of movement
    // It takes in the direction vector and return number of collision counts
    int checkCollisions(Vector2 direction)
    {
        int count = rb.Cast(
            direction,
            movementFilter, // The settings that determine what is considered a hit
            castCollisions, // List of collisions
            // moveSpeed * Time.deltaTime *2 + collisionOffset // The distance to cast i.e. movement plus a small offset
            //characterData.MoveSpeed * Time.deltaTime *2 + collisionOffset // The distance to cast i.e. movement plus a small offset
            player.CurrentMoveSpeed * Time.deltaTime *2 + collisionOffset // The distance to cast i.e. movement plus a small offset


        );
        return count;
    }


    void OnMove(InputValue movementValue)
    {

        if(GameManager.instance.isGameOver)
        {
            return;
        }

        animator.SetBool("isMoving", true);
        movement = movementValue.Get<Vector2>();

        if(movement.x != 0)
        {
            lastHorizontalVector = movement.x;
            lastMovedVector = new Vector2(lastHorizontalVector, 0f); // Gathers the last moved X value
        }

        if(movement.y != 0)
        {
            lastVerticalVector = movement.y;
            lastMovedVector = new Vector2(0f, lastVerticalVector); // Gathers the last moved Y value
        }

        // Makes sure we do not store values when the player is moving diagonally
        // Prevent diagonal shots when standing still
        // When both X and Y are moving, lastMovedVector only stores the last update from the above functions
        // Prevents last X and Y from updating during this time
        if(movement.x != 0 && movement.y != 0)
        {
            lastMovedVector = new Vector2(lastHorizontalVector, lastVerticalVector); // While moving
        }

    }



    void OnFire()
    {
        if(GameManager.instance.isGameOver)
        {
            return;
        }
        animator.SetTrigger("attack");
    }




    public void LockMovement()
    {
        canMove = false;
    }

    public void UnlockMovement()
    {
        canMove = true;
    }

    public void SwordAttack()
    {
        LockMovement();
        if (spriteRenderer.flipX)
        {
            swordAttack.AttackLeft();
        }
        else
        {
            swordAttack.AttackRight();
        }
    }

    public void StopAttack()
    {
        UnlockMovement();
        swordAttack.StopAttack();
    }


}
