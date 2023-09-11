using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{   
    
    // public EnemyScriptableObject enemyData;

    EnemyStats enemy;
    Transform player;

    SpriteRenderer enemySR;

    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<EnemyStats>();
        player = FindObjectOfType<PlayerController>().transform;

        enemySR = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        var x0 = transform.position.x;
        var x1 = player.transform.position.x;
        enemySR.flipX = x0 > x1;
        
        //transform.position = Vector2.MoveTowards(transform.position, player.transform.position, enemyData.MoveSpeed * Time.deltaTime);
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, enemy.currentMoveSpeed * Time.deltaTime);

    }
}
