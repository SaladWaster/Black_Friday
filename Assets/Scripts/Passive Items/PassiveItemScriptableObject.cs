using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PassiveItemScriptableObject", menuName = "ScriptableObjects/Passive Item")]

public class PassiveItemScriptableObject : ScriptableObject
{
    
    [SerializeField]
    float multiplier;
    public float Multiplier { get => multiplier; private set => multiplier = value; }
    
    [SerializeField]
    int level;  // We only modify this for testing. This will not be modified in game
    public int Level { get => level; private set => level = value; }

    [SerializeField]
    GameObject nextLevelPrefab;  // Prefab of wep's next level/stage (What it evolves into)
    public GameObject NextLevelPrefab{ get => nextLevelPrefab; private set => nextLevelPrefab = value; }

    // We use a Sprite instead of image as we will be changing the
    // image.sprite property, not the image itself
    [SerializeField]
    Sprite icon;  // This is modified only in the editor, not during runtime
    public Sprite Icon{ get => icon; private set => icon = value; }

}
