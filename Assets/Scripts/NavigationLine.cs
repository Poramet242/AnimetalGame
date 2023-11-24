using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationLine : MonoBehaviour
{
    public float Speed;
    private int waypointIndex = 0;
    private Transform target;

    void Start()
    {
        target = WaypointManager.points[0];
    }

    private void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * Speed * Time.deltaTime, Space.World);
        if (Vector3.Distance(transform.position, target.position) <= 0.4f)
        {
            GetNextWaypoints();
        }
    }

    public void GetNextWaypoints()
    {
        if (waypointIndex >= WaypointManager.points.Length - 1)
        {
            EndPath();
            return;
        }
        waypointIndex++;
        target = WaypointManager.points[waypointIndex];
    }

    void EndPath()
    {
        waypointSpawner.instance.EnemiesAlibe--;
        Destroy(gameObject);
    }
}
