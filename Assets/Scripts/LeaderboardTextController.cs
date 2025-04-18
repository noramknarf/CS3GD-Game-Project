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

    public void SubmitScore(){
        Debug.Log("submit triggers");
        if (PersistentDataHandler.instance != null){
            int currentSessionPB = (int) PersistentDataHandler.instance.currentTotalScore;
            List<HighScoreResult> highScoresfromDB = RemoteHighScoreManager.Instance.highScoresFromDB;
            Debug.Log("Your score was: "+ currentSessionPB);
            bool worthy = false;

            if(RemoteHighScoreManager.Instance.highScoresFromDB.Count >= 5){
                foreach(HighScoreResult highScore in highScoresfromDB){
                    Debug.Log("previous score: " + highScore.Score);
                    if (currentSessionPB > highScore.Score){
                        Debug.Log("New PB added to leaderboard");
                        worthy = true;
                        RemoteHighScoreManager.Instance.SetHighScore(UpdateUI, currentSessionPB, highScore.objectId);
                        RemoteHighScoreManager.Instance.GetHighScore(UpdateUI);
                        break;
                    }
                }
            }
            else{
                Debug.Log(highScoresfromDB.Count);
            }
            
        }
        else{
            Debug.Log("No Global data object initialised");
        }
    }

    void UpdateUI() {
        List<HighScoreResult> DBEntries = RemoteHighScoreManager.Instance.highScoresFromDB;
        List<int> scores = new List<int>();
        foreach(HighScoreResult result in DBEntries){
            scores.Add(result.Score);
        }
        scores.Sort();
        scores.Reverse();
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
        foreach(HighScoreResult highScore in RemoteHighScoreManager.Instance.highScoresFromDB){
            Debug.Log("CRAAABB PEOPLEE " + highScore.objectId);
            RemoteHighScoreManager.Instance.SetHighScore(ResetOnComplete,0, highScore.objectId);
        }
        
    }

    void ResetOnComplete() {
        
        UpdateUI();
    }

}

