using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fans : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Generator.generatorDraining)
        {
            gameObject.transform.RotateAround(transform.position, -transform.right, Time.deltaTime * 500f);
        }
    
           
    }
}
