using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorTutorial : Tutorial
{

    private bool isCurrentTutorial = false;
    public Transform hitTransform;

    public override void CheckIfHappening()
    {
        isCurrentTutorial = true;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (!isCurrentTutorial)
        {
            return;
        }
        print(other.transform.tag);
        if (other.transform == hitTransform)
        {
            GameManager.GetManager<TutorialManager>().CompletedTutorial();
            isCurrentTutorial = false;
        }
    }


}
