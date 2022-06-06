using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GeneratorScreen : Interactable
{
    public static GeneratorScreen Instance { get; private set; }
    public Image genImage;
    private float energy;
    public Sprite[] battery;
    private bool toggleScreen = true;
    // Start is called before the first frame update
    private void Awake()
    {
            Instance = this;
    }
    public override void Start()
    {
       
    }
    // Update is called once per frame
    public override void Update()
    {
        if (genImage.isActiveAndEnabled)
        {
            SetBattery();
        }
        energy = GameManager.GetManager<EnergyManager>().energyBar;
    }
    public void ToggleScreen() 
    {
        toggleScreen = !toggleScreen;
        genImage.gameObject.SetActive(toggleScreen);
        if (toggleScreen)
        {
            SetBattery(); 
        }
    }

    public void SetBattery() 
    {
        if (energy > 74)
        {
            genImage.sprite = battery[0];
        }
        else if (energy < 75 && energy > 49)
        {
            genImage.sprite = battery[1];
        }
        else if (energy < 51 && energy > 24)
        {
            genImage.sprite = battery[2];
        }
        else if (energy < 26 && energy > 0)
        {
            genImage.sprite = battery[3];
        }
        else if (energy <= 0)
        {
            genImage.sprite = battery[4];
        }
    }
}
