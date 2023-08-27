using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropRandomiser : MonoBehaviour
{
    public List<GameObject> propSpawnPoints;
    public List<GameObject> propPrefabs;

    // Start is called before the first frame update
    void Start()
    {
        SpawnProps();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnProps()
    {
        foreach (GameObject sp in propSpawnPoints)
        {
            int rand = Random.Range(0, propPrefabs.Count);

            // We assign the Instantitate to a GameObject prop parent
            // This spawns the props as children of their respective props rather than outside Terrain Chunk
            // This keeps our work organised
            GameObject prop = Instantiate(propPrefabs[rand], sp.transform.position, Quaternion.identity);
            prop.transform.parent = sp.transform;
        }
    }
}
