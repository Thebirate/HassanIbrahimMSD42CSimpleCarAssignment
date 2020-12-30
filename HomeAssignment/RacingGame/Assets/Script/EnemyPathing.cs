using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    [SerializeField] List<Transform> waypoints;
    [SerializeField] float moveSpeed = 2f;
    int waypointIndex = 0;
    [SerializeField] WaveConfig waveConfig;



    void Start()
    {
         transform.position = waypoints[waypointIndex].transform.position;
        waypoints = waveConfig.GetWaypoints();


    }

    void Update()
    {
        EnemyMove();
    }

    void EnemyMove()
    {
        if (waypointIndex <= waypoints.Count - 1)
        {
           
            var targetPosition = waypoints[waypointIndex].transform.position;

            targetPosition.z = 0f;

            var movementThisFrame = moveSpeed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, movementThisFrame);

            if (transform.position == targetPosition)
            {
                waypointIndex++;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

}