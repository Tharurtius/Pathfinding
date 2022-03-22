using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMovement : MonoBehaviour
{
    public Transform player;
    public float chaseDistance;
    //public Transform[] waypoints;
    public List<GameObject> waypoints;
    public GameObject waypointPrefab;
    public Transform[] bushPosition;
    public int waypointIndex = 0;
    public bool chasedPlayer = false;
    public float speed = 1.5f;
    public float minGoalDistance = 0.1f;
    // Update is called once per frame
    //void Update()
    //{
    //    if (Vector2.Distance(transform.position, player.position) < chaseDistance)
    //    {
    //        //move towards the player
    //        AIMoveTowards(player);
    //        chasedPlayer = true;
    //    }
    //    else
    //    {
    //        if (chasedPlayer)//if recently chased player
    //        {
    //            chasedPlayer = false;
    //            LowestDistance();
    //        }
    //        WaypointUpdate();
    //        //moves towards our waypoints
    //        AIMoveTowards(waypoints[waypointIndex].transform);
    //    }
    //}

    public void WaypointUpdate()
    {
        //if we are near the goal
        if (Vector2.Distance(transform.position, waypoints[waypointIndex].transform.position) < minGoalDistance)
        {
            Destroy(waypoints[waypointIndex]);
            waypoints.Remove(waypoints[waypointIndex]);
            LowestDistance();//find closest waypoint
            //waypointIndex++;
            /*if (waypointIndex >= waypoints.Count)//probably dont need this anymore
            {
                waypointIndex = 0;
            }*/
        }
    }
    public void AIMoveTowards(Transform goal)
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
    public void NewWaypoint()
    {
        float x = Random.Range(-5, 5);
        float y = Random.Range(-5, 5);
        int bushIndex = Random.Range(0, 1);
        x += bushPosition[bushIndex].position.x;
        y += bushPosition[bushIndex].position.y;

        GameObject newPoint = Instantiate(waypointPrefab, new Vector2(x, y), Quaternion.identity);

        waypoints.Add(newPoint);
    }
    //loop to find lowest distance index
    public void LowestDistance()
    {
        float lowestDistance = float.PositiveInfinity;
        int lowestIndex = 0;
        float distance;
        for (int i = 0; i < waypoints.Count; i++)
        {
            distance = Vector2.Distance(transform.position, waypoints[i].transform.position);
            if (distance < lowestDistance)
            {
                lowestDistance = distance;
                lowestIndex = i;
            }
        }
        waypointIndex = lowestIndex;
    }
    public void AIMoveTowardsClone(Vector2 goal) //cannot move to position without a transform type goal, kinda dodgy but it'll probably work
    {
        //if we are near the goal
        Vector2 AIPosition = transform.position;
        if (Vector2.Distance(AIPosition, goal) > minGoalDistance)
        {
            Vector2 directionToGoal = ((Vector3)goal - transform.position);
            directionToGoal.Normalize();
            transform.position += (Vector3)directionToGoal * speed * Time.deltaTime;
        }
    }
}