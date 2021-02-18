using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreWindow : MonoBehaviour
{

    private Text scoreText;

    private void Awake()
    {
        scoreText = transform.Find("ScoreText").GetComponent<Text>();
    }

    private void Start()
    {
        Bird.GetInstanse().OnPassPipes += UpdateScoreText;
    }


    private void UpdateScoreText(object o,EventArgs e)
    {
        scoreText.text = ((Bird)o).GetPipesPassed().ToString();
    }
}
