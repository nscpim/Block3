using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public int order;
    [TextArea(3, 10)]
    public string explanation;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.GetManager<TutorialManager>().tutorials.Add(this);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public virtual void CheckIfHappening() { }
}
