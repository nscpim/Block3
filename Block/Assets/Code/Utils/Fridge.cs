using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fridge : Interactable
{

    public void PlayAnimation()
    {
        print(anim.GetFloat("Direction"));
        if (anim.GetFloat("Direction") == 0)
        {
            Forward();
        }
        if (anim.GetFloat("Direction") == 1)
        {
            BackWards();
        }
        if (anim.GetFloat("Direction") == -1)
        {
            Forward();
        }
    }

    public void Forward()
    {
        anim.SetFloat("Direction", 1);
        anim.Play("Fridge animation rev");
    }

    public void BackWards()
    {
        anim.SetFloat("Direction", -1);
        anim.Play("Fridge animation rev");
    }
}
