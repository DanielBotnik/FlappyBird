using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{

    private Button startButton;

    public event EventHandler startGame;

    private void Awake()
    {
        startButton = GetComponent<Button>();
        startButton.onClick.AddListener(ButtonClicked);
    }

    private void ButtonClicked()
    {
        GameHandler.GetInstanse().HandleStart();
    }
}
