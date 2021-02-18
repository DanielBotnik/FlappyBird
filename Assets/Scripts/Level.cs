using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    private const float PIPE_MOVE_SPEED = 0.7f;
    private const float REMOVE_PIPES_X = -2.0f;
    private const float PIPE_SPAWN_INTERVAL = 2.2f;
    private const float PIPE_SPACE_GAP = 0.9f;
    private const float PIPE_SPAWN_X = 4.0f;
    private const float BOX_COLLIDER_SPAWN_X = 0.4f;
    private const float GAP_LOWER_Y = -4.0f;
    private const float GAP_HEIGHT_Y = -0.6f;

    private List<Pair> pairs;
    private float nextPipeSpawn;   
    private static Level instance;
    private bool movePipes;

    private void Awake()
    {
        pairs = new List<Pair>();
        instance = this;
        nextPipeSpawn = float.Epsilon;
        UnityEngine.Random.InitState(System.DateTime.Now.Millisecond);
        movePipes = false;
    }

    public void Start()
    {
        Bird.GetInstanse().OnDeath += OnBirdDeath;
        Bird.GetInstanse().OnStart += OnBirdStart;
    }

    void Update()
    {
        if (movePipes)
        {
            HandlePipeSpawning();
            HandlePipeMovement();
        }
    }

    private void OnBirdDeath(object sender,EventArgs e)
    {
        movePipes = false;
    }
    
    private void OnBirdStart(object sender,EventArgs e)
    {
        movePipes = true;
    }

    private void HandlePipeSpawning()
    {
        nextPipeSpawn -= Time.deltaTime;
        if(nextPipeSpawn < 0)
        {
            nextPipeSpawn = PIPE_SPAWN_INTERVAL;
            CreatePipe();
        }
    }

    private void HandlePipeMovement()
    {
        for(int i = 0; i < pairs.Count; i++)
        {
            pairs[i].Move();
            if(pairs[i].GetX() < REMOVE_PIPES_X)
            {
                pairs[i].DestoryPair();
                pairs.RemoveAt(i--);
            }
        }
    } 

    private void CreatePipe()
    {
        float lowerPipeY = UnityEngine.Random.Range(GAP_LOWER_Y, GAP_HEIGHT_Y);
        Transform pipeUp = Instantiate(GameAssets.GetInstance().pfPipeUp);
        pipeUp.position = new Vector2(PIPE_SPAWN_X,lowerPipeY);
        Transform pipeDown = Instantiate(GameAssets.GetInstance().pfPipeDown);
        pipeDown.position = new Vector2(PIPE_SPAWN_X, 4.9f + lowerPipeY+PIPE_SPACE_GAP);
        Transform box2D = Instantiate(GameAssets.GetInstance().pfBoxTrigger);
        box2D.position = new Vector2(PIPE_SPAWN_X + BOX_COLLIDER_SPAWN_X, lowerPipeY + 2.7f);
        pairs.Add(new Pair(pipeUp, pipeDown,box2D));
    }

    public class Pair
    {
        private Transform pipeUpTransform;
        private Transform pipeDownTransform;
        private Transform scoreTriggerBoxTransform;

        public Pair(Transform pipeUpTransform, Transform pipeDownTransform,Transform scoreTriggerBoxTransform)
        {
            this.pipeUpTransform = pipeUpTransform;
            this.pipeDownTransform = pipeDownTransform;
            this.scoreTriggerBoxTransform = scoreTriggerBoxTransform;
        }

        public void Move()
        {
            // Cant += new Vector2 ):
            pipeUpTransform.position += Vector3.left * PIPE_MOVE_SPEED * Time.deltaTime;
            pipeDownTransform.position += Vector3.left * PIPE_MOVE_SPEED * Time.deltaTime;
            scoreTriggerBoxTransform.position += Vector3.left * PIPE_MOVE_SPEED * Time.deltaTime;
        }
        public void DestoryPair()
        {
            Destroy(pipeUpTransform.gameObject);
            Destroy(pipeDownTransform.gameObject);
            Destroy(scoreTriggerBoxTransform.gameObject);
        }

        public float GetX()
        {
            return pipeUpTransform.position.x;
        }
    }
}

