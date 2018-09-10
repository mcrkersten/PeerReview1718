using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ClockScript : MonoBehaviour {
    //Public clock variables
    public GameObject clockBackground;
    public Text clockText;

    //Time variables
    private int startTime = 30;
    private int time;

    public GameObject timesUpScreen, eggSpawner;

    //Winner screens
    public GameObject winnerScreen_P1, winnerScreen_P2, drawScreen;

    //Win texts
    public Text pointsTextWin_P1, pointsTextWin_P2;

    //Lose texts
    public Text pointsTextLose_P1, pointsTextLose_P2;

    //Draw texts
    public Text pointsTextDraw_P1, pointsTextDraw_P2;

    //Score displays
    public GameObject scoreDisplay_P1, scoreDisplay_P2;

    public Score score;
    public Text scoreText_P1, scoreText_P2;

    public GameObject postGameMenu, musicPlayerGame;

    private bool canContinue = false;

    private void Start(){
        time = startTime;
        StartCoroutine("CountdownTime");
	}
	
	private void Update(){
        clockText.text = time.ToString();

        if (Input.GetKeyDown(KeyCode.Escape)) ReturnToMainMenu();

        if (time == 0){
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
    private IEnumerator CountdownTime (){
        while (time > 0){
            yield return new WaitForSeconds(1);
            time--;
            //Debug.Log(time);
        }
    }

    // Post-game screen coroutine
    private IEnumerator PostGame (){
        // Wait 2 seconds before showing the results screen
        yield return new WaitForSeconds(2);

        Debug.Log("Player 1 score: " + score.score_P1.ToString());
        Debug.Log("Player 2 score: " + score.score_P2.ToString());

        private string scoreString_P1 = score.score_P1.ToString() + " eggs";
        private string scoreString_P2 = score.score_P2.ToString() + " eggs";

        //winnerScreen_P1Text.text = scoreText_P1.ToString();
        //winnerScreen_P2Text.text = scoreText_P2.ToString();

        // Display winner's screen
        if (score.score_P1 > score.score_P2){
            pointsTextWin_P1.text = scoreString_P1;
            pointsTextLose_P2.text = scoreString_P2;
            winnerScreen_P1.SetActive(true);
        }
        else if (score.score_P1 < score.score_P2){
            pointsTextWin_P2.text = scoreString_P2;
            pointsTextLose_P1.text = scoreString_P1;
            winnerScreen_P2.SetActive(true);
        }
        else if (score.score_P1 == score.score_P2){
            pointsTextDraw_P1.text = scoreString_P1;
            pointsTextDraw_P2.text = scoreString_P2;
            drawScreen.SetActive(true);
        }

        yield return new WaitForSeconds(1);
        canContinue = true;
        if (canContinue){
            postGameMenu.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)) SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            if (Input.GetKeyDown(KeyCode.Space)) ReturnToMainMenu();
        }
    }

    private void ReturnToMainMenu(){
        Destroy(musicPlayerGame);
        SceneManager.LoadScene(sceneBuildIndex: 1);
    }
}
