using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneTutorial : Tutorial
{
    private bool isCurrentTutorial = false;
    public override void CheckIfHappening()
    {
        isCurrentTutorial = true;
    }

    public void Update()
    {
        if (isCurrentTutorial) 
        {
            if (Player.instance.hasObject)
            {
                GameManager.GetManager<TutorialManager>().CompletedTutorial();
                isCurrentTutorial = false;
            }
        }
    }

}
