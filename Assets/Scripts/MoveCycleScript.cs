using UnityEngine;
using System;
using System.Collections;

public class MoveCycleScript : MonoBehaviour
{
    //Speed and dir
    [SerializeField] public float moveSpeed = 1;
    public Vector2 moveDirection = Vector2.right;

    //Control screen-wrap
    public int objSize = 1;
    [SerializeField] private Vector2 rightEdge;
    [SerializeField] private Vector2 leftEdge;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        leftEdge = Camera.main.ViewportToWorldPoint(Vector2.zero);
        rightEdge = Camera.main.ViewportToWorldPoint(Vector2.right);
    }

    // Update is called once per frame
    void Update()
    {
        if((moveDirection.x > 0) && ((transform.position.x - objSize) > rightEdge.x))
        { 
            Vector2 vehiclePosition = transform.position;
            vehiclePosition.x = leftEdge.x - objSize;
            transform.position = vehiclePosition;
        }
        else if((moveDirection.x < 0) && ((transform.position.x + objSize) < leftEdge.x))
        { 
            Vector2 vehiclePosition = transform.position;
            vehiclePosition.x = rightEdge.x + objSize;
            transform.position = vehiclePosition;
        }
        else
        {
            transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
        }
    }
}
