using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "WeaponScriptableObject", menuName = "ScriptableObjects/Weapon")]
public class WeaponScriptableObject : ScriptableObject
{
    // SCRIPTABLE OBJECTS change values even during runtime, and do not reset after
    // This is very dangerous, as we may accidentally modify values unknowingly
    // We use Properties to prevent this from happening

    [SerializeField]
    GameObject prefab;
    public GameObject Prefab { get => prefab; private set => prefab = value; }

        // Prefab with a capital P, indicating it is a property
        // Public getter and Private setter using {} Lambda operator
        // This allows us to simply READ from the variable, rather than SET the variable


    // Base stats for weapons
    [SerializeField]
    float damage;
    public float Damage { get => damage; private set => damage = value; }

    [SerializeField]
    float speed;
    public float Speed { get => speed; private set => speed = value; }

    [SerializeField]
    float cooldownDuration;
    public float CooldownDuration { get => cooldownDuration; private set => cooldownDuration = value; }

    [SerializeField]
    int pierce; // Pierce is hits before a wep breaks
    public int Pierce { get => pierce; private set => pierce = value; }

    [SerializeField]
    float range; // Range of Weapon (radius)
    public float Range { get => range; private set => range = value; }

    [SerializeField]
    int level;  // We only modify this for testing. This will not be modified in game
    public int Level { get => level; private set => level = value; }

    [SerializeField]
    GameObject nextLevelPrefab;  // Prefab of wep's next level/stage (What it evolves into)
    public GameObject NextLevelPrefab{ get => nextLevelPrefab; private set => nextLevelPrefab = value; }

    [SerializeField]
    new string name;
    public string Name{ get => name; private set => name = value; }

    [SerializeField]
    string description;     // Weapon Description (+Upgrades)
    public string Description{ get => description; private set => description = value; }


    // We use a Sprite instead of image as we will be changing the
    // image.sprite property, not the image itself
    [SerializeField]
    Sprite icon;  // This is modified only in the editor, not during runtime
    public Sprite Icon{ get => icon; private set => icon = value; }


    // public GameObject prefab;
    // // Base stats for weapons
    // public float damage;
    // public float speed;
    // public float cooldownDuration;
    // public int pierce; // Pierce is hits before a wep breaks
}
