using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTutorial : Tutorial
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
            if (GameManager.instance.openedDoor)
            {
                GameManager.GetManager<TutorialManager>().CompletedTutorial();
                isCurrentTutorial = false;
            }
        }
    }
}
