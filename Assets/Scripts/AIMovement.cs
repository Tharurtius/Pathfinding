using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMovement : MonoBehaviour
{
    public Transform player;
    public float chaseDistance;
    public GameObject[] waypoints;
    public int waypointIndex = 0;
    //public GameObject position0;
    //public GameObject position1;
    public float speed = 1.5f;
    public float minGoalDistance = 0.1f;
    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, player.position) < chaseDistance)
        {
            //move towards the player
            AIMoveTowards(player);
        }
        else
        {
            WaypointUpdate();
            //moves towards our waypoints
            AIMoveTowards(waypoints[waypointIndex].transform);
        }
    }

    private void WaypointUpdate()
    {
        //if we are near the goal
        if (Vector2.Distance(transform.position, waypoints[waypointIndex].transform.position) < minGoalDistance)
        {
            waypointIndex++;
            if (waypointIndex >= waypoints.Length)
            {
                waypointIndex = 0;
            }
        }
    }
    private void AIMoveTowards(Transform goal)
    {
        //if we are near the goal
        Vector2 AIPosition = transform.position;
        if (Vector2.Distance(AIPosition, goal.transform.position) > minGoalDistance)
        {
            Vector2 directionToGoal = (goal.transform.position - transform.position);
            directionToGoal.Normalize();
            transform.position += (Vector3)directionToGoal * speed * Time.deltaTime;
        }
    }
}