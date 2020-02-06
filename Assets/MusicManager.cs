using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{

    public AudioClip GameClip;
    public AudioClip  ElevatorClip;
    public AudioSource MySource; 

    public static void ElevatorMusic()
    {
        CBUG.Do("Elevator Music!");
        FindObjectOfType<MusicManager>().MySource.Stop();
        FindObjectOfType<MusicManager>().MySource.clip = GameObject.FindObjectOfType<MusicManager>().ElevatorClip;
        FindObjectOfType<MusicManager>().MySource.Play();
    }

    public static void GameMusic()
    {
        CBUG.Do("Game Music!");
        FindObjectOfType<MusicManager>().MySource.Stop();
        FindObjectOfType<MusicManager>().MySource.clip = GameObject.FindObjectOfType<MusicManager>().GameClip;
        FindObjectOfType<MusicManager>().MySource.Play();
    }

}
