using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorManager : Manager
{

    public bool generatorStatus;

    // Start is called before the first frame update
    public override void Start()
    {

    }

    // Update is called once per frame
    public override void Update()
    {
       
    }

    public void TurnOnGenerator() 
    {
        ToggleGenStatus();
    }


    public bool GetGenStatus() 
    {
        return generatorStatus;
    }

    public void ToggleGenStatus() 
    {
        generatorStatus = !generatorStatus;
    }


}
