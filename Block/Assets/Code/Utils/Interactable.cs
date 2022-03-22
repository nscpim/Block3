using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{

    private string name;
    // Start is called before the first frame update
    void Start()
    {
        name = gameObject.name;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Interact() 
    {
        Debug.Log("Object name: " + name);
    
    }
}
