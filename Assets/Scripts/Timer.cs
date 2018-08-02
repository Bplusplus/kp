using System;

using System.Collections;

using System.Collections.Generic;

using UnityEngine;

using UnityEngine.Events;

using UnityEngine.UI;



public class Timer : MonoBehaviour
{



    public float setTime = 15f;



    float startTime = 0f;



    float deltaTime = 0f;

    float t;

    string displayTime = "";



    bool timerIsRunning = false;

    public TextMesh text;

    public RoundManager rm;



    public delegate void OnTimesUp();

    public static event OnTimesUp TimesUp;



    // Use this for initialization

    void Start()
    {

        StartTimer();

    }



    // Update is called once per frame

    void Update()
    {

        if (timerIsRunning)
        {

            if (setTime-t <= 0)

            {

                timerIsRunning = false;

                TimesUp();

            }

            else
            {

                t += Time.deltaTime;

                displayTime = ConvertSteps2Time(setTime-t);

                text.text = displayTime;

            }

        }

    }





    public void StartTimer()

    {

        {

            timerIsRunning = true;

          //  startTime = Time.time;
          
            t = 0;

            displayTime = "";

            t = TimerCount();



            displayTime = ConvertSteps2Time(t);

            text.text = displayTime;

        }

    }



    float TimerCount()
    {



        deltaTime += Time.deltaTime;

        float dt = setTime - (deltaTime - startTime);

        return dt;



    }



    string ConvertSteps2Time(float steps)
    {

        string seconds = ((int)steps % 60).ToString("F0");

        string minutes = (((int)steps / 60) % 60).ToString("F0");

        return minutes + " : " + seconds;

    }
    public void pauseTimer() {
        timerIsRunning = !timerIsRunning;
        
    }
    public void resetTimer() {
        t = 0;
    }
}
