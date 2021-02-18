using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SoundMangaer
{
    public enum Clip
    {
        Jump,
        Hit,
        Died,
        Score,
    }
    public static void PlaySound(Clip clip)
    {
        AudioClip audioClip = null;
        switch (clip)
        {
            case Clip.Jump:
                audioClip = GameAssets.GetInstance().BirdJumpClip;                
                break;
            case Clip.Hit:
                audioClip = GameAssets.GetInstance().BirdHitClip;
                break;
            case Clip.Died:
                audioClip = GameAssets.GetInstance().BirdDeathClip;
                break;
            case Clip.Score:
                audioClip = GameAssets.GetInstance().BirdScoreClip;
                break;
        }

        GameObject gameObject = new GameObject("Sound", typeof(AudioSource));
        AudioSource audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.PlayOneShot(audioClip);
        Object.Destroy(gameObject, 1);
    }

}
