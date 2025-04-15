using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardTextController : MonoBehaviour {

    public Text highScoreText;

    void Start() {
        RemoteHighScoreManager.Instance.GetHighScore(UpdateUI);
        Debug.Log("MERP");
    }

    void UpdateUI(int score) {
        if (score > 0) highScoreText.text = "High Score: " + score + "!";
        else highScoreText.text = "No High Score!";
    }

    public void ButtonHandlerReset() {
        RemoteHighScoreManager.Instance.SetHighScore(ResetOnComplete,0);
    }

    void ResetOnComplete() {
        UpdateUI(0);
    }

}

