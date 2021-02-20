using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    
    private const float JUMP_FORCE = 180f;
    private const float DEATH_FORCE = 100f;
    private const float DEATH_SOUND_DELAY = 1.5f;
    private const float DEATH_HANDLE_DELAY = 2f;

    public event EventHandler OnDeath;
    public event EventHandler OnPassPipes;
    public event EventHandler OnStart;

    private int pipesPassed;
    private State state;

    private static Bird instance;
    private enum State
    {
        WaitingForStart,
        Waiting,
        Playing,
        Dead,
    }
    public static Bird GetInstanse()
    {
        return instance;
    }

    private Rigidbody2D rigidBody;

    private void Awake()
    {
        instance = this;
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.bodyType = RigidbodyType2D.Static;
        pipesPassed = 0;
        state = State.WaitingForStart;
    }

    private void Start()
    {
        GameHandler.GetInstanse().OnStart += ChangeToWaiting;
    }

    private void ChangeToWaiting(object o,EventArgs e)
    {
        if(state == State.WaitingForStart)
            state = State.Waiting;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))  
        {
            Jump();
        }

        CheckBirdNotFallingToVoid();
        
    }

    private void Jump()
    {
        if (state == State.Waiting)
        {
            rigidBody.bodyType = RigidbodyType2D.Dynamic;
            rigidBody.velocity = Vector2.zero;
            rigidBody.AddForce(new Vector2(0, 1) * JUMP_FORCE);
            OnStart(this, EventArgs.Empty);
            state = State.Playing;
            SoundMangaer.PlaySound(SoundMangaer.Clip.Jump);

        }
        else if (state == State.Playing)
        {
            rigidBody.velocity = Vector2.zero;
            rigidBody.AddForce(new Vector2(0, 1) * JUMP_FORCE);
            SoundMangaer.PlaySound(SoundMangaer.Clip.Jump);
        }
    }

    private void CheckBirdNotFallingToVoid()
    {
        if (rigidBody.position.y < -5) // to prevent bird from falling up to -inf
        {
            rigidBody.bodyType = RigidbodyType2D.Static;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {   
        if(state != State.Dead)
        {
            if (OnDeath != null)
                OnDeath(this, EventArgs.Empty);
            rigidBody.AddForce(new Vector2(-0.4f, 0.4f) * DEATH_FORCE);
            rigidBody.rotation += 45f;
            SoundMangaer.PlaySound(SoundMangaer.Clip.Hit);
            Invoke("PlayDeathSound", DEATH_SOUND_DELAY);
            Invoke("HandleDeath", DEATH_HANDLE_DELAY);
            state = State.Dead;
        }
      
    }

    private void PlayDeathSound()
    {
        SoundMangaer.PlaySound(SoundMangaer.Clip.Died);
    }

    private void HandleDeath()
    {
        GameHandler.GetInstanse().HandleRestart();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(state == State.Dead)
        {
            return;
        }
        pipesPassed++;
        if (OnPassPipes != null)
            OnPassPipes(this,EventArgs.Empty);
        SoundMangaer.PlaySound(SoundMangaer.Clip.Score);
    }

    public int GetPipesPassed()
    {
        return pipesPassed;
    }
}
