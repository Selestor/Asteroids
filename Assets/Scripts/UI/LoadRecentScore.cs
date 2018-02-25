using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadRecentScore : MonoBehaviour
{
    public Text recentScoreText;

    private void Awake()
    {
        recentScoreText.text = "Your Score: \n" + PlayerPrefs.GetInt("score");
    }
}
