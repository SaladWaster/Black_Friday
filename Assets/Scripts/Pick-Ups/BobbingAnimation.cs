using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobbingAnimation : MonoBehaviour
{
    public float frequency; // Frequency at which item bobs (bob speed)
    public float magnitude; // Magnitude at which item bobs from initial position (bob range)
    public Vector3 direction;
    Vector3 initialPos;

    // Start is called before the first frame update
    void Start()
    {
        initialPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = initialPos + direction * Mathf.Sin(Time.time * frequency) * magnitude;
    }
}
