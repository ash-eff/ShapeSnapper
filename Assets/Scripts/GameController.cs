using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameController : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public GameObject scoreObj;
    public TextMeshProUGUI endScoreText;
    public GameObject endScoreObj;
    public GameObject retryButton;
    public GameObject quitButton;
    public GameObject crossHolder;
    public GameObject whiteOut;
    public GameObject[] crosses;
    public AudioClip[] chimes;
    public Image dangerBar;
    public Color scoreTextColor1;
    public Color scoreTextColor2;

    public float lerpTime;
    
    public int changeTimer;
    public int adjustAmount;
    public float startingSpawnSpeed;
    public float spawnSpeedAdjust;
    public float minSpawnSpeed;
    public int maxShapes;

    private int shapesOnScreen;
    public float spawnSpeed;
    private bool gameOver;
    private int score;
    private int run = 0;
    private int mismatch;

    private AudioSource audioSource;

    public bool GameOver
    {
        get { return gameOver; }
        //set { gameOver = value; }
    }

    public float SpawnSpeed
    {
        get { return spawnSpeed; }
    }

    public int ShapesOnScreen
    {
        get { return shapesOnScreen; }
        set { shapesOnScreen = value; }
    }

    public int Score
    {
        get { return score; }
        set { score = value; }
    }

    public int Run
    {
        get { return run; }
        set { run = value; }
    }

    private void Awake()
    {
        spawnSpeed = startingSpawnSpeed;
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (gameOver)
        {
            return;
        }

        if(shapesOnScreen >= maxShapes)
        {
            gameOver = true;
            StartCoroutine(FadeToEndScreen());
        }

        float fillAmt = shapesOnScreen / (float)maxShapes;
        dangerBar.fillAmount = fillAmt;
        scoreText.text = score.ToString("0000");
        GameAdjust();
    }

    public void PlayChime()
    {
        if(run > 5)
        {
            run = 5;
        }
        audioSource.PlayOneShot(chimes[run]);
    }

    public void Mismatch()
    {
        crosses[mismatch].SetActive(true);
        mismatch++;
        if(mismatch >= 3)
        {
            gameOver = true;
            StartCoroutine(FadeToEndScreen());
        }
    }

    IEnumerator FadeToEndScreen()
    {
        float currentLerpTime = 0;
        while(whiteOut.GetComponent<Image>().color != scoreTextColor2)
        {
            currentLerpTime += Time.deltaTime;
            float perc = currentLerpTime / lerpTime;
            whiteOut.GetComponent<Image>().color = Color.Lerp(scoreTextColor1, scoreTextColor2, perc);

            yield return null;
        }

        endScoreObj.SetActive(true);
        retryButton.SetActive(true);
        quitButton.SetActive(true);
        endScoreText.text = "FINAL SCORE: " + score.ToString("0000");
        scoreObj.SetActive(false);
        crossHolder.SetActive(false);

        currentLerpTime = 0;
        while (whiteOut.GetComponent<Image>().color != scoreTextColor1)
        {
            currentLerpTime += Time.deltaTime;
            float perc = currentLerpTime / lerpTime;
            whiteOut.GetComponent<Image>().color = Color.Lerp(scoreTextColor2, scoreTextColor1, perc);

            yield return null;
        }

        Debug.Log("End Game");
    }

    void GameAdjust()
    {
        if (Time.timeSinceLevelLoad > changeTimer)
        {
            changeTimer += adjustAmount;
            float spawnAdj = spawnSpeed - spawnSpeedAdjust;

            if (spawnSpeed <= minSpawnSpeed)
            {
                spawnSpeed = minSpawnSpeed;
            }
            else
            {
                spawnSpeed = spawnAdj;
            }
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        SceneManager.LoadScene(0);
    }
}
