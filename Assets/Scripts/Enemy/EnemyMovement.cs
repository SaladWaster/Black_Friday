using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public EnemyScriptableObject enemyData;

    // Vector2 movement;
    // [SerializeField]    // For Better tracking due to Kyoko
    Transform player;
   // public float moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>().transform;

            // Use the posTransform for better tracking, but for Kyoko
            // Highlights the collision bugs; See if vod resolves it
        // player = FindObjectOfType<PlayerController>().posTransform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, enemyData.MoveSpeed * Time.deltaTime);
    }
}
