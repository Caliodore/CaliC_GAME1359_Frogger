using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleControlScript : MonoBehaviour
{
    public TurtleAnimationScript[] attachedTurtles;
    public Collider2D killCollider;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        killCollider.enabled = false;
        StartCoroutine(TurtleDiveSequence());
    }

    IEnumerator TurtleDiveSequence()
    { 
        float randomizedTimer = UnityEngine.Random.Range(5,8);
        yield return new WaitForSeconds(randomizedTimer);
        
        foreach(TurtleAnimationScript turt in attachedTurtles)
        { 
            turt.TurtleDive();
        }
        yield return new WaitForSeconds(2.75f);
        foreach(TurtleAnimationScript turt in attachedTurtles)
        { 
            turt.TurtleRise();
        }
        yield return new WaitForSeconds(2.75f);
        StartCoroutine(TurtleDiveSequence());
        yield return null;    
    }
}
