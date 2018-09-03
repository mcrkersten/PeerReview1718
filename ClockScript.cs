using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ClockScript : MonoBehaviour {

    public GameObject clockBackground;
    public Text clockText;

    public GameObject timesUpScreen;
    public GameObject eggSpawner;

    public GameObject winnerScreen_P1;
    public GameObject winnerScreen_P2;
    public GameObject drawScreen;

    public Text pointsTextWin_P1;
    public Text pointsTextWin_P2;

    public Text pointsTextLose_P1;
    public Text pointsTextLose_P2;

    public Text pointsTextDraw_P1;
    public Text pointsTextDraw_P2;

    public GameObject scoreDisplay_P1;
    public GameObject scoreDisplay_P2;

    int startTime = 30;
    int time;

    public Score score;
    public Text scoreText_P1;
    public Text scoreText_P2;

    public GameObject postGameMenu;
    public GameObject musicPlayerGame;

    bool canContinue = false;

    // Use this for initialization
    void Start () {
        time = startTime;
        StartCoroutine("CountdownTime");
	}
	
	// Update is called once per frame
	void Update () {
        clockText.text = time.ToString();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ReturnToMainMenu();
        }

        if (time == 0)
        {
            // Stop countdown timer and activate TIME'S UP screen
            StopCoroutine("CountdownTime");
            timesUpScreen.SetActive(true);

            // Deactivate HUD
            clockBackground.SetActive(false);
            eggSpawner.SetActive(false);
            scoreDisplay_P1.SetActive(false);
            scoreDisplay_P2.SetActive(false);

            // Start post-game screen coroutine
            StartCoroutine("PostGame");
        }
	}

    // Do I really need to explain this tho
    IEnumerator CountdownTime ()
    {
        while (time > 0)
        {
            yield return new WaitForSeconds(1);
            time--;
            //Debug.Log(time);
        }
    }

    // Post-game screen coroutine
    IEnumerator PostGame ()
    {
        // Wait 2 seconds before showing the results screen
        yield return new WaitForSeconds(2);

        Debug.Log("Player 1 score: " + score.score_P1.ToString());
        Debug.Log("Player 2 score: " + score.score_P2.ToString());

        string scoreString_P1 = score.score_P1.ToString() + " eggs";
        string scoreString_P2 = score.score_P2.ToString() + " eggs";

        //winnerScreen_P1Text.text = scoreText_P1.ToString();
        //winnerScreen_P2Text.text = scoreText_P2.ToString();

        // Display winner's screen
        if (score.score_P1 > score.score_P2)
        {
            pointsTextWin_P1.text = scoreString_P1;
            pointsTextLose_P2.text = scoreString_P2;
            winnerScreen_P1.SetActive(true);
        }
        else if (score.score_P1 < score.score_P2)
        {
            pointsTextWin_P2.text = scoreString_P2;
            pointsTextLose_P1.text = scoreString_P1;
            winnerScreen_P2.SetActive(true);
        }
        else if (score.score_P1 == score.score_P2)
        {
            pointsTextDraw_P1.text = scoreString_P1;
            pointsTextDraw_P2.text = scoreString_P2;
            drawScreen.SetActive(true);
        }

        yield return new WaitForSeconds(1);
        canContinue = true;
        if (canContinue)
        {
            postGameMenu.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                ReturnToMainMenu();
            }
        }
    }

    void ReturnToMainMenu()
    {
        Destroy(musicPlayerGame);
        SceneManager.LoadScene(sceneBuildIndex: 1);
    }
}
