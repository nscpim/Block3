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
        if (firstTime)
        {
            foreach (Light item in GameManager.instance.lights)
            {
                item.transform.gameObject.GetComponent<Interactable>().Interact(false, true, null);
                item.gameObject.SetActive(true);
                firstTime = false;
            }
        }
       
        generatorDraining = !generatorDraining;
        Display();
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
