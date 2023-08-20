using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// BASE SCRIPT OF ALL PROJECTILE BEHAVIOURS ///
// PLACE ON A PREFAB OF A WEP THAT IS A PROJECTILE //

public class ProjectileWepBehaviour : MonoBehaviour
{
    public WeaponScriptableObject weaponData;
    
    // direction has to be Vector3 it seems
    protected Vector3 direction;
    public float destroyAfterSeconds;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        Destroy(gameObject, destroyAfterSeconds);
    }

    // public so we can call in more scripts later on
    public void DirectionChecker(Vector3 dir)
    {
        direction = dir;

        float dirx = direction.x;
        float diry = direction.y;

        Vector3 scale = transform.localScale;
        Vector3 rotation = transform.rotation.eulerAngles;

        if(dirx < 0 && diry == 0) // left
        {
            scale.x = scale.x * -1;
            scale.y = scale.y * -1;
        }
        else if(dirx == 0 && diry < 0) // down
        {
            scale.y = scale.y * -1;
        }
        else if(dirx == 0 && diry > 0) // up
        {
            scale.x = scale.x * -1;
        }
        else if(dirx > 0 && diry > 0) // top-right
        {
            rotation.z = 0f;
        }
        else if(dirx > 0 && diry < 0) // bottom-right
        {
            rotation.z = -90f;
        }
        else if(dirx < 0 && diry > 0) // top-left
        {
            scale.x = scale.x * -1;
            scale.y = scale.y * -1;
            rotation.z = -90f;
        }
        else if(dirx < 0 && diry < 0) // bottom-left
        {
            scale.x = scale.x * -1;
            scale.y = scale.y * -1;
            rotation.z = 0f;
        }


        transform.localScale = scale;
        transform.rotation = Quaternion.Euler(rotation); // Must use Euler; we cannot convert Quaternion to Vector3
    }
}
