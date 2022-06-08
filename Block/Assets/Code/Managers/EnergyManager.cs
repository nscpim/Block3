using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyManager : Manager
{
    public float needsBar { get; private set; }
    public float energyBar;
    public Timer eventTimer;
    public Timer drainTimer;
    public Timer needsTimer;
    public Timer lightsFlickering;
    public Timer endTimer;
    private float drainage = 0;
    private float needsDrainage;
    public int minimumTime = 20;
    public int maximumTime = 180;
    public int eventInt { get; private set; }
    private EventEnum eventDummy;
    private float lightsflicking = 1f;
    private bool eventComing;
    public bool canReceivePower = false;

    // Start is called before the first frame update
    public override void Start()
    {
        eventTimer = new Timer();
        drainTimer = new Timer();
        endTimer = new Timer();
        lightsFlickering = new Timer();
        needsTimer = new Timer();
        energyBar = 100f;
        needsBar = 100f;
        needsDrainage = 1.5f;
        drainTimer.SetTimer(1);
        needsTimer.SetTimer(1);
        ShowEvent(Random.Range(0, 2));
        ComputerScreen.Instance.ToggleScreen();
        UpdateBar();
    }

    // Update is called once per frame
    public override void Update()
    {
        //If statements so our energy bar doesnt go out of bounds
        if (energyBar >= 100)
        {
            energyBar = 100;
        }
        if (energyBar <= 0)
        {
            energyBar = 0;
            endTimer.SetTimer(5);
        }

        if (needsBar >= 100)
        {
            needsBar = 100;
        }
        if (needsBar <= 0)
        {
            needsBar = 0;
        }
       
        //Under 25%
        if (energyBar < 26 && !lightsFlickering.isActive)
        {
            lightsFlickering.SetTimer(lightsflicking);
        }
        else if (energyBar <= 0)
        {
            foreach (Light i in GameManager.instance.lights)
            {
                i.color = Color.black;
            }
        }
        else if (energyBar > 25 && !eventComing)
        {
            foreach (Light i in GameManager.instance.lights)
            {
                i.color = Color.white;
            }
        }
        else if (energyBar > 25 && eventComing)
        {
            foreach (Light i in GameManager.instance.lights)
            {
                i.color = Color.red;
            }
        }
        if (eventTimer.TimeLeft() < 11)
        {
            eventComing = true;
        }
        else if (eventTimer.TimeLeft() > 5)
        {
            eventComing = false;
        }

        if (lightsFlickering.TimerDone() && lightsFlickering.isActive)
        {
            lightsFlickering.StopTimer();
            GameManager.instance.Flick();
        }
        Debug.Log(energyBar + " " + needsBar);


        if (eventTimer.isActive && eventTimer.TimerDone())
        {
            eventTimer.StopTimer();
            ExecuteEvent();
        }


        if (Generator.CanDrain())
        {
            if (drainTimer.isPaused)
            {
                drainTimer.PauseTimer(false);
            }
            else
            {
                if (drainTimer.isActive && drainTimer.TimerDone())
                {
                    drainTimer.StopTimer();
                    SubstractEnergy(drainage);
                    drainTimer.SetTimer(2);
                }
            }
        }
        else
        {
            if (drainTimer.isActive)
            {
                drainTimer.PauseTimer(true);
            }
        }

        if (needsTimer.isActive && needsTimer.TimerDone())
        {
            needsTimer.StopTimer();
            RemoveNeeds(needsDrainage);
            UpdateBar();
            needsTimer.SetTimer(2);
        }

        if (endTimer.isActive && endTimer.TimerDone())
        {
            endTimer.StopTimer();
            GameManager.EndGame(energyBar, needsBar);
        }
    }


    public float SubstractEnergy(float amount)
    {
        return energyBar -= amount;
    }

    public float AddEnergy(float amount)
    {
        return energyBar += amount;
    }

    public void SetRandomTimer()
    {
        eventTimer.SetTimer(Random.Range(minimumTime, maximumTime));
    }

    public void UpdateBar()
    {
        GameManager.instance.energyBarSlider.value = energyBar;
        //GameManager.instance.needsBarSlider.value = needsBar;
        GameManager.instance.eventText.text = eventDummy.ToString() + " in : " + (int)eventTimer.TimeLeft() + "    Energy: " + (int)energyBar;
        if (needsBar <= 100 && needsBar >= 50)
        {
            GameManager.instance.phone.GetComponent<Renderer>().material = GameManager.instance.green;
            GameManager.instance.phoneLight.color = Color.green;
        }
        if (needsBar <= 50 && needsBar >= 25)
        {
            GameManager.instance.phone.GetComponent<Renderer>().material = GameManager.instance.orange;
            Color color = new Color32(255, 165, 0, 255);
            GameManager.instance.phoneLight.color = color;
        }
        if (needsBar <= 25 && needsBar >= 0)
        {
            GameManager.instance.phone.GetComponent<Renderer>().material = GameManager.instance.red;
            GameManager.instance.phoneLight.color = Color.red;

        }
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
                if (canReceivePower)
                {
                    AddEnergy(10);
                    canReceivePower = false;
                }
                break;
            case (int)EventEnum.Earthquake:
                if (Generator.CanDrain())
                {
                    SubstractEnergy(20);
                }
                Utils.instance.StartShake();
                break;
            case (int)EventEnum.None:
                break;
            default:
                break;
        }
        ShowEvent(Random.Range(0, 2));
        ComputerScreen.Instance.SetEventImage(eventInt);
    }


    public int GetEvent() 
    {
        return eventInt;
    }
 

    public float AddDrainage(float _amount)
    {
        return drainage += _amount;
    }
    public float RemoveDrainage(float _amount)
    {
        return drainage -= _amount;
    }


    public float AddNeeds(float _amount)
    {
        return needsBar += _amount;
    }
    public float RemoveNeeds(float _amount)
    {
        return needsBar -= _amount;
    }
}
public enum EventEnum
{
    Thunderstorm,
    Earthquake,
    None,
}
