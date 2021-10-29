using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Tower : MonoBehaviour
{
    public LayerMask whatIsPlayer;
    RaycastHit hit;
    public ParticleSystem particleEffect0;
    public ParticleSystem particleEffect1;
    public ParticleSystem particleEffect2;
    public ParticleSystem particleEffect3;

    public float sphereRadius = 5f;
    public float sphereOffset = 5f;

    public int attackDamage = 10;
    public float attackInterval = 5f;
    float nextTimeToAttack = 0f;

    public float sightRange;
    public float attackRange;

    [SerializeField]
    bool isStatic = false; // For static towers.
    [SerializeField]
    bool constantParticle;

    bool isAttacking = false;
    bool inSightRange = false;
    bool inAttackRange = false;

    private void FixedUpdate()
    {
        inSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        inAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);


        if (inSightRange)
        {
            if (constantParticle)
            {
                if (particleEffect0 != null)
                {
                    particleEffect0.Play();
                }
                if (particleEffect1 != null)
                {
                    particleEffect1.Play();
                }
                if (particleEffect2 != null)
                {
                    particleEffect2.Play();
                }
                if (particleEffect3 != null)
                {
                    particleEffect3.Play();
                }
            }
            if (!isStatic)
            {
                LookAt();
            }
            if (inAttackRange)
            {
                if (Time.time >= nextTimeToAttack)
                {
                    print("fire");
                    Attack();
                    nextTimeToAttack = Time.time + 1f / attackInterval;

                    if (!isAttacking)
                    {
                        isAttacking = true;
                    }
                }
            }
        }
        else
        {
            if (constantParticle)
            {
                if (particleEffect0 != null)
                {
                    particleEffect0.Play();
                }
                if (particleEffect1 != null)
                {
                    particleEffect1.Play();
                }
                if (particleEffect2 != null)
                {
                    particleEffect2.Play();
                }
                if (particleEffect3 != null)
                {
                    particleEffect3.Play();
                }
            }
        }
    }

    public void Attack()
    {
        if (isStatic)
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, attackRange);
            foreach (Collider hitCollider in hitColliders)
            {
                if (hitCollider.gameObject.tag == "Enemy")
                {
                    print(hitCollider.gameObject.name);
                    hitCollider.gameObject.GetComponent<HealthSystem>().Damage(attackDamage);
                }

                if (inSightRange)
                {
                    // TODO: Play particle effects here.
                }
            }
        }
        else
        {
            // Bit shift the index of the layer (8) to get a bit mask.
            int layerMask = 1 << 8;
            layerMask = ~layerMask;

            // Visualize the raycast.
            if (Physics.SphereCast(transform.position, sphereRadius, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
            {
                if (inSightRange)
                {
                    Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);

                    if (inAttackRange)
                    {
                        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.green);
                    }
                }
                //print("FixedUpdate->Debug.DrawRay(): reg.\n");
            }
            else
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.red);
                //print("FixedUpdate->Debug.DrawRay(): noreg.\n");
            }

            if (hit.collider != null)
            {
                if (hit.collider.gameObject.tag == "Enemy")
                {
                    hit.collider.gameObject.GetComponent<HealthSystem>().Damage(attackDamage);
                }
            }
            else
            {
                print("Fire(): var 'hit.collider' NULL.\n");
            }
        }
    }

    public void RateLimit()
    {
        isAttacking = false;
    }

    void LookAt()
    {
        float nearest = Mathf.Infinity;
        Collider nearestEnemy = null;

        foreach (Collider enemyCollider in Physics.OverlapSphere(transform.position, sightRange))
        {
            if (enemyCollider.gameObject.tag == "Enemy")
            {
                float distSqr = (enemyCollider.transform.position - transform.position).sqrMagnitude;
                if (distSqr < nearest)
                {
                    nearest = distSqr;
                    nearestEnemy = enemyCollider;
                }
            }
        }
        if (nearestEnemy == null)
        {
            //print("Attack(): var 'enemy' NULL.\n");
            return;
        }
        transform.LookAt(nearestEnemy.transform);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
    IEnumerator Sleep(float time)
    {
        yield return new WaitForSeconds(time);
    }
}
