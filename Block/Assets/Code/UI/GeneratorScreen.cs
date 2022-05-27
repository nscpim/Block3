using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GeneratorScreen : Interactable
{
    public Image[] eventImages;
    public Image genImage;
    // Start is called before the first frame update
    public override void Start()
    {

    }
    // Update is called once per frame
    public override void Update()
    {

    }
    public void SwitchImage(EventEnum eventValue)
    {
        switch (eventValue)
        {
            case EventEnum.Thunderstorm:
                genImage = eventImages[0];
                break;
            case EventEnum.Earthquake:
                genImage = eventImages[1];
                break;
            case EventEnum.None:
                break;
            default:
                break;
        }
    }
}
