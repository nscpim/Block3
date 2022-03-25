using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{

    private string _name;
    public Animator anim;
   

    // Start is called before the first frame update
    public void Start()
    {
        _name = gameObject.name;
    }

    // Update is called once per frame
   public void Update()
    {

    }

    public void Interact() 
    {
        var inventory = GameManager.GetManager<InventoryManager>();
        Debug.Log("Object name: " + _name);
        if (inventory.CheckIfInInv(gameObject.name))
        {
           // GameManager.GetManager<AudioManager>().PlaySound();
            inventory.AddItem(gameObject);
            gameObject.SetActive(false);
        }
        
        //anim.Play("open");


       
    }
}
