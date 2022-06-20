using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : Interactable
{
    
    public void PlayAnimation()
    {
        if (!anim.GetBool("open"))
        {
            GameManager.instance.openedDoor = true;
            open();
            anim.SetBool("open", true);
        }
    }
    public void open()
    {
        anim.Play("Door animation opening");
        StartCoroutine(ClosingDoor());
        Player.instance.canPlay = false;
        

    }
    public void BackWards()
    {
        GameManager.GetManager<AudioManager>().PlaySound("Doorslide");
        anim.Play("Door animation closing");
        Player.instance.canPlay = true;
    }
    private IEnumerator ClosingDoor()
    {
        yield return new WaitForSeconds(5);
        anim.SetBool("open", false);
        BackWards();    
    }
}



    