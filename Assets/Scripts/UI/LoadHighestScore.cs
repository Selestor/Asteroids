using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadHighestScore : MonoBehaviour {

    public Text highestScoreText;

    private void Awake()
    {
        highestScoreText.text = "Highest Score: \n" + PlayerPrefs.GetInt("highscore");
    }
}
