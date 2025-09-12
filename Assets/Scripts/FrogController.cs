using System;
using System.Collections;
using UnityEngine;

public class FrogController : MonoBehaviour
{
    Vector3 forwardDirection;
    Vector3 frogUp;

    [SerializeField] Animator frogAnimator;

    [SerializeField] private bool canMove;
    [SerializeField] public float moveTimer = 0.1f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        canMove = true;
        frogAnimator = GetComponentInChildren<Animator>();
        //frogUp = new Vector3(0, 0, 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove) 
        {
            PlayerUpdate();
        }
    }

    void PlayerUpdate()
    {
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.rotation = Quaternion.Euler(0,0,0);
            PlayerMove(Vector2.up);
        }
        if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            transform.rotation = Quaternion.Euler(0,0,180);
            PlayerMove(Vector2.down);    
        }
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.rotation = Quaternion.Euler(0,0,90);
            PlayerMove(Vector2.left);
        }
        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.rotation = Quaternion.Euler(0,0,270);
            PlayerMove(Vector2.right);    
        }
    }

    void PlayerMove(Vector2 _direction)
    {
        if (canMove) 
        { 
            canMove = false;
            frogAnimator.SetTrigger("Hop");
            Vector2 _destination = transform.position + (Vector3)_direction;
            StartCoroutine(LerpMove(_destination));
            //PlayerRotate(_direction);
        }
    }

    IEnumerator LerpMove(Vector2 _destination)
    {
        Vector2 startPos = transform.position;
        float elapsedTime = 0f;
        float duration = 0.1f;

        while (elapsedTime < duration)
        { 
            float _time = elapsedTime / duration;
            transform.position = Vector2.Lerp(startPos, _destination, _time);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = _destination;
        yield return new WaitForSeconds(moveTimer);
        canMove = true;
    }

    void PlayerRotate(Vector3 forwardDirection)
    {
        Vector3 restructuredForwards = new Vector3(forwardDirection.x, forwardDirection.y, 0);
        transform.rotation *= Quaternion.FromToRotation(transform.forward, restructuredForwards);
    }
}
