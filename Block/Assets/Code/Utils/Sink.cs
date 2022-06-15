using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sink : Interactable
{
    public Animator animator2;
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
        anim.Play("Sinkhandle on");
        GameManager.GetManager<EnergyManager>().AddNeeds(needsAmount);
    }

    public void BackWards()
    {
        anim.Play("Sinkhandle off");
    }


}
