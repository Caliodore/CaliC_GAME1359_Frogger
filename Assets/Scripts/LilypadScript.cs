using System;
using System.Collections;
using UnityEngine;

public class LilypadScript : MonoBehaviour
{
    public GameObject frogImage;
    public bool isOccupied;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        frogImage.SetActive(false);
        isOccupied = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player made it to pad!");
            if(!isOccupied)
            {
                Debug.LogError("Player jumped on an unoccupied pad.");
                isOccupied = true;
                frogImage.SetActive(true);
                GameManager.instance.lilypadCounter++;
                GameManager.instance.SpawnFrog();
                Destroy(other.gameObject);
            }
            else 
            { 
                Debug.LogError("Player jumped on an occupied pad.");
                other.GetComponent<FrogController>().KillFrog();    
            }
        }
    }
}
