using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : Interactable
{

    public void PlayAnimation()
    {
        if (!anim.GetBool("open"))
        {
            open();
            anim.SetBool("open", true);

        }
    }

    public void open()
    {
        anim.Play("Door animation opening");
        StartCoroutine(ClosingDoor());
        

    }

    public void BackWards()
    {
        anim.Play("Door animation closing");
    }

    private IEnumerator ClosingDoor()
    {
        yield return new WaitForSeconds(5);
        anim.SetBool("open", false);
        BackWards();    
    }
}



    