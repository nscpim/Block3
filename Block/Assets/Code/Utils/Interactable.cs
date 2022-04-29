using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [Tooltip("Only use values that are needed for the object you are using it for,leave blank if you dont need it")]
    public float drainAmount;
    private string _name;
    public Animator anim;
    public bool canDrain = false;
    public float needsAmount;
   
   

    // Start is called before the first frame update
    public virtual void Start()
    {
        _name = gameObject.name;

    }

    // Update is called once per frame
   public virtual void Update()
    {
        
    }

    public void Interact(bool canPickUp, bool drain) 
    {
        if (canPickUp)
        {
            var inventory = GameManager.GetManager<InventoryManager>();
            GameObject objectList = gameObject;
            if (inventory.CheckIfInInv(objectList.name))
            {
                // GameManager.GetManager<AudioManager>().PlaySound();
                inventory.AddItem(objectList);
                gameObject.SetActive(false);
            }
        }
        if (drain && !canDrain)
        {
            GameManager.GetManager<EnergyManager>().AddDrainage(drainAmount);
            canDrain = true;
        }
        else if(drain && canDrain)
        {
            GameManager.GetManager<EnergyManager>().RemoveDrainage(drainAmount);
            canDrain = false;
        }
      
        
     
    }
}

