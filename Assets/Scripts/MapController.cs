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
//     Vector3 noTerrainPosition;
//     public LayerMask terrainMask;

//     // PlayerMovement pm;
//     // This is actually the PlayerController script atm, as we use that script for PlayerMovement
//     // This is similarly used in AutoWepController script
//     // We may revisit this at a later time and readjust this
//     PlayerController pm;


//     // Start is called before the first frame update
//     void Start()
//     {
//         pm = FindObjectOfType<PlayerController>();
//     }

//     // Update is called once per frame
//     void Update()
//     {
//         ChunkChecker();
//     }

//     void ChunkChecker()
//     {
//         if(pm.moveDir.x > 0 && pm.moveDir.y == 0)   // right
//         {
//             // we use Physics2D OverlapCircle to check if ther is a chunk a dist. away from our player
//             // We do not want to spawn chunks continuously, only in areas without a chunk
//             if((!Physics2D.OverlapCircle(player.transform.position + new Vector3(20, 0, 0)), checkerRadius, terrainMask))
//             {
//                 noTerrainPosition = player.transform.position + new Vector3(20, 0, 0);
//                 SpawnChunk();
//             }
//         }

//         if(pm.moveDir.x < 0 && pm.moveDir.y == 0)   // left
//         {
//             // we use Physics2D OverlapCircle to check if ther is a chunk a dist. away from our player
//             // We do not want to spawn chunks continuously, only in areas without a chunk
//             if((!Physics2D.OverlapCircle(player.transform.position + new Vector3(-20, 0, 0)), checkerRadius, terrainMask))
//             {
//                 noTerrainPosition = player.transform.position + new Vector3(-20, 0, 0);
//                 SpawnChunk();
//             }
//         }

//         if(pm.moveDir.x == 0 && pm.moveDir.y > 0)   // up
//         {
//             // we use Physics2D OverlapCircle to check if ther is a chunk a dist. away from our player
//             // We do not want to spawn chunks continuously, only in areas without a chunk
//             if((!Physics2D.OverlapCircle(player.transform.position + new Vector3(0, 20, 0)), checkerRadius, terrainMask))
//             {
//                 noTerrainPosition = player.transform.position + new Vector3(0, 20, 0);
//                 SpawnChunk();
//             }
//         }
        
//         if(pm.moveDir.x == 0 && pm.moveDir.y < 0)   // down
//         {
//             // we use Physics2D OverlapCircle to check if ther is a chunk a dist. away from our player
//             // We do not want to spawn chunks continuously, only in areas without a chunk
//             if((!Physics2D.OverlapCircle(player.transform.position + new Vector3(0, -20, 0)), checkerRadius, terrainMask))
//             {
//                 noTerrainPosition = player.transform.position + new Vector3(0, -20, 0);
//                 SpawnChunk();
//             }
//         }
//     }

//     void SpawnChunk()
//     {
//         int rand = Random.Range(0, terrainChunks.Count);
//         Instantiate(terrainChunks[rand], noTerrainPosition, Quaternion.identity);
//     }
}
