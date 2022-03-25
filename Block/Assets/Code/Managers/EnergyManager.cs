using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyManager : Manager
{
    public int EnergyBar { get; private set; }
    public Timer eventTimer;
    public Timer drainTimer;
    public int minimumTime = 1;
    public int maximumTime = 100;
    private float cameraShake = 5f;

    // Start is called before the first frame update
    public override void Start()
    {
        eventTimer = new Timer();
        EnergyBar = 100;
    }

    // Update is called once per frame
    public override void Update()
    {
        if (eventTimer.TimerDone())
        {
            eventTimer.StopTimer();
        }
    }


    public int SubstractEnergy(int amount)
    {
        return EnergyBar -= amount;
    }

    public int AddEnergy(int amount)
    {
        return EnergyBar += amount;
    }

    public void SetRandomTimer()
    {
        eventTimer.SetTimer(Random.Range(minimumTime, maximumTime));
    }


    public void RandomEvent(EventEnum _event, int _delay)
    {
        switch (_event)
        {
            case EventEnum.Thunderstorm:
                AddEnergy(50);
                break;
            case EventEnum.Earthquake:
                SubstractEnergy(10);
                
                break;
            default:
                break;
        }
       
        SetRandomTimer();
    }

}
public enum EventEnum
{
   Thunderstorm,
   Earthquake,
}