using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterRoad : MonoBehaviour
{
    public float speed;
    public float startSpeed = 100f;
    private int waypointIndex = 0;
    private Transform target;
    private EnemyManager enemy;
}
