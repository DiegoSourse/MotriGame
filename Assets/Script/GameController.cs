using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshPro   gameTimeText;
    public int InitMin = 0;
    public int InitSec = 15;
    float gameTimer = 0f;
    void Start()
    {
        gameTimeText.text = "00:00";
        gameTimer = (InitMin * 60) + InitSec;
    }

    // Update is called once per frame
    void Update()
    {
        int seconds = (int)(gameTimer % 60);
        int minutes = (int)(gameTimer / 60) % 60;

        string StringTime = string.Format("{0:00}:{1:00}",minutes,seconds);

        gameTimeText.text = StringTime;
        //Debug.Log("Time:" + Time.deltaTime);
        if (gameTimer>0)
        {
            gameTimer -= Time.deltaTime;
        }
        
        if (seconds <= 10 && minutes==0)
            gameTimeText.color = Color.red;
    }
}
