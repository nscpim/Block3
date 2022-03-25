using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : Manager
{
    public List<GameObject> items = new List<GameObject>();
    
    private bool inInv;

    // Start is called before the first frame update
    public override void Start()
    {
       
    }

    // Update is called once per frame
   public override void Update()
    {
        
    }
    public bool CheckIfInInv(string _name) 
    {
        foreach (GameObject item in items)
        {
            if (item.name == _name)
            {
                inInv = true;
            }
            else
            {
                inInv = false;
            }
            
        }
        return inInv;
    }
    public void AddItem(GameObject _gameobject) 
    {
        items.Add(_gameobject);
    }
    public void RemoveItem(GameObject _gameobject) 
    {
        items.Remove(_gameobject);
    }
}
