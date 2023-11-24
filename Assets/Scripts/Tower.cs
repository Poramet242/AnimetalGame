using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class Tower : MonoBehaviour
{
    private Transform target;
    private EnemyManager targetEnemy;
    private string enemyTag = "Enemy";
    private float fireCountdown = 0f;
    [Header("Set Ranged")]
    
    public float fireRate = 2f;
    public float range = 6f;
    public float turnSpeed = 10f;  
    public float slowPct = .5f;
    [Header("Health")]
    public Image healthBarCharacter;
    public float starhealth = 10;
    private float health;
    [Header("Set Magic")]
    public bool useMagic = false;
    public int damageOverTime = 5;
    public LineRenderer lineRend;
    public ParticleSystem impactEfc;
    public Light impactLight;
    [Header("Set Fields")]
    public Transform partRotate;
    public GameObject bullPrefab;
    public Transform firePoint;

    GameObject[] enemies;
    SoundManager soundManager;
    public Animator animator;

    private void Start()
    {

        InvokeRepeating("UpdateTarget", 0f, 0.5f);
        health = starhealth;
    }
    private void Update()
    {
        TargetLockOn();
        UpdateTarget();
    }
    public void TargetLockOn()
    {
        if (target == null)
        {
            if (useMagic)
            {
                if (lineRend.enabled)
                {
                    lineRend.enabled = false;
                    impactEfc.Stop();
                    impactLight.enabled = false;
                }
            }
            return;
        }
        //LockOnTaget();
        if (useMagic)
        {
            Magic();
        }
        else
        {
            //Shoot target
            if (fireCountdown <= 0f)
            {
                Shoot();
                fireCountdown = 1f / fireRate;
            }
            fireCountdown -= Time.deltaTime;
        }
    }
    public void Magic()
    {
        targetEnemy.TakeDamage(damageOverTime * Time.deltaTime);
        targetEnemy.Slow(slowPct,3f);
        if (!lineRend.enabled)
        {
            lineRend.enabled = true;
            impactEfc.Play();
            impactLight.enabled = true;
        }
        lineRend.SetPosition(0, firePoint.position);
        lineRend.SetPosition(1, target.position);
        Vector3 dir = firePoint.position - target.position;
        impactEfc.transform.position = target.position + dir.normalized;
        impactEfc.transform.rotation = Quaternion.LookRotation(dir);
    }
    public void LockOnTaget()
    {
        Vector3 dir = target.position - transform.position;
        Quaternion lookAtRotate = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partRotate.rotation, lookAtRotate, turnSpeed * Time.deltaTime).eulerAngles;
        partRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }
    public void UpdateTarget()
    { 
        enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach (GameObject enemy in enemies)
        {
            float distanceEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceEnemy < shortestDistance)
            {
                shortestDistance = distanceEnemy;
                nearestEnemy = enemy;
            }

            if(transform.position.z < enemy.transform.position.z)
            {
                transform.localScale = new Vector3 (1, 1, -1);
            }
            else if (transform.position.z > enemy.transform.position.z)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
        }
        if (nearestEnemy != null && shortestDistance <= range)
        { 
            target = nearestEnemy.transform;
            targetEnemy = nearestEnemy.GetComponent<EnemyManager>();
        }
        else
        {
            target = null;
        }
    }
    public void Shoot()
    {
        Debug.Log("SHOOTByTower");
        if(gameObject.tag =="Melee")
        {
            
            FindObjectOfType<SoundManager>().PlaySounded("Slash1");
            FindObjectOfType<SoundManager>().PlaySounded("Slash2");
            animator.SetTrigger("Atk");
           
        }
        else
        {
            FindObjectOfType<SoundManager>().PlaySounded("Shoot");
            animator.SetTrigger("Atk");

        }
        targetEnemy.Slow(slowPct,1f);
        GameObject bulletGo = (GameObject)Instantiate(bullPrefab, firePoint.position, firePoint.rotation);
        bulletGo.transform.Rotate(90f, 0f, 0f);
        Bullet bullet = bulletGo.GetComponent<Bullet>();

        /*foreach (GameObject enemy in enemies)
        {
            enemy.GetComponent<EnemyManager>().speed = 0f;
        }*/

        if (bullet != null || targetEnemy.health >= 0)
        {
            Debug.Log("This is Bullet");
            bullet.Seek(target);

        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
    public void TakeDamageEnemy(float amount)
    {
        health -= amount;

        healthBarCharacter.fillAmount = health / starhealth;
        if (health <= 0f)
        {
            Destroy(gameObject);
        }
    }

    /*private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            shootingEnemy = other.gameObject;
            EnemyManager enemyManager = shootingEnemy.GetComponent<EnemyManager>();
            enemyManager.speed = 0f;
        }
    }*/
}
