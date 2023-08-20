using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "WeaponScriptableObject", menuName = "ScriptableObjects/Weapon")]
public class WeaponScriptableObject : ScriptableObject
{
    // SCRIPTABLE OBJECTS change values even during runtime, and do not reset after
    // This is very dangerous, as we may accidentally modify values unknowingly
    // We use Properties to prevent this from happening

    // [SerializeField]
    // GameObject prefab;
    // public GameObject Prefab { get => prefab; private set => prefab = value; }

    //     // Prefab with a capital P, indicating it is a property
    //     // Public getter and Private setter using {} Lambda operator
    //     // This allows us to simply READ from the variable, rather than SET the variable


    // // Base stats for weapons
    // [SerializeField]
    // float damage;
    // public float Damage { get => damage; private set => damage = value; }

    // [SerializeField]
    // float speed;
    // public float Speed { get => speed; private set => speed = value; }

    // [SerializeField]
    // float cooldownDuration;
    // public float CooldownDuration { get => cooldownDuration; private set => cooldownDuration = value; }

    // [SerializeField]
    // int pierce; // Pierce is hits before a wep breaks
    // public float Pierce { get => pierce; private set => pierce = value; }


    public GameObject prefab;
    // Base stats for weapons
    public float damage;
    public float speed;
    public float cooldownDuration;
    public int pierce; // Pierce is hits before a wep breaks
}
