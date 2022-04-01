using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyManager : Manager
{
    public float needsBar { get; private set; }
    public float EnergyBar { get; private set; }
    public Timer eventTimer;
    public Timer drainTimer;
    private int drainage = 0;
    public int minimumTime = 1;
    public int maximumTime = 100;
    public bool canDrain = false;
    private int delay = 30;
    private int eventInt;
    private EventEnum eventDummy;



    // Start is called before the first frame update
    public override void Start()
    {
        eventTimer = new Timer();
        drainTimer = new Timer();
        EnergyBar = 100f;
        drainTimer.SetTimer(1);
        ShowEvent(Random.Range(0, 2));
        UpdateBar();
        
    }

    // Update is called once per frame
    public override void Update()
    {
        //If statements so our energy bar doesnt go out of bounds
        if (EnergyBar >= 100)
        {
            EnergyBar = 100;
        }
        if (EnergyBar <= 0)
        {
            EnergyBar = 0;
        }

        if (eventTimer.isActive && eventTimer.TimerDone())
        {
            eventTimer.StopTimer();
            ExecuteEvent();
        }

        UpdateBar();

        if (drainTimer.TimerDone() && drainTimer.isActive)
        {
            drainTimer.StopTimer();
            SubstractEnergy(5f);
           
            drainTimer.SetTimer(2);
        }
        /*  if (!canDrain && drainTimer.isActive)
          {
              drainTimer.PauseTimer(true);
          }
        */
    }


    public float SubstractEnergy(float amount)
    {
        return EnergyBar -= amount;
    }

    public float AddEnergy(float amount)
    {
        return EnergyBar += amount;
    }

    public void SetRandomTimer()
    {
        eventTimer.SetTimer(Random.Range(minimumTime, maximumTime));
    }

    public void UpdateBar()
    {
        GameManager.instance.energyBarSlider.value = EnergyBar;
        GameManager.instance.eventText.text = eventDummy.ToString() + " in : " + (int)eventTimer.TimeLeft() + "    Energy: " + (int)EnergyBar;
        Debug.Log(EnergyBar);
    }
    public void ShowEvent(int _event)
    {
        eventInt = _event;
        switch (_event)
        {
            case (int)EventEnum.Thunderstorm:
                eventDummy = EventEnum.Thunderstorm;
                break;
            case (int)EventEnum.Earthquake:
                eventDummy = EventEnum.Earthquake;
                break;
            case (int)EventEnum.None:
                break;
            default:
                break;
        }
        SetRandomTimer();
    }
    public void ExecuteEvent()
    {
        switch (eventInt)
        {
            case (int)EventEnum.Thunderstorm:
                AddEnergy(20);
                break;
            case (int)EventEnum.Earthquake:
                Utils.instance.StartShake();
                SubstractEnergy(20);
                break;
            case (int)EventEnum.None:
                break;
            default:
                break;
        }
        ShowEvent(Random.Range(0, 2));
    }



    //End game
    public void EnergyDepleted()
    {

    }

    public int AddDrainage(int _amount)
    {
        return drainage += _amount;
    }
    public int RemoveDrainage(int _amount)
    {
        return drainage -= _amount;
    }
}
public enum EventEnum
{
    Thunderstorm,
    Earthquake,
    None,
}
