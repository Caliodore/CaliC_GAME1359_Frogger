using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject playerPrefab;
    public Transform startPosition;

    public int lifeCounter = 3;
    public int lilypadCounter = 0;
    public TMP_Text lifeText;
    public TMP_Text lilypadText;

    //Timer vars
    public Image timerBar;
    public TMP_Text timerText;
    public float elapsedTime;
    public float timerDuration;
    public float durationPercentage;
    public float timeLeft;
    public bool timerResetTrigger;

    //Manage Screens
    public GameObject winScreen;
    public GameObject loseScreen;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (instance != null)
        {
            Destroy(instance);
        }
        else
        {
            instance = this;
        }

        startPosition = GameObject.FindGameObjectWithTag("Start Position").transform;
        SpawnFrog();
        winScreen.SetActive(false);
        loseScreen.SetActive(false);
    }

    private void FixedUpdate()
    {
        lifeText.text = lifeCounter.ToString();
        lilypadText.text = lilypadCounter.ToString();

        if(!timerResetTrigger)
        { 
            elapsedTime += Time.deltaTime;
        }
        else if(timerResetTrigger)
        { 
            elapsedTime = 0;
            timerResetTrigger = false;
        }

        timeLeft = timerDuration - elapsedTime;
        timerText.text = ((int)timeLeft).ToString();
        durationPercentage = 1 - (elapsedTime/timerDuration);
        timerBar.fillAmount = durationPercentage;
    }

    public void SpawnFrog()
    {
        timerResetTrigger = true;
        if(elapsedTime > timerDuration)
        { 
            loseScreen.SetActive(true);
            StartCoroutine(RestartGame());
        }
        if ((lifeCounter > 0) && (lilypadCounter < 5))
        {
            Instantiate(playerPrefab, startPosition.position, startPosition.rotation);
        }
        else
        {
            if (lifeCounter <= 0)
            {
                loseScreen.SetActive(true);
            }
            else if (lilypadCounter >= 5)
            {
                winScreen.SetActive(true);
            }
            StartCoroutine(RestartGame());
        }
    }

    IEnumerator RestartGame()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadSceneAsync(0);
    }

    /*IEnumerator GameTimer()
    {
        yield return new WaitForSeconds(0.25f);
        elapsedTime = 0;
        while(elapsedTime < timerDuration)
        {
            if(breakCoro)
            {    
                StopCoroutine(GameTimer());
                yield break;
            }
            Debug.Log(timeLeft.ToString());
            Debug.Log(elapsedTime.ToString());
            timeLeft = timerDuration - elapsedTime;
            timerText.text = ((int)timeLeft).ToString();
            elapsedTime++;
            durationPercentage = 1 - (elapsedTime/timerDuration);
            timerBar.fillAmount = durationPercentage;

            yield return new WaitForSecondsRealtime(1);
        }
        yield return null;    
    }*/
}
