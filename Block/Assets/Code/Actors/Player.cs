using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Actor
{
    public int needsBar { get; private set; }


    public void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    public void Start()
    {

    }

    // Update is called once per frame
    public void Update()
    {
        var energyManager = GameManager.GetManager<EnergyManager>();
        if (Input.GetKeyDown(KeyCode.A))
        {
            GameManager.GetManager<UIManager>().ShowTempUI(5, "Test", 100, 100);
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (energyManager.eventTimer.isActive)
            {
                energyManager.eventTimer.StopTimer();
                energyManager.eventTimer.SetTimer(Random.Range(energyManager.minimumTime, energyManager.maximumTime));
            }
            else
            {
                energyManager.eventTimer.SetTimer(Random.Range(energyManager.minimumTime, energyManager.maximumTime));
            }
        }
    }
}