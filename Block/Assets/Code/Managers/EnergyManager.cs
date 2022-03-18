using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyManager : Manager
{
    public int consumption { get; private set; }
    

    // Start is called before the first frame update
    public override void Start()
    {
        consumption = 0;
    }

    // Update is called once per frame
    public override void Update()
    {
        
    }


    public int SubstractEnergy(int amount)
    {
        return consumption -= amount;
    }

    public int AddEnergy(int amount)
    {
        return consumption += amount;
    }


}
