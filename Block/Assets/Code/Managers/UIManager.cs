using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Manager
{
    private Canvas canvas;
    private Text text;
    // Start is called before the first frame update
    public override void Start()
    {
        canvas = GameManager.instance.UIcanvas;
        Debug.Log(GameManager.instance.Startscreen.activeInHierarchy + " pipi small");
        Startscreen();  
    }

    

    // Update is called once per frame
    public override void Update()
    {
        if (GameManager.instance.Startscreen.activeInHierarchy && Input.GetKeyDown(KeyCode.Space))
            {
            GameManager.instance.Startscreen.GetComponent<Animator>().enabled = true;
        }
        if (GameManager.pause)
        {
            GameManager.GetManager<AudioManager>().SetVolume(GameManager.instance.MusicvolumeSlider.value, AudioType.Music);
            GameManager.GetManager<AudioManager>().SetVolume(GameManager.instance.SFXvolumeSlider.value, AudioType.sfx);
            GameManager.instance.musictext.text = "" + GameManager.instance.MusicvolumeSlider.value;
            GameManager.instance.SFXtext.text = "" + GameManager.instance.SFXvolumeSlider.value;
        }
    }

    public void ShowTempUI(int _delay, string _text, int posX, int posY)
    {
        GameObject textSourceObj = new GameObject();
        Text textSource = textSourceObj.AddComponent<Text>();
        textSource.text = _text;
        textSource.rectTransform.position = new Vector2(posX, posY);
        textSource.font = GameManager.instance.font;
        textSourceObj.transform.SetParent(canvas.transform);
        textSourceObj.name = "TempUIText";
        GameObject.Destroy(textSourceObj, _delay);
    }
    public void Startscreen()
    {
        GameManager.instance.Startscreen.GetComponent<Animator>().enabled = false;
    }
}

