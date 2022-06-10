using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    //UI variables
    [Header("UI")]
    public Canvas UIcanvas;
    public Font font;
    public Text eventText;
    public Slider energyBarSlider;
    public static bool pause;
    public Slider MusicvolumeSlider;
    public Slider SFXvolumeSlider;
    public Toggle Muteallvolume;
    public Text musictext;
    public Text SFXtext;
    private bool doonce = false;
    public GameObject Startscreen;
    [Header("Managers")]
    //managers array
    private static Manager[] managers;

    [Header("Player")]
    //Player Instance
    public Player player;
    public GameObject phone;
    public Animator phoneanim;
    public Material red;
    public Material green;
    public Material orange;
    public Light phoneLight;

    [Header("Scene")]
    public Light[] lights;


    //Gamemanager instance
    public static GameManager instance { get; private set; }

 

    //Check if ingame
    private static bool inGame;
    //check if the game is paused
    private static bool Pause;
    //bool for loading once
    private bool loadLevelOnce;

    private int time;


    //Main Camera
    public Camera mainCamera;

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
            new InventoryManager(),
            new EnergyManager(),
            new AudioManager(),
            new UIManager(),
        };
        loadLevelOnce = false;
        DontDestroyOnLoad(gameObject);

        for (int i = 0; i < managers.Length; i++)
        {
            managers[i].Awake();
        }
    }

    // Start is called before the first frame update
    public void Start()
    {

        for (int i = 0; i < managers.Length; i++)
        {
            managers[i].Start();
        }
      

    }

    public static void LoadLevel(Levels level) 
    {
        SceneManager.LoadScene((int)level);
    }

    public IEnumerator StartGame() 
    {
        yield return new WaitForSeconds(5f);



    }


    // Update is called once per frame
   public void Update()
    {

       
        for (int i = 0; i < managers.Length; i++)
        {
            managers[i].Update();
        }
        if (!doonce)
        {
            GetManager<AudioManager>().PlayMusic("testmusic", 1);
            doonce = true;
        }
        
    }

    public static void PauseGame(bool value)
    {
        pause = value;
        for (int i = 0; i < managers.Length; i++)
        {
            managers[i].Pause(value);
        }
    }
    public void Flick() 
    {
        StartCoroutine(LightsFlicking());
    }
    public IEnumerator LightsFlicking() 
    {
        foreach (Light i in lights)
        {
            i.intensity = 0.13f;
        }
        yield return new WaitForSeconds(0.2f);
        
        foreach (Light i in lights)
        {
            i.intensity = 3f;
        }

    }

}
public enum Levels
{
    MainMenu, 
    Game,
    EndScreen


}
