using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAnimHolder : MonoBehaviour
{
    Animator anim;

    void Start()
    {
        //Get the Animator attached to the GameObject you are intending to animate.
        anim = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log("Spinning");
            anim.Play("UI_BorderLoop");
        }

    }
}
