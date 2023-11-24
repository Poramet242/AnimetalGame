using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;
    public float speed = 70f;
    public float explosionRadius = 0f;
    public GameObject impactEff;
    public int damages = 5;
    private GameObject effecIns;
    private Tower tower;
    private EnemyManager enemyManager;
    public void Seek(Transform _target)
    {
        target = _target;
    }
    private void Start()
    {
        enemyManager = GetComponent<EnemyManager>();
    }
    void Update()
    {
        if (target == null )
        {
            Destroy(gameObject);
        } if(target != null) {
        Vector3 dir = target.position - transform.position;
        float dirstanceThisFrame = speed*Time.deltaTime;
        if (dir.magnitude <= dirstanceThisFrame)
        {
            HitTarget();
        }
        transform.Translate(dir.normalized*dirstanceThisFrame,Space.World);
        transform.LookAt(target);
        }
    }
    public void HitTarget()
    {
        effecIns = (GameObject)Instantiate(impactEff, transform.position, Quaternion.Euler(Vector3.forward * 30f));
        Destroy(effecIns, 0.8f);
        if (explosionRadius > 0f)
        {
            Explode();
        }
        else
        {
            Damage(target);
        }

        Destroy(gameObject);
    }
    public void Damage(Transform enemy)
    {
        if (enemy.tag == "Enemy")
        {
            EnemyManager enem = enemy.GetComponent<EnemyManager>();         
            if (enem != null )
            {
                enem.TakeDamage(damages);
            }
        }
        else
        {
            Tower enemTower = enemy.GetComponent<Tower>();
            if (enemTower != null)
            {
                enemTower.TakeDamageEnemy(damages);
            }
        }
    }
    public void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position,explosionRadius);
        foreach (Collider collid in colliders)
        {
            if (collid.tag =="Enemy")
            {
                Damage(collid.transform);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,explosionRadius);
    }
}
