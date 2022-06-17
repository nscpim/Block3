using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorToggle : Tutorial
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
            if (!Generator.firstTime)
            {
                GameManager.GetManager<TutorialManager>().CompletedTutorial();
                isCurrentTutorial = false;
            }
        }
    }
}
