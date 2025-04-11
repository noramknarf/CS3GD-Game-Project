using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public int levelToLoad = 1;
    public Text scoreTextBox;
    public float totalScore = 0;
    // Start is called before the first frame update
    void Start()
    {
        if(scoreTextBox != null){
            UpdateScores();
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
    public void UpdateScores(){
        scoreTextBox.text = ("Your score:" + totalScore);
        if(PersistentDataHandler.instance != null){
            UpdateScores();
        }
    }
}
