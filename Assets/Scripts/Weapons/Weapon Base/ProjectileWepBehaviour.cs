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


    // Current stats
    protected float currentDamage;
    protected float currentSpeed;
    protected float currentCooldownDuration;
    protected int currentPierce;
    protected float currentRange;

    protected virtual void Awake()
    {
        //Assign current stats to be the values of the ones set in the scriptable object upon awakening
        currentDamage = weaponData.Damage;
        currentSpeed = weaponData.Speed;
        currentCooldownDuration = weaponData.CooldownDuration;
        currentPierce = weaponData.Pierce;
        currentRange = weaponData.Range;

    }


    // Updates current Damage with any modifiers to stat (e.g might)
    // Repalce currentDamage with this function GetCurrentDamage()
    public float GetCurrentDamage()
    {
        return currentDamage *= FindObjectOfType<PlayerStats>().CurrentMight;
    }





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


    // virtual so we can overwrite if needed
    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
        // Reference script from collided collider, dealing damage using the TakeDamage function
        // TakeDamage function from EnemyStats script
        if(col.CompareTag("Enemy"))
        {
            EnemyStats enemy = col.GetComponent<EnemyStats>();
            enemy.TakeDamage(GetCurrentDamage()); // Use current damage, as we may add damage modifiers later, rather than weapondata.damage
            ReducePierce();
        }
        else if(col.CompareTag("Prop"))
        {
            if(col.gameObject.TryGetComponent(out BreakableProps breakable))
            {
                breakable.TakeDamage(GetCurrentDamage());
                ReducePierce();
            }
            
        }
    }


    void ReducePierce() // Destroys projectiles when pierce is 0, rather than waiting for timer despawns
    {
        currentPierce--;
        if(currentPierce <= 0 )
        {
            Destroy(gameObject);
        }
    }


}
