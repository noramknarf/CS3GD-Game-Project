using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameTimer : MonoBehaviour
{
    public Text timerText;
    
    public float remainingTime = 20;
    private int remainingMins;
    private int remainingSeconds;
    private bool loading = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        remainingTime -= Time.deltaTime;
        remainingMins = (int)(remainingTime / 60);
        remainingSeconds = (int)(remainingTime % 60);
        timerText.text = string.Format("{0:D2}:{1:D2}", remainingMins, remainingSeconds);
        
        if(remainingTime <= 0 && loading == false){
            SceneManager.LoadSceneAsync(2);
            loading = true;
        }

    }
}
