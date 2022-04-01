using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{

    private string _name;
    public Animator anim;
   

    // Start is called before the first frame update
    public virtual void Start()
    {
        _name = gameObject.name;

    }

    // Update is called once per frame
   public virtual void Update()
    {
        
    }

    public void Interact(bool canPickUp, string animationName = default) 
    {
        if (canPickUp)
        {
            var inventory = GameManager.GetManager<InventoryManager>();
            if (inventory.CheckIfInInv(gameObject.name))
            {
                // GameManager.GetManager<AudioManager>().PlaySound();
                inventory.AddItem(gameObject);
                gameObject.SetActive(false);
            }
        }
        else
        {
            anim.Play(animationName);
        }
       
    }
}

