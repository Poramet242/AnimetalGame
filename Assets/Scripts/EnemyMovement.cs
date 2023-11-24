 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyManager))]
public class EnemyMovement : MonoBehaviour
{
    private int waypointIndex = 0;
    private Transform target;
    private EnemyManager enemy;
    private void Start()
    {
        enemy = GetComponent<EnemyManager>();
        target = WaypointManager.points[0];
    }
    private void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * enemy.speed * Time.deltaTime, Space.World);
        if (Vector3.Distance(transform.position, target.position) <= 0.4f)
        {
            GetNextWaypoints();
        }
        if(enemy.slowTime > 0f)
        {
            enemy.slowTime -= Time.deltaTime;
        }
        else
        {
            enemy.speed = enemy.startSpeed;
        }
    }
    //Next waypoint
    public void GetNextWaypoints()
    {
        if (waypointIndex >= WaypointManager.points.Length - 1)
        {
            EndPath();
            return;
        }
        waypointIndex++;
        TurnToNextWaypoint(WaypointManager.points[waypointIndex]);
        target = WaypointManager.points[waypointIndex];
    }

    private void TurnToNextWaypoint(Transform nextWaypoint)
    {
        Vector3 direction = target.position - nextWaypoint.position;
       // Vector3 enemyScale = transform.rotation.y;
        // LEFT / RIGHT
        if(direction.z != 0)
        {
            /*
            if(direction.z > 0)
            {
            }
            if(direction.z < 0)
            {

            }*/
            Vector3 scale = transform.localScale;
            scale.z *= -1;
            transform.localScale = scale;
        }

        // UP / DOWN
        if(direction.x != 0)
        {
            if (direction.x > 0)
            {
                //UP
                //Debug.Log(" TURN UP ");
            }
            if (direction.x < 0)
            {
                //DOWN
                //Debug.Log(" TURN DOWN ");
            }
        }
    }

    void EndPath()
    {
        PlayerStats.Live--;
        FindObjectOfType<SoundManager>().PlaySounded("HealthDamage");
        waypointSpawner.instance.EnemiesAlibe--;
        waypointSpawner.instance.enemyCount--;
        Destroy(gameObject);
        if(PlayerStats.Live < 0)
        {
            FindObjectOfType<SoundManager>().StopSounded("HealthDamage");
            PlayerStats.Live = 0;
            return;
        }
    }
}
