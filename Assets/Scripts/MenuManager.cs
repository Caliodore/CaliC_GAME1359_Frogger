using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        { 
            StartCoroutine(StartGame());    
        }
    }

    IEnumerator StartGame()
    {
        SceneManager.LoadSceneAsync(1);
        yield return null;    
    }
}
