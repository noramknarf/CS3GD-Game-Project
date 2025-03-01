using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    public Text timerText;
    
    private float remainingTime;


    // Start is called before the first frame update
    void Start()
    {
        remainingTime = 20;
    }

    // Update is called once per frame
    void Update()
    {
        remainingTime -= Time.deltaTime;
        timerText.text = string.Format("{0}", remainingTime);

    }
}
