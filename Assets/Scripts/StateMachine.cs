using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    private SpriteRenderer m_spriteRenderer;
    public enum State
    {
        Attack,
        Defence,
        RunAway,
        BerryPicking,
    }
    public State currentState;
    public AIMovement aiMovement;
    private void Start()
    {
        aiMovement = GetComponent<AIMovement>();
        m_spriteRenderer = GetComponent<SpriteRenderer>();
        currentState = State.BerryPicking;
        NextState();
    }
    private void NextState()
    {
        switch (currentState)
        {
            case State.Attack:
                StartCoroutine(AttackState());
                break;
            case State.Defence:
                StartCoroutine(DefenceState());
                break;
            case State.RunAway:
                StartCoroutine(RunAwayState());
                break;
            case State.BerryPicking:
                StartCoroutine(BerryPickingState());
                break;
            default:
                Debug.Log("Error!");
                break;
        }
    }
    private IEnumerator AttackState()
    {
        Debug.Log("Attack: Enter");
        m_spriteRenderer.color = Color.red;
        while (currentState == State.Attack)
        {
            //Debug.Log("Currently Attacking");
            aiMovement.AIMoveTowards(aiMovement.player);
            if (Vector2.Distance(aiMovement.player.position, aiMovement.transform.position) > aiMovement.chaseDistance)
            {
                aiMovement.LowestDistance();
                currentState = State.BerryPicking;
            }
            yield return null;
        }
        Debug.Log("Attack: Exit");
        NextState();
    }
    private IEnumerator DefenceState()
    {
        Debug.Log("Defence: Enter");
        m_spriteRenderer.color = Color.green;
        while (currentState == State.Defence)
        {
            //Debug.Log("Currently Defending");
            aiMovement.NewWaypoint();
            yield return new WaitForSeconds(2);
            if (aiMovement.waypoints.Count >= 3)
            {
                currentState = State.BerryPicking;
            }
        }
        Debug.Log("Defence: Exit");
        NextState();
    }
    private IEnumerator RunAwayState()
    {
        Debug.Log("RunAway: Enter");
        while (currentState == State.RunAway)
        {
            Debug.Log("Currently Running Away");
            yield return null;
        }
        Debug.Log("RunAway: Exit");
        NextState();
    }
    private IEnumerator BerryPickingState()
    {
        Debug.Log("BerryPicking: Enter");
        m_spriteRenderer.color = Color.blue;
        while (currentState == State.BerryPicking)
        {

            //Debug.Log("Currently Picking Berries");
            aiMovement.WaypointUpdate();
            if (aiMovement.waypoints.Count > 0)
            {
                aiMovement.AIMoveTowards(aiMovement.waypoints[aiMovement.waypointIndex].transform);
            }
            else
            {
                currentState = State.Defence;
            }

            if (Vector2.Distance(aiMovement.player.position, aiMovement.transform.position) < aiMovement.chaseDistance)
            {
                currentState = State.Attack;
            }
            yield return null;
        }
        Debug.Log("BerryPicking: Exit");
        NextState();
    }
}
