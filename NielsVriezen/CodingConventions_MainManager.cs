using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour {

	MainManager instance;

	string[ , ] userTimeArray;
	int currentPlayer = 0;
	float userTime;
	string userName;
	public float startUserTime;
	public float EndUserTime;

	// Use this for initialization
	void Awake () {
		if (Display.displays.Length > 1)
			Display.displays[1].Activate();
		if (Display.displays.Length > 2)
			Display.displays[2].Activate();
		if (this != instance && instance == null) {
			instance = this;
		} else {
			Destroy (this.gameObject);
		}
		DontDestroyOnLoad (instance.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (1)) {
			userTime = 899;
			SaveScore ();
		}
		
	}

	public void OnEndEdit(){
		userName = GameObject.Find("InputField").GetComponent<InputField> ().text;
		GameObject.Find ("confirmationText").GetComponent<Animator> ().Play ("ConfirmationAnimation");
		Debug.Log (userName);
		//userTimeArray[0,0] = {{userName, userTime},{"0",""}};
		//++currentPlayer;
	}

	public void OnLogin(){
		OnEndEdit ();
		SceneManager.LoadSceneAsync (1);
	}

	public void SaveScore(){
		//convert time;
		userTime = (EndUserTime - startUserTime);

		float[] oldTime = PlayerPrefsX.GetFloatArray("HighscoresTimes", 900, 100);
		string[] oldName = PlayerPrefsX.GetStringArray("HighscoresNames", "AI 1.2", 100);
		//int[] highscores = new int[oldHigh.Length];
		//int oldH = 0;
		for (int i = 0; i < oldTime.Length; i++){
			float oldT = oldTime[i];
			string oldN = oldName [i];
			if (userTime < oldTime[i]) {
				oldTime[i] = userTime;
				oldName [i] = userName;
				userTime = oldT;
				userName = oldN;
			}
		}
		PlayerPrefsX.SetFloatArray ("HighscoresTimes", oldTime);
		PlayerPrefsX.SetStringArray ("HighscoresNames", oldName);
		//string[] oldName = PlayerPrefsX.GetStringArray("HighscoresNames", 0, 100);
		//int[] highscores = new int[oldHigh.Length];
		//int oldH = 0;
		/*for (int i = 0; i < oldName.Length; i++){
			string oldN = oldName[i];
			if (userName > oldName[i]) {
				oldName[i] = userTime;
				userName = oldN;
			}
		}
		*/
		//PlayerPrefsX.SetFloatArray ("HighscoresNames", oldName);

		//Debug.Log (oldName[1] + " -- " + oldTime[1]);
	}
}
