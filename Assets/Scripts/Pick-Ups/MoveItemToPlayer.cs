using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveItemToPlayer : MonoBehaviour
{
    public Transform target;
    public float pullSpeed;
    public bool isMoving = false;

    void Update()
    {
        if(isMoving)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, pullSpeed * Time.deltaTime);
        }
    }

    public void MoveToPlayer(Transform player)
    {
        target = player;
        isMoving = true;
    }
}
