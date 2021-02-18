using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssets : MonoBehaviour
{
    private static GameAssets instance;

    public Transform pfPipeUp;
    public Transform pfPipeDown;
    public Transform pfBoxTrigger;

    public AudioClip BirdJumpClip;
    public AudioClip BirdDeathClip;
    public AudioClip BirdHitClip;
    public AudioClip BirdScoreClip;

    public void Awake()
    {
        instance = this;
    }
    
    public static GameAssets GetInstance()
    {
        return instance;
    }


}
