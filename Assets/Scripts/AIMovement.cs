using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMovement : MonoBehaviour
{
    public Transform player;
    public float chaseDistance;
    public GameObject[] waypoints;
    public int waypointIndex = 0;
    public bool chasedPlayer = false;
    public float speed = 1.5f;
    public float minGoalDistance = 0.1f;
    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, player.position) < chaseDistance)
        {
            //move towards the player
            AIMoveTowards(player);
            chasedPlayer = true;
        }
        else
        {
            if (chasedPlayer)//if recently chased player
            {
                chasedPlayer = false;
                LowestDistance();
            }
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
    //loop to find lowest distance index
    private void LowestDistance()
    {
        float lowestDistance = 99999999f;
        int lowestIndex = 0;
        for (int i = 0; i < waypoints.Length; i++)
        {
            if (Vector2.Distance(player.position, waypoints[i].transform.position) < lowestDistance)
            {
                lowestDistance = Vector2.Distance(player.position, waypoints[i].transform.position);
                lowestIndex = i;
            }
        }
        waypointIndex = lowestIndex;
    }
}