using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fridge : Interactable
{

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
    }

    public void BackWards()
    {
        anim.Play("Fridge animation rev back");
    }

    
}
