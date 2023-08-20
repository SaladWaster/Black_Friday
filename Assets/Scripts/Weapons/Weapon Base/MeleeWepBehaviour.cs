using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// BASE SCRIPT OF ALL MELEE BEHAVIOURS ///
// PLACE ON A PREFAB OF A WEP THAT IS MELEE //

public class MeleeWepBehaviour : MonoBehaviour
{

    public WeaponScriptableObject weaponData;
    
    public float destroyAfterSeconds;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        Destroy(gameObject, destroyAfterSeconds);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
