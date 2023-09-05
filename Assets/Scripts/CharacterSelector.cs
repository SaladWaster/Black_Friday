using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelector : MonoBehaviour
{
    public static CharacterSelector instance;
    public CharacterScriptableObject characterData;


    void Awake()
    {
        // Check to see if instance of CharacterSelector is null
        // Destroy otherwise
        // Ensures only one instance of a char at a time
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static CharacterScriptableObject GetData()
    {
        return instance.characterData;
    }

    public void SelectCharacter(CharacterScriptableObject character)
    {
        characterData = character;
    }


    // This is essential to prevent memory leaks
    // We must be careful when handling DontDestroyOnLoad objects
    // Only necessary data is present and no unnecessary elements run in the background
    public void DestroySingleton()
    {
        instance = null;
        Destroy(gameObject);
    }
}
