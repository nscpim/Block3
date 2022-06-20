using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Generator : Interactable
{
    public static bool generatorDraining = false;
    public GameObject display;
    public GameObject screen;
   
    public static bool firstTime = true;
   


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
        Display();
        if (firstTime)
        {
            //3 minutes
            GameManager.instance.gameTimer.SetTimer(180);
            Player.instance.mayDrain = true;
            GameManager.GetManager<EnergyManager>().needsTimer.SetTimer(1);
            foreach (Light item in GameManager.instance.doorLights)
            {
                item.gameObject.SetActive(false);
            }
            foreach (GameObject item in GameManager.instance.lightObjects)
            {
                item.GetComponent<Interactable>().Interact(false, true, null);
                item.gameObject.GetComponent<Lights>().lightToggle = true;
            }
            foreach (Light item in GameManager.instance.lights)
            {
                item.gameObject.SetActive(true);
              
            }
            firstTime = false;
        }
        if (!generatorDraining)
        {
            GameManager.instance.Fansound.GetComponent<AudioSource>().Stop();
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
            screen.gameObject.GetComponent<Image>().sprite = GameManager.instance.onOff[0];
        }
        else 
        {
            screen.gameObject.GetComponent<Image>().sprite = GameManager.instance.onOff[1];
        }
    }

}
