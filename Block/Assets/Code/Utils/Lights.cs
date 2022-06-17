using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lights : Interactable
{
    public Image lightState;
    public Sprite[] onOff;
    public Light[] lights;
    public bool lightToggle = false;

    // Start is called before the first frame update
   public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        if (lightToggle)
        {
            lightState.sprite = onOff[0];
        }
        else
        {
            lightState.sprite = onOff[1];
        }
    }


    public void ToggleLights() 
    {
        lightToggle = !lightToggle;
        foreach  (Light i in lights)
        {
            i.transform.gameObject.SetActive(lightToggle);
        }
    }
    public void SetToggleLights(bool _toggle)
    {
        foreach (Light i in lights)
        {
            i.transform.gameObject.SetActive(_toggle);
        }
    }


    public bool GetState() 
    {
        return lightToggle;
    }
}
