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
        
    }

    // Update is called once per frame
  public override void Update()
    {
        
    }


    public void ShowTempUI(int _delay, string _text, int posX, int posY)
    {
        GameObject sourceObj = new GameObject();
        GameObject textSourceObj = new GameObject();
        Canvas source = sourceObj.AddComponent<Canvas>();
        source.renderMode = RenderMode.ScreenSpaceOverlay;
        sourceObj.AddComponent<RectTransform>();
        Vector2 position = sourceObj.GetComponent<RectTransform>().anchoredPosition;
        position.x = posX;
        position.y = posY;
        Text textSource = textSourceObj.AddComponent<Text>();
        textSource.text = _text;
        textSourceObj.transform.parent = sourceObj.transform;
        source.name = "TempUI";
        GameObject.Destroy(sourceObj, _delay);
    }
}
