using System;
using System.Collections;
using UnityEngine;

public class LilypadScript : MonoBehaviour
{
    public GameObject FrogImage;
    public bool isOccupied;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        FrogImage.SetActive(false);
        isOccupied = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            if(!isOccupied)
            {
                isOccupied = true;
                FrogImage.SetActive(true);
                GameManager.instance.SpawnFrog();
                Destroy(other.gameObject);
            }
        }
        else 
        { 
            other.GetComponent<FrogController>().KillFrog();    
        }
    }
}
