using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleAnimationScript : MonoBehaviour
{
    [SerializeField] TurtleControlScript turtCtrl;
    [SerializeField] Animator turtAnim;

    private void Start()
    {
        turtCtrl = GetComponentInParent<TurtleControlScript>();
        turtAnim = GetComponent<Animator>();
    }

    public void TurtleDive()
    { 
        turtAnim.SetTrigger("Dive");
    }

    public void TurtleRise()
    { 
        turtAnim.SetTrigger("Rise");
    }

    public void KillCollEnable()
    { 
        turtCtrl.killCollider.enabled = true;
    }

    public void KillCollDisable()
    { 
        turtCtrl.killCollider.enabled = false;
    }
}
