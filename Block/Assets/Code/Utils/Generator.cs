using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{

    public bool generatorStatus;


    // Start is called before the first frame update
    public void Start()
    {

    }

    // Update is called once per frame
    public void Update()
    {

    }




    public bool GetGenStatus() 
    {
        return generatorStatus;
    }

    public bool ToggleGenStatus() 
    {
        generatorStatus = !generatorStatus;
        return generatorStatus;
    }


}
