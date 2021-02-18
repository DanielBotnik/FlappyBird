using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour
{
    
    public GameObject CanvasStart; // Tried dealing with it with events but got to tired
    public GameObject scoreCanvas;
    public GameObject floor;

    public event EventHandler OnStart;
    public float rand;
    public static GameHandler instanse;

    private void Awake()
    {
        rand = UnityEngine.Random.Range(1,100);
        instanse = this;
    }
    public static GameHandler GetInstanse()
    {
        return instanse;
    }

    public void HandleStart()
    {
        CanvasStart.SetActive(false);
        scoreCanvas.SetActive(true);
        Animator animation = floor.GetComponent<Animator>();
        animation.enabled = true;
        if(OnStart != null)
            OnStart(this,null);
        Time.timeScale = 1;
    }

    public void HandleRestart()
    {
        SceneManager.LoadScene("GameScene");
    }
}
