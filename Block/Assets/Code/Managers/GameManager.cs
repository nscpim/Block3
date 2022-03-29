using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //UI variables
    [Header("UI")]
    public Canvas UIcanvas;
    public Font font;
    public Text eventText;

    [Header("Managers")]
    //managers array
    private static Manager[] managers;

    [Header("Player")]
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

    private int time = (int)GetManager<EnergyManager>().eventTimer.TimeLeft();


    //Main Camera
    public Camera mainCamera;

    GameManager()
    {
        
    
        
    }

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

    public void Awake()
    {
        instance = this;
        managers = new Manager[]
        {
            new AudioManager(),
            new EnergyManager(),
            new UIManager(),
            new InventoryManager(),
        };
        loadLevelOnce = false;
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    public void Start()
    {
        for (int i = 0; i < managers.Length; i++)
        {
            managers[i].Start();
        }
    }

    // Update is called once per frame
   public void Update()
    {

       print(GetManager<InventoryManager>().items.ToString());

      
        eventText.text = time.ToString();
       
        for (int i = 0; i < managers.Length; i++)
        {
            managers[i].Update();
        }
    }

    public static void Pause(bool value)
    {
        pause = value;
        for (int i = 0; i < managers.Length; i++)
        {
            managers[i].Pause(value);
        }
    }
   
}
public enum Levels
{


}
