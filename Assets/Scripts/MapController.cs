using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{

    // This is basically an infinitely generating map
    // WE WILL ONLY USE THIS IF WE DO MAKE IT SO/FINAL TOUCH
    // 18:00 2

//     public List<GameObject> terrainChunks;
//     public GameObject player;
//     public float checkerRadius;
//     public LayerMask terrainMask;
//     public GameObject currentChunk;
// Vector3 playerLastPosition;

// [Header("Optimisation")]
// public List<GameObject> spawnedChunks;
// GameObject latestChunk;
// public float maxOpDist; // Must be greater than length and width of tilemap
// float OpDist;
// float optimiserCooldown;
// public float optimiserCooldownDur;




//     // Start is called before the first frame update
//     void Start()
//     {
//         playerLastPosition = player.transform.position;
//     }

//     // Update is called once per frame
//     void Update()
//     {
//         ChunkChecker();
//         ChunkOptimiser();
//     }

//     void ChunkChecker()
//     {
//         if(!currentChunk)
//          {
//              return;
//          }

            // Vector3 moveDir = player.transform.position - playerLastPosition;
            // playerLastPosition = player.transform.position;

//     }

// string GetDirectionName(Vector3 direction)
// {
//     direction = direction.normalized;

//     // obtain magnitude of vector
//     if(Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
//     {
//         // More horizontal than vertical
//         if(direction.y > 0.5f)
//         {
//             return direction.x > 0 ? "Right Up" : "Left Up";
//         }
//         else if (direction.y < -0.5f)
//         {
//             return direction.x > 0 ? "Right Down" : "Left Down";
//         }
//         else
//         {
//             return direction.x > 0 ? "Right" : "Left";
//         }
//     }
//     else
//     {
//         //  More vertical than horizontal
//         if(direction.x > 0.5f)
//         {
//             return direction.y > 0 ? "Right Up" : "Right Down";
//         }
//         else if (direction.x < -0.5f)
//         {   
//             return direction.y > 0 ? "Left Up" : "Left Down";
//         }
//         else
//         {
//             return direction.y > 0 ? "Up" : "Down";
//         }
//     }

//     }
// }

//     void SpawnChunk(Vector3 spawnPosition)
//     {
//         int rand = Random.Range(0, terrainChunks.Count);
//         latestChunk = Instantiate(terrainChunks[rand], spawnPosition, Quaternion.identity);
//         spawnedChunks.Add(latestChunk);
//     }

    // void ChunkOptimiser()
    // {
    //     optimiserCooldown -= Time.deltaTime;
    // }



}
