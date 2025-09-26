using System;
using System.Collections;
using UnityEngine;

public class FrogController : MonoBehaviour
{
    Vector3 forwardDirection;
    Vector3 frogUp;
    Vector2 direction2D;
    public Vector3 directionTest;
    public Vector3 directionToPlayer;

    Collider2D barrierCollider;

    [SerializeField] Animator frogAnimator;

    [SerializeField] private bool canMove;
    [SerializeField] private bool isDead;
    [SerializeField] private bool onRiver;
    [SerializeField] private bool onPlatform;

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
        //if (canMove) 
        //{
            PlayerUpdate();
        //}
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Obstacle") || other.gameObject.CompareTag("Kill"))
        { 
            KillFrog();    
        }
    }

    public void KillFrog()
    { 
        StartCoroutine(FrogDeath());    
    }

    void PlayerUpdate()
    {
        if(!isDead)
        { 
            if(Input.GetKeyDown(KeyCode.UpArrow))
            {
                transform.rotation = Quaternion.Euler(0,0,0);
                direction2D = Vector2.up;
                PlayerMove(direction2D);
            }
            if(Input.GetKeyDown(KeyCode.DownArrow))
            {
                transform.rotation = Quaternion.Euler(0,0,180);
                direction2D = Vector2.down;
                PlayerMove(direction2D);    
            }
            if(Input.GetKeyDown(KeyCode.LeftArrow))
            {
                transform.rotation = Quaternion.Euler(0,0,90);
                direction2D = Vector2.left;
                PlayerMove(direction2D);
            }
            if(Input.GetKeyDown(KeyCode.RightArrow))
            {
                transform.rotation = Quaternion.Euler(0,0,270);
                direction2D = Vector2.right;
                PlayerMove(direction2D);    
            }
        }
    }

    void PlayerMove(Vector2 _direction)
    {
        Vector2 _destination = transform.position + (Vector3)_direction;
        LayerMask platformLayer = LayerMask.GetMask("Platform");
        LayerMask barrierLayer = LayerMask.GetMask("Barrier");
        Collider2D platformCollider = Physics2D.OverlapBox(_destination, Vector2.zero, 0, platformLayer);
        barrierCollider = Physics2D.OverlapBox(_destination, Vector2.zero, 0, barrierLayer);

        if (canMove) 
        { 
            canMove = false;
            frogAnimator.SetTrigger("Hop");
            //PlayerRotate(_direction);

            if(barrierCollider != null)
            {
                StartCoroutine(BarrierCheck());
                return;
            }


            if (platformCollider != null)
            {
                transform.SetParent(platformCollider.transform);
                onPlatform = true;
            }
            else
            {
                transform.SetParent(null);
                onPlatform = false;
            }

            //while(gameObject.transform.parent.CompareTag("Platform"))
            //{ 
            //    //if(barrierCollider != null)
            //    //{ 
            //    //    KillFrog();    
            //    //}
            //}

            StartCoroutine(LerpMove(_destination));
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

    IEnumerator BarrierCheck()
    {
        while(barrierCollider != null)
        {
            directionTest = barrierCollider.transform.InverseTransformDirection(direction2D).normalized;
            if(directionTest == (Vector3)direction2D)
                canMove = true;
            yield return null;
        }
    }

    IEnumerator FrogDeath() 
    {
        isDead = true;
        frogAnimator.SetTrigger("Dead");
        yield return new WaitForSeconds(1.5f);
        GameManager.instance.SpawnFrog();
        Destroy(this.gameObject);
    }
}
