using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer
{
    //Time.Deltatime.
    private float timeStamp;
    //Interval for how long the timer runs.
    private float interval;
    //Timeleft when the timer is paused.
    private float pauseDifference;

    public bool isPaused { get; private set; }
    public bool isActive { get; private set; }


    //Return the timeleft for the timer
    public float TimeLeft()
    {
        return TimerDone() ? 0 : (1 - TimerProgress()) * interval;
    }

    //return the progress of the timer
    public float TimerProgress()
    {
        return (isPaused) ? (interval - pauseDifference / interval) : TimerDone() == true ? 1 : Mathf.Abs((timeStamp - Time.time) / interval);
    }

    //returns boolean if the timer is done or not
    public bool TimerDone()
    {
        return (isPaused) ? pauseDifference == 0.0f : Time.time >= timeStamp + interval ? true : false;
    }

    //Sets the timer with the given interval
    public void SetTimer(float _interval = 2)
    {
        timeStamp = Time.time;
        interval = _interval;
        isActive = true;
    }

    //Restarts the timer with the same values
    public void RestartTimer()
    {
        SetTimer(interval);
    }

    //Stops the timer
    public void StopTimer()
    {
        isActive = false;
        timeStamp = interval;
    }

    //Pauses the timer
    public void PauseTimer(bool pause)
    {
        if (pause)
        {
            pauseDifference = TimeLeft();
            isPaused = pause;
            return;
        }
        isPaused = pause;
        timeStamp = Time.time - (interval - pauseDifference);
    }

}
