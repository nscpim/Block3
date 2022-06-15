using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "InteractionUI")]

public class InteractionUI : ScriptableObject {
    [TextArea(4, 10)]
    public string text;
    public bool firstTime;
   
}
