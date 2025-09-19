using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject playerPrefab;
    public Transform startPosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(instance != null)
        { 
            Destroy(instance);    
        }
        else 
        { 
            instance = this;            
        }

        startPosition = GameObject.FindGameObjectWithTag("Start Position").transform;
        SpawnFrog();
    }

    public void SpawnFrog()
    { 
        Instantiate(playerPrefab, startPosition.position, startPosition.rotation);    
    }
}
