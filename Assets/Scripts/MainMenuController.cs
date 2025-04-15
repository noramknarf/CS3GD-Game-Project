using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public int levelToLoad = 1;
    public Text scoreTextBox;
    public Text PBTextBox;
    public Text totalTextBox;
    public int previousLevel;
    private float totalScore;
    private float personalBest;
    private float prevLevelScore;
    private PersistentDataHandler dataHandler = PersistentDataHandler.instance;
    // Start is called before the first frame update
    void Start()
    {   
        totalScore = 0.0f;
        personalBest = 0.0f;
        prevLevelScore = 0.0f;
        
        //If the data object exists, check scores against personal bests and update them appropriately
        if(dataHandler != null && previousLevel != 0){
            UpdateScores();
        }
        //if the menu has a textbox for the previous level, show the last level's score
        if(scoreTextBox != null){
            DisplayPrevLevel();
        }
        //if there is a textbox for the PB, display the relevant level's PB or total PB depending on the number passed.
        //for now just looks at the first index in the dataHandler's PBs array.
        if(PBTextBox != null){
            DisplayPersonalBest();
        }
        //if there is a textbox for the total score, display that
        if(totalTextBox != null){
            DisplayTotal();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ButtonControllerPlay(){
        SceneManager.LoadSceneAsync(levelToLoad);
    }

    public void ExitToMenu(){
        SceneManager.LoadSceneAsync(0);
    }
    public void ExitGame(){
        Application.Quit();
    }
    public void DisplayPrevLevel(){
        //TODO: Add a parameter so this can be told what the previous level was.
        //perhaps might split into a separate method for PBs
        
        if (dataHandler != null && dataHandler.remainingTime != null){
            prevLevelScore = dataHandler.remainingTime;
        }
        
        scoreTextBox.text = ("Your score: " + prevLevelScore); //left outside the if statement so that the scoreboard will always at least have a placeholder value if dataHandler doesn't exist.
        
    }

    public void DisplayPersonalBest(){
        if (dataHandler != null && dataHandler.personalBests[previousLevel-1] != null){
            personalBest = dataHandler.personalBests[previousLevel-1];
        }
        PBTextBox.text = ("Personal best: " + personalBest);
    }

    public void DisplayTotal(){
        totalTextBox.text = ("Total score: " + totalScore);
    }

    public void UpdateScores(){
        //checks if there is a score greater than the most recent score in the relevant slot of the personal bests array and, if not, stores that score in the slot.
            //Will need to be able to handle different levels later but for now, just assuming level 1.
            Debug.Log("PreviousLevel = " + previousLevel);
            Debug.Log("PreviousLevel -1 = " + (previousLevel-1) );

            print("Personal bests =" + dataHandler.personalBests[0]);
            print(dataHandler.personalBests[1]);
            print(dataHandler.personalBests[(previousLevel-1)]);
            if (dataHandler.personalBests[previousLevel-1] == null || dataHandler.personalBests[previousLevel-1] < dataHandler.remainingTime){
                dataHandler.personalBests[previousLevel-1] = dataHandler.remainingTime; 
            }
            foreach(float score in dataHandler.personalBests){
                totalScore += score; // totals up the scores in the PB. May do weird things if levels are played out of order but for now doesn't matter.
            } // For the moment, maybe it'd be best to have most menus just display the last level's score, then have the total only used for the final victory screen.
            
    }
}
