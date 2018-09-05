using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class FileChooser : MonoBehaviour {
    public List<AudioClip> audioList = new List<AudioClip>();
    public GameObject buttonPrefab;
    public GameObject canvas;
    private int position = -100;
    private string path;


    private void Start() {
        path = Application.dataPath + "\\AudioFolder\\";
        if (Application.platform == RuntimePlatform.WindowsPlayer) {
            path = Application.dataPath;
            path += "\\..\\";
            path += "\\AudioFolder\\";
        }
        StartCoroutine(LoadAudioFile());
    }

    //this coroutine reads .wav files and imports them into the audio list
    private IEnumerator LoadAudioFile() {
        //start reading files
        var info = new DirectoryInfo(path);
        var fileInfo = info.GetFiles("*.wav");
        foreach (FileInfo file in fileInfo) {
            WWW www = new WWW(file.FullName);
            yield return www;

            AudioClip audioClip = www.GetAudioClip(false, true, AudioType.WAV);

            audioClip.name = file.Name;
            audioList.Add(audioClip);

            //stop reading if there are 10 or more files
            if (audioList.Count >= 10) break;
        }
        SpawnButtons();
    }

    //this function presents the player a button for every song in the list
    private void SpawnButtons() {
        foreach (AudioClip clip in audioList) {
            GameObject button = Instantiate(buttonPrefab);
            button.transform.SetParent(canvas.transform);
            button.transform.localPosition = new Vector3(0, position, 0);
            button.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

            Text buttonText = button.GetComponentInChildren<Text>();
            buttonText.text = clip.name;
            position += 20;

            Button theButton = button.GetComponent<Button>();
            theButton.onClick.AddListener(delegate {OnClickButton(clip);});
        }
    }

    //this function simply adds the chosen clip to a singleton
    private void OnClickButton(AudioClip clip) {
        valueKeeper.instance.audioClip = clip;
        UnityEngine.SceneManagement.SceneManager.LoadScene("mainMenu");
        valueKeeper.instance.ResetStuff();
    }
}
