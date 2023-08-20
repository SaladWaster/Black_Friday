using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeBehaviour : ProjectileWepBehaviour
{

    //KnifeController kc;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        //kc = FindObjectOfType<KnifeController>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * weaponData.speed * Time.deltaTime; // Sets movement of knife
    }
}
