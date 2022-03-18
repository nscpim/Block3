using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Actor
{

    public void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    public void Start()
    {
      

    }

    // Update is called once per frame
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            GameManager.GetManager<UIManager>().ShowTempUI(5, "Test", 100, 100);
        }
    }
}
