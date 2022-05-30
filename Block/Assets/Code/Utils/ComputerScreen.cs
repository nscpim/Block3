using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComputerScreen : Interactable
{
    public static ComputerScreen Instance { get; private set; }

   
    public GameObject sourceObject;
    public Sprite[] events;
    private bool toggleScreen = true;

    ComputerScreen() 
    {
    
    Instance = this;   
    
    }

    // Start is called before the first frame update
    public override void Start()
    {
       
    }

    // Update is called once per frame
    public override void Update()
    {
        
    }

    public void ToggleScreen() 
    {
        toggleScreen = !toggleScreen;
        sourceObject.SetActive(toggleScreen);
        if (toggleScreen)
        {
            SetEventImage(GameManager.GetManager<EnergyManager>().eventInt);
            print(GameManager.GetManager<EnergyManager>().eventInt + " EventInteger");
        }
    }
    public void SetEventImage(int eventInteger) 
    {
        switch (eventInteger)
        {
            case (int)EventEnum.Thunderstorm:
                sourceObject.GetComponent<Image>().sprite = events[0];
                break;
            case (int)EventEnum.Earthquake:
                sourceObject.GetComponent<Image>().sprite = events[1];
                break;
            case (int)EventEnum.None:
                break;
            default:
                break;
        }

    }

}
