using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : Interactable
{
    public static bool generatorDraining = false;
    public GameObject display;
    public Material red;
    public Material green;
    public GameObject[] screens;
    public bool firstTime = true;

    // Start is called before the first frame update
    public override void Start()
    {
        display.gameObject.GetComponent<MeshRenderer>().material = red;
    }

    // Update is called once per frame
    public override void Update()
    {
        print("Generator" + CanDrain());
    }
    
    public bool ToggleDrain()
    {
        generatorDraining = !generatorDraining;
        Display();
        if (firstTime)
        {
            foreach (Light item in GameManager.instance.lights)
            {
                item.gameObject.SetActive(true);
               if (item.enabled) 
                {
                    item.GetComponent<Interactable>().canDrain = true;
                }  
            }
            firstTime = false;
        }
        return generatorDraining;
    }

    public static bool CanDrain()
    {
        return generatorDraining;
    }

    public void Display()
    {
        if (CanDrain())
        {
            display.gameObject.GetComponent<MeshRenderer>().material = green;
        }
        else 
        {
            display.gameObject.GetComponent<MeshRenderer>().material = red;
        }
    }

}
