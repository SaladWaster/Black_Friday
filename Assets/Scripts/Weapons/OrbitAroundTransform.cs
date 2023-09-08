using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Utility script for any projectile/object that needs to orbit around a target.
/// </summary>
public class OrbitAroundTransform : MonoBehaviour
{
    [SerializeField] private Transform orbitTarget;
    [SerializeField] private float degreesPerSecond;

    //Set to true if you just want the position to orbit and not the rotation of the object.
    public bool keepRotation = true;

    private Quaternion originalRotation;

    private void Awake()
    {
        originalRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        //Rotates 360 degrees per second.
        transform.RotateAround(orbitTarget.position, new Vector3(0f, 0f, 1f), Time.deltaTime * degreesPerSecond);
        
        if(keepRotation)
        {
            transform.rotation = originalRotation;
        }
        
    }
}
