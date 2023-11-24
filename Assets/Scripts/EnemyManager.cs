using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour
{
    private Transform target;
    private EnemyManager targetEnemy;
    private string towerTag = "Melee";
    private float fireCountdown = 0f;
    private Tower tower;
    [Header("Set Ranged")]
    public float fireRate = 2f;
    public float range = 6f;
    public float turnSpeed = 10f;
    [HideInInspector]
    [Header("Set Attribuild")]
    public float speed ;
    public float startSpeed = 100f;
    public int worth = 30;
    public float slowTime = 0f;
    [Header("Health")]
    public Image healthBar;
    public float starhealth = 10;
    public float health;
    [Header("Set Fields")]
    public Transform partRotate;
    public GameObject bullPrefab;
    public Transform firePoint;

    private GameObject[] FindTower;
    private float distance;   
    public static int enemyCount;
    Wave wave;
    private int deadCount;

    private void Start()
    {       
        speed = startSpeed;
        health = starhealth;
    }

    private void Update()
    {
        TargetLockOn();
        UpdateTarget();      

    }

    /*void FindDistance()
    {
        FindTower = GameObject.FindGameObjectsWithTag("Melee");

        for (int i = 0; i < FindTower.Length; i++)
        {
            distance = Vector3.Distance(transform.position, FindTower[i].transform.position);

            if(distance <= range)
            {
                tower[i] = FindTower.GetComponent<Tower>().TackeDamgage(100);
            }
        }

    }*/


    public void Slow(float pct, float mTime)
    {
        speed = startSpeed * pct;
        slowTime = mTime;
    }
   
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
    public void Die()
    {
        
        //PlayerStats.Money += worth;
        PlayerStats.canPlusTime = true;
        waypointSpawner.instance.EnemiesAlibe--;
        //Debug.Log("EnemiesDead : " + waypointSpawner.instance.EnemiesAlibe);       
        waypointSpawner.instance.enemyCount --;
        Debug.Log("EnemyDeadddddddddd : " + waypointSpawner.instance.enemyCount);
        waypointSpawner.instance.UpdateEnemyCountText();
        Destroy(gameObject);
    }
    public void TakeDamage(float amount)
    {
        health -= amount;      
        healthBar.fillAmount = health/starhealth;
        if (health <= 0f && deadCount==0f)
        {            
            Die();
            deadCount++;
            Debug.Log("deadCount : " + deadCount);
        }
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
        GameObject[] tower = GameObject.FindGameObjectsWithTag(towerTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach (GameObject enemy in tower)
        {
            float distanceEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceEnemy < shortestDistance)
            {
                shortestDistance = distanceEnemy;
                nearestEnemy = enemy;
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
            Debug.Log("HitByTower");        
            GameObject bulletGo = (GameObject)Instantiate(bullPrefab, firePoint.position, firePoint.rotation);
            bulletGo.transform.Rotate(90f, 0f, 0f);
            Bullet bullet = bulletGo.GetComponent<Bullet>();        

            if (bullet != null)
            {
                bullet.Seek(target);
            }
        }
    public void TargetLockOn()
    {                  
         if (fireCountdown <= 0f)
            {
                Shoot();
                fireCountdown = 1f / fireRate;
            }
            fireCountdown -= Time.deltaTime;
        }

  }





