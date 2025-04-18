using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardTextController : MonoBehaviour {

    public Text highScore1;
    public Text highScore2;
    public Text highScore3;
    public Text highScore4;
    public Text highScore5;
    

    void Start() {
        RemoteHighScoreManager.Instance.GetHighScore(UpdateUI);
        Debug.Log("MERP");
    }

    void UpdateUI(List<int> scores) {
        foreach(int i in scores){
            Debug.Log(i);
        }
        
        Text[] textboxes = {highScore1, highScore2, highScore3, highScore4, highScore5};
        for (int i = 0; i < 5; i++){
            if (i < scores.Count && scores[i] != null && scores[i] != 0){
                textboxes[i].text = "High Score: " + scores[i] + "!";
            }
            else{
                textboxes[i].text = "No High Score!";
            }
        }
        
        

        
    }

    public void ButtonHandlerReset() {
        RemoteHighScoreManager.Instance.SetHighScore(ResetOnComplete,0);
    }

    void ResetOnComplete() {
        List<int> thisIsDUMB = new List<int>(); //this makes no sense and needs to be completely reworked but first let's get the top5 logic sorted.
        thisIsDUMB.Add(0);
        UpdateUI(thisIsDUMB);
    }

}

