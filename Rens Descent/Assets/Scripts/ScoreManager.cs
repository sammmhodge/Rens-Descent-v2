using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{

    public static ScoreManager Instance;

    public Text scoreText, timeText;
    public int score = 10000;
    float levelTime;
    float startTime;
    int roundedTime;
    // Start is called before the first frame update
    void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        startTime = Time.time;

    }

    // Update is called once per frame
    void Update()
    {
        levelTime = Time.time - startTime;
        roundedTime = Mathf.RoundToInt(levelTime);
        timeText.text = roundedTime.ToString();

        scoreText.text = (score - roundedTime).ToString();


    }

    public void resetTimer()
    {
        score -= roundedTime;
        startTime = Time.time;
    }
}
