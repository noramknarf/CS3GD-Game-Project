using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameTimer : MonoBehaviour
{
    public Text timerText;
    
    private float remainingTime;
    private int remainingMins;
    private int remainingSeconds;

    // Start is called before the first frame update
    void Start()
    {
        remainingTime = 20;
    }

    // Update is called once per frame
    void Update()
    {
        remainingTime -= Time.deltaTime;
        remainingMins = (int)(remainingTime / 60);
        remainingSeconds = (int)(remainingTime % 60);
        timerText.text = string.Format("{0:D2}:{1:D2}", remainingMins, remainingSeconds);
        
        if(remainingTime <= 0){
            SceneManager.LoadSceneAsync(1);
        }

    }
}
