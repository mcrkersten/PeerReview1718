using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    [SerializeField]
    //This is the saved level index number
    private int level;

    private void Update() {
        //here keep track of variable level
    }
    //This function will be connected to an onscreen button
    public void PlayGame() {
        SceneManager.LoadScene(level);
    }
    public void QuitGame() {
        Application.Quit();
    }

}
