using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMovement : MonoBehaviour
{
    public GameObject position0;
    public GameObject position1;
    // Update is called once per frame
    void Update()
    {
        //Vector2.MoveTowards(transform.position, position0.transform.position, Time.deltaTime)
        
        /*Vector2 AiPosition = transform.position;
        if (transform.position.x < position0.transform.position.x)
        {
            AiPosition.x = AiPosition.x + 1 * Time.deltaTime; //right
            transform.position = AiPosition;
        }
        else if (transform.position.x > position0.transform.position.x)
        {
            AiPosition.x = AiPosition.x - 1 * Time.deltaTime; //left
            transform.position = AiPosition;
        }
        if (transform.position.y < position0.transform.position.y)
        {
            transform.position += Vector3.up * 1 * Time.deltaTime;
        }
        else if (transform.position.y > position0.transform.position.y)
        {
            transform.position += Vector3.down * 1 * Time.deltaTime;
        }*/

        Vector2 directionToPos0 = (position0.transform.position - transform.position);
        directionToPos0.Normalize();
        transform.position += (Vector3)directionToPos0 * 1 * Time.deltaTime;
    }
}
