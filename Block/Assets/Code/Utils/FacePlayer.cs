using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacePlayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var target = Player.instance;
        if (target != null)
        {
            transform.LookAt(target.transform.localPosition);
        }
    }
}
