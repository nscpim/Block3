using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : Interactable
{
    public static bool generatorDraining = false;


    // Start is called before the first frame update
    public override void Start()
    {
        
    }

    // Update is called once per frame
    public override void Update()
    {
        print("Generator" + CanDrain());
    }
    
    public bool ToggleDrain()
    {
        generatorDraining = !generatorDraining;
        return generatorDraining;
    }

    public static bool CanDrain()
    {
        return generatorDraining;
    }
}
