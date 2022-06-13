using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fridge : Interactable
{
    private Timer cooldown;

    public override void Start()
    {
        cooldown = new Timer();
    }

    public override void Update()
    {
        if (cooldown.TimerDone() && cooldown.isActive)
        {
            cooldown.StopTimer();
        }
    }

    public void PlayAnimation()
    {
        if (!anim.GetBool("forward"))
        {
            Forward();
            anim.SetBool("forward", true);

        }
        else
        {
            anim.SetBool("forward", false);
        }
    }

    public void Forward()
    {
        anim.Play("Fridge animation rev");
        if (!cooldown.isActive)
        {
            GameManager.GetManager<EnergyManager>().AddNeeds(needsAmount);
            cooldown.SetTimer(10f);
        }
       
    }

    public void BackWards()
    {
        anim.Play("Fridge animation rev back");
    }


}
