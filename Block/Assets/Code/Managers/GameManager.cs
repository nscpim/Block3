using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //managers array
    private static Manager[] managers;

    //Player Instance
    public Player player;
    
    //Gamemanager instance
    public static GameManager instance { get; private set; }

    //Check if ingame
    private static bool inGame;
    //check if the game is paused
    private static bool pause;
    //bool for loading once
    private bool loadLevelOnce;

    public static T GetManager<T>() where T : Manager
    {
        for (int i = 0; i < managers.Length; i++)
        {
            if (typeof(T) == managers[i].GetType())
            {
                return (T)managers[i];
            }
        }
        return default(T);
    }

    GameManager()
    {
        instance = this;
        managers = new Manager[]
        {
            new AudioManager(),
        };
        loadLevelOnce = false;
        
    }
    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   
}
