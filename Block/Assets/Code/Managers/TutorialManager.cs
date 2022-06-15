using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : Manager
{
    public List<Tutorial> tutorials = new List<Tutorial>();
    private Tutorial currentTutorial;


    // Start is called before the first frame update
    public override void Start()
    {
        SetNextTutorial(0);
    }

    // Update is called once per frame
    public override void Update()
    {
        if (currentTutorial)
        {
            currentTutorial.CheckIfHappening();
        }
    }

    public void CompletedTutorial()
    {
        SetNextTutorial(currentTutorial.order + 1);
    }


    public void SetNextTutorial(int currentOrder)
    {
        currentTutorial = getTutorialByOrder(currentOrder);
        if (!currentTutorial)
        {
            CompletedAllTutorials();
            return;
        }


        GameManager.instance.tutText.text = currentTutorial.explanation;
    }

    public void CompletedAllTutorials()
    {
        GameManager.instance.tutText.text = "Try to conserve your energy";
    }

    public Tutorial getTutorialByOrder(int _order)
    {
        for (int i = 0; i < tutorials.Count; i++)
        {
            if (tutorials[i].order == _order)
            {
                return tutorials[i];
            }
        }
        return null;
    }
}
