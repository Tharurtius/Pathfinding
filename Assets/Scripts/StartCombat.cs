using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCombat : MonoBehaviour
{
    [SerializeField] GameObject _combatCanvas;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log(collision.collider.gameObject.name);
        AIMovement aiMove = collision.collider.gameObject.GetComponent<AIMovement>();

        if (aiMove == null)
        {
            return;
        }

        //Debug.Log("We have hit an AI");
        //enter combat
        _combatCanvas.SetActive(true);
        Time.timeScale = 0;
    }
}
