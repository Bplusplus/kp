using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class AudioManager : MonoBehaviour
{

    public delegate Event play(int i);
    public AudioClip[] files;
    public AudioClip[] bounceSounds;

    public AudioClip[] scoreSounds;

    public AudioSource player;

    // Use this for initialization
    void Start()
    {
        //player = Camera.main.GetComponent<AudioSource>();
        BallMovement.ballHit += PlayBounce;
        BallMovement.score += PlayScore;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayBounce(int i)
    {

        if (i < bounceSounds.Length && bounceSounds != null)
        {
           
                player.PlayOneShot(bounceSounds[i]);
           



        }



    }
    public void PlayScore(int i)

    {

        if (i < scoreSounds.Length && scoreSounds != null)

        {

            player.PlayOneShot(scoreSounds[i]);



        }




    }
}
