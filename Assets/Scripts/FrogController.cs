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
    LayerMask platformLayer;
    LayerMask barrierLayer;

    //Rigidbody playerRB;
    FrogController thisScript;

    [SerializeField] Animator frogAnimator;

    [SerializeField] private bool canMove;
    [SerializeField] private bool isDead;
    [SerializeField] private bool onRiver;
    [SerializeField] private bool onPlatform;

    [SerializeField] public float moveTimer = 0.1f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        direction2D = new Vector2(0, 0);
        canMove = true;
        frogAnimator = GetComponentInChildren<Animator>();
        thisScript = GetComponent<FrogController>();
        //playerRB = GetComponent<Rigidbody>();
        //frogUp = new Vector3(0, 0, 1);
    }

    // Update is called once per frame
    void Update()
    {
        PlayerUpdate();
    }

    private void FixedUpdate()
    {
        if(onPlatform)
        { 
            
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Obstacle"))
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
                PlayerMove(Vector2.up);
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
                thisScript.PlayerMove(direction2D);
            }
            if(Input.GetKeyDown(KeyCode.RightArrow))
            {
                transform.rotation = Quaternion.Euler(0,0,270);
                direction2D = Vector2.right;
                thisScript.PlayerMove(direction2D);    
            }
        }
    }

    void PlayerMove(Vector2 _direction)
    {
        Vector2 transform2D = new Vector2(transform.position.x, transform.position.y);
        Vector2 _destination = transform2D + _direction;
        platformLayer = LayerMask.GetMask("Platform");
        barrierLayer = LayerMask.GetMask("Barrier");
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
            
            if(platformCollider != null)
            { 
                transform.SetParent(platformCollider.transform);
                onPlatform = true;
            }
            else 
            { 
                transform.SetParent(null);
                onPlatform = false;
            }
            
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
            bool spaceCheck = Physics2D.Raycast(gameObject.transform.position, directionTest, 1f, platformLayer);
            if(spaceCheck)
            { 
                transform.Translate(directionTest);
            }
            if(directionTest == (Vector3)direction2D)
            { 
                canMove = true;
            }
            yield return null;
        }
    }

    IEnumerator PlatformBarrierCheck()
    { 
        yield return null;    
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
