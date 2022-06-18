using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

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
    public GameObject phoneEnergy;
    public Material red;
    public Material green;
    public Material orange;
    public Light phoneLight;

    [Header("Scene")]
    public Light[] lights;
    public GameObject[] lightObjects;
    public GameObject[] lightState;
    public Sprite[] onOff;

    [SerializeField] private GameObject pauseMenuUI;

    public Light[] doorLights; 
    public Timer gameTimer;

    [Header("Tutorial")]
    public Text tutText;

    //Gamemanager instance
    public static GameManager instance { get; private set; }

    private static float needs;
    private static float energy;

    public bool openedDoor = false;

    //Check if ingame
    private static bool inGame;
    //check if the game is paused
    private static bool Pause;
    //bool for loading once
    private bool loadLevelOnce;

    private int time;

    public Text energyLeft;
    public Text needsLeft;
    public Text gameState;
    public Text flavourText;
    public GameObject endGamePanel;


    //Main Camera
    public Camera mainCamera;

    private List<string> goodTexts = new List<string>();
    private List<string> badTexts = new List<string>();
    private List<string> needsTexts = new List<string>();
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
            new TutorialManager(),
        };

        goodTexts.Add("You did well, thanks to you the world did not end.");
        goodTexts.Add("You did good, The planet will be rebuild");
        badTexts.Add("You did poorly and the world burns.");
        badTexts.Add("That went bad, With this power conserving the world will be gone in no time.");
        needsTexts.Add("You die of hunger");
        loadLevelOnce = false;
        DontDestroyOnLoad(gameObject);

        for (int i = 0; i < managers.Length; i++)
        {
            managers[i].Awake();
        }
        gameTimer = new Timer();
    }

    // Start is called before the first frame update
    public void Start()
    {

        for (int i = 0; i < managers.Length; i++)
        {
            managers[i].Start();
        }
        
    }

    public float GetTime()
    {
        return gameTimer.TimeLeft();
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
        if (gameTimer.TimerDone() && gameTimer.isActive)
        {
            gameTimer.StopTimer();

            EndGame(GetManager<EnergyManager>().energyBar, GetManager<EnergyManager>().needsBar, GameState.Won, NeedsorPower.NONE);
        }
        if (SceneManager.sceneCount == (int)Levels.EndScreen)
        {
            gameTimer.StopTimer();
        }
        for (int i = 0; i < managers.Length; i++)
        {
            managers[i].Update();
        }
        if (!doonce)
        {
            //GetManager<AudioManager>().PlayMusic("testmusic", 1);
            doonce = true;
        }

    }


    public void ResetGame()
    {
        LoadLevel(Levels.MainMenu);
    }
    public void EndGame(float _energy, float _needs, GameState _state, NeedsorPower _needsorPower)
    {
        PauseGame(true);
        Cursor.lockState = CursorLockMode.None;
        GetManager<AudioManager>().StopPlaying();
        endGamePanel.SetActive(true);
        energyLeft.text = string.Format("You have {0}% power left", _energy);
        needsLeft.text = string.Format("You have {0}% needs left", (int)_needs);
        gameState.text = string.Format("You {0}", _state.ToString());
        if (_state == GameState.Lost && _needsorPower == NeedsorPower.NONE)
        {
            var randomText = Random.Range(0, badTexts.Count);
            flavourText.text = badTexts[randomText];
        }
        else if (_state == GameState.Won && _needsorPower == NeedsorPower.NONE)
        {
            var randomText = Random.Range(0, goodTexts.Count);
            flavourText.text = goodTexts[randomText];
        }
        else if (_state == GameState.Lost && _needsorPower == NeedsorPower.Needs)
        {
            var randomText = Random.Range(0, needsTexts.Count);
            flavourText.text = needsTexts[randomText];
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
    public void ActivateMenu()
    {
        pauseMenuUI.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
    }
    public void DeactivateMenu()
    {
        GameManager.PauseGame(false);
        pauseMenuUI.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
public enum Levels
{
    MainMenu,
    Game,
    EndScreen
}
public enum GameState
{
    Won,
    Lost
}
public enum NeedsorPower
{
    Needs,
    Power,
    NONE
}
