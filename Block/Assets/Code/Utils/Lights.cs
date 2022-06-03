using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lights : Interactable
{

    public Light[] lights;
    private bool lightToggle = false;

    // Start is called before the first frame update
   public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        
    }


    public void ToggleLights() 
    {
        lightToggle = !lightToggle;
        foreach  (Light i in lights)
        {
            i.transform.gameObject.SetActive(lightToggle);
        }
    }


    public bool GetState() 
    {
        return lightToggle;
    }
}
