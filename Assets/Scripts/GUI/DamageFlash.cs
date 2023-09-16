using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageFlash : MonoBehaviour
{

    [Tooltip("Flash Material")]
    [SerializeField] private Material flashMaterial;

    [Tooltip("Flash Duration")]
    [SerializeField] private float duration;


    // The SpriteRenderer that should flash.
    private SpriteRenderer spriteRenderer;
    
    // The material that was in use, when the script started.
    private Material originalMaterial;
    private Coroutine flashRoutine;

    // Start is called before the first frame update
    void Start()
    {
        // Get the SpriteRenderer to be used or set it from the inspector
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Saves the original Material, so it switches back after damage flash ends
        originalMaterial = spriteRenderer.material;
    }

    public void Flash()
    {
        // Checks to only run flashRoutine if it is not active
        if (flashRoutine != null)
        {
            // If it is active, stop it
            StopCoroutine(flashRoutine);
        }

        // Start the Coroutine and store the reference for it
        flashRoutine = StartCoroutine(FlashRoutine());
    }

    private IEnumerator FlashRoutine()
    {
        // Swap to the flashMaterial.
        spriteRenderer.material = flashMaterial;

        // Pause the execution of this function for "duration" seconds.
        yield return new WaitForSeconds(duration);

        // After the pause, swap back to the original material.
        spriteRenderer.material = originalMaterial;

        // Set the routine to null, signaling that it's finished.
        flashRoutine = null;
    }
}
