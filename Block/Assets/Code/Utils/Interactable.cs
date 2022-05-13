using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
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

    public void Interact(bool canPickUp, bool drain, GameObject objectPickedUp) 
    {
        if (canPickUp && objectPickedUp != null && !Player.instance.hasObject)
        {
            if (objectPickedUp.GetComponent<Rigidbody>() == null)
            {
                objectPickedUp.AddComponent<Rigidbody>();
            }
            objectPickedUp.GetComponent<Rigidbody>().isKinematic = true;
            objectPickedUp.transform.position = GameManager.instance.player.handLocation.transform.position;
            objectPickedUp.transform.SetParent(GameManager.instance.player.handLocation.transform, true);
            Player.instance.hasObject = true;
        }
        bool generator = Generator.CanDrain();

        if (drain && !canDrain && generator)
        {
            GameManager.GetManager<EnergyManager>().AddDrainage(drainAmount);
            canDrain = true;
        }
        else if(drain && canDrain && generator)
        {
            GameManager.GetManager<EnergyManager>().RemoveDrainage(drainAmount);
            canDrain = false;
        }
      
        
     
    }
}

