using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public int levelToLoad = 1;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("This should be doing something?");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ButtonControllerPlay(){
        SceneManager.LoadSceneAsync(levelToLoad);
    }

    public void ExitGame(){
        Application.Quit();
    }
}
