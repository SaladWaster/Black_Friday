using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBehavior : ProjectileWepBehaviour
{
    private Animator animator;
    public float explosionRadius;
    protected override void Awake()
    {
        base.Awake();
        animator = GetComponent<Animator>();
    }
    protected override void Start()
    {
        //destroyAfterSeconds will now invoke a detonate function instead of Destroy(gameObject)
        Invoke(nameof(Detonate), destroyAfterSeconds);
    }

    private void Detonate()
    {
        animator.SetTrigger("Explode");
        RaycastHit2D[] explosionHits = Physics2D.CircleCastAll(transform.position, explosionRadius, Vector2.zero);
        //Find all the things in the cast that the bomb cares about and call appropriate behavior on them
        foreach (RaycastHit2D explosionHit in explosionHits)
        {
            if (explosionHit.transform.CompareTag("Enemy"))
            {
                EnemyStats enemy = explosionHit.transform.GetComponent<EnemyStats>();
                enemy.TakeDamage(GetCurrentDamage());
            }
            else if (explosionHit.transform.CompareTag("Prop"))
            {
                if(explosionHit.transform.gameObject.TryGetComponent(out BreakableProps breakable))
                {
                    breakable.TakeDamage(GetCurrentDamage());
                }
            }
        }
    }

    protected override void OnTriggerEnter2D(Collider2D col)
    {
        //do nothing
    }

    //Self destroy function for the animation controller to call.
    public void ByeBye()
    {
        Destroy(gameObject);
    }

    //For visualising the explosion radius
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
