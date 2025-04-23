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
        Debug.Log("initial UI update complete.");
    }

    public void SubmitScore(){
        Debug.Log("submit triggers");
        if (PersistentDataHandler.instance != null){
            int currentSessionPB = (int) PersistentDataHandler.instance.currentTotalScore;
            List<HighScoreResult> highScoresfromDB = RemoteHighScoreManager.Instance.highScoresFromDB; //hsfdb = scores in the DB as they appear
            List<HighScoreResult> scoresLowToHigh = highScoresfromDB;
            scoresLowToHigh.Sort();

            Debug.Log("Your score was: "+ currentSessionPB);
            bool worthy = false;

            if(RemoteHighScoreManager.Instance.highScoresFromDB.Count > 0){
                
                
                foreach(HighScoreResult highScore in scoresLowToHigh){
                    Debug.Log("previous score: " + highScore.Score);
                    if (currentSessionPB > highScore.Score){
                        Debug.Log("New PB added to leaderboard");
                        highScore.Score = currentSessionPB;
                        RemoteHighScoreManager.Instance.SetHighScore(GetHighScore, currentSessionPB, highScore.objectId);
                        /*
                        highScoresfromDB[highScoresfromDB.Count-1].Score = currentSessionPB;
                        
                        
                        highScoresfromDB.Sort();
                        
                        for(int i=0; i<highScoresfromDB.Count; i++){
                           RemoteHighScoreManager.Instance.SetHighScore(GetHighScore, highScoresfromDB[i].Score, RemoteHighScoreManager.Instance.highScoresFromDB[i].objectId); //In theory should save the dataset one by one to the unaltered indexes
                        }
                        
                        //RemoteHighScoreManager.Instance.GetHighScore(UpdateUI);
                        */
                        worthy = true;
                        break;
                    }
                }
            }
            else{
                Debug.Log("number of high scores obtained from DB = " + highScoresfromDB.Count + " Attempting to add a new score rather than overwrite.");
                Debug.Log("");
                RemoteHighScoreManager.Instance.SetHighScore(GetHighScore, currentSessionPB);
                //RemoteHighScoreManager.Instance.GetHighScore(UpdateUI);
            }
            if(!worthy){
                Debug.Log("Score was not higher than any in DB and no empty slots to save to.");
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
        for(int i = 0; i < scores.Count; i++){
            Debug.Log("scores " + i + " = " + scores[i]);
        }
        
        Text[] textboxes = {highScore1, highScore2, highScore3, highScore4, highScore5};
        for (int i = 0; i < 5; i++){
            if (i < scores.Count && scores[i] != null && scores[i] != 0){
                textboxes[i].text = (i+1) + "." + " High Score: " + scores[i] + "!";
            }
            else{
                textboxes[i].text = "No High Score!";
            }
        }
        
        

        
    }

    public void ButtonHandlerReset() {
        foreach(HighScoreResult highScore in RemoteHighScoreManager.Instance.highScoresFromDB){
            Debug.Log("Object ID " + highScore.objectId);
            RemoteHighScoreManager.Instance.SetHighScore(GetHighScore,0, highScore.objectId);
        }
        
    }



    void GetHighScore(){
        RemoteHighScoreManager.Instance.GetHighScore(UpdateUI);
    }

}

