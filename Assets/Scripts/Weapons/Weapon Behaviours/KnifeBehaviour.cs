using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeBehaviour : ProjectileWepBehaviour
{
    //SFX
    AudioManager audioManager;
    //KnifeController kc;

    // Start is called before the first frame update
    protected override void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        audioManager.PlaySound(audioManager.knife);
        base.Start();
        //kc = FindObjectOfType<KnifeController>();
    }

    // Update is called once per frame
    void Update()
    {
       // transform.position += direction * weaponData.Speed * Time.deltaTime; // Sets movement of knife
       transform.position += direction * currentSpeed * Time.deltaTime; // Sets movement of knife
    }
}
