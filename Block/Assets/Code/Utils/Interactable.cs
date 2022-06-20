using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Interactable : MonoBehaviour
{
    [Tooltip("Only use values that are needed for the object you are using it for,leave blank if you dont need it")]
    public float drainAmount;
    private string _name;
    public Animator anim;
    [HideInInspector] public bool canDrain = false;
    public float needsAmount;
    public float shaderThickness;
    public TextMeshProUGUI interactableText;
    public InteractionUI interaction_UI;
    public int cooldown;
    private Timer cooldownTimer;


    // Start is called before the first frame update
    public virtual void Start()
    {
        cooldownTimer = new Timer();
        _name = gameObject.name;
        if (interaction_UI != null)
        {
            interaction_UI.firstTime = false;
        }
    }


    // Update is called once per frame
    public virtual void Update()
    {
        if (cooldownTimer.TimerDone() && cooldownTimer.isActive)
        {
            cooldownTimer.StopTimer();
        }
    }
    public void Interact(bool canPickUp, bool drain, GameObject objectPickedUp)
    {

        if (canPickUp && objectPickedUp != null && !Player.instance.hasObject)
        {
            objectPickedUp = objectPickedUp.transform.parent.gameObject;
            if (objectPickedUp.GetComponent<Rigidbody>() == null)
            {
                objectPickedUp.AddComponent<Rigidbody>();
            }
            objectPickedUp.transform.GetComponentInChildren<Interactable>().interaction_UI.firstTime = true;
            Debug.LogWarning(objectPickedUp.transform.name + " Name of hit object");
            Destroy(objectPickedUp.transform.GetComponentInChildren<Interactable>());
            objectPickedUp.GetComponent<Rigidbody>().isKinematic = true;
            objectPickedUp.transform.position = GameManager.instance.player.leftHandLocation.transform.position;
            objectPickedUp.transform.rotation = GameManager.instance.player.leftHandLocation.transform.rotation;
            objectPickedUp.transform.SetParent(GameManager.instance.player.leftHandLocation.transform, true);
            objectPickedUp.gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
            Player.instance.hasObject = true;
        }
        bool generator = Generator.CanDrain();

        if (drain && !canDrain && generator)
        {
            GameManager.GetManager<EnergyManager>().AddDrainage(drainAmount);
            canDrain = true;
        }
        else if (drain && canDrain && generator)
        {
            GameManager.GetManager<EnergyManager>().RemoveDrainage(drainAmount);
            canDrain = false;
        }
        if (needsAmount != 0 && !cooldownTimer.isActive)
        {
            GameManager.GetManager<EnergyManager>().AddNeeds(needsAmount);
            cooldownTimer.SetTimer(cooldown);
        }

    }
    public Timer GetTimer() 
    {
        return cooldownTimer;
    }
}

