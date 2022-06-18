using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Actor
{
    public static Player instance { get; private set; }

    private RaycastHit hit;
    public float gravity = 40.0f;
    private Vector3 moveDirection = Vector3.zero;
    private float turner;
    private float looker;
    public float sensitivity;
    private float speed = 1.0F;
    private float needsModifier = 0.5f;
    [HideInInspector]
    public Camera cam;
    public Transform leftHandLocation;
    public Transform rightHandLocation;
   [HideInInspector] public bool hasObject = false;
    private bool sprintneed = false;
    private Vector3 offset;
   [HideInInspector] public bool mayDrain = false;



    [Header("Materials")]
    Material ogMat;
    public Material highlightmat;
    GameObject lasthighlightedObject;
    GameObject lastrayObject;


    public void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    public void Start()
    {
        instance = this;
        Cursor.lockState = CursorLockMode.Locked;
        cam = gameObject.GetComponentInChildren<Camera>();


    }

    // Update is called once per frame
    public void Update()
    {

        movement();

        var energyManager = GameManager.GetManager<EnergyManager>();
        if (Input.GetKeyDown(KeyCode.U))
        {
            GameManager.GetManager<UIManager>().ShowTempUI(5, "Test", 400, 400);
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (energyManager.eventTimer.isActive)
            {
                energyManager.eventTimer.StopTimer();
                energyManager.eventTimer.SetTimer(Random.Range(energyManager.minimumTime, energyManager.maximumTime));
            }
            else
            {
                energyManager.eventTimer.SetTimer(Random.Range(energyManager.minimumTime, energyManager.maximumTime));
            }
        }

        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Mouse0))
        {
            Interaction();
        }
        print(sprintneed + " Sprint");
        if (Input.GetKey(KeyCode.LeftShift))
        {
            sprintneed = true;
            speed = 2f;
        }
        else
        {
            speed = 1.0f;
            sprintneed = false;
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (!GameManager.GetManager<InventoryManager>().HasItem())
            {
                print("false");
                return;
            }
            else
            {
                print("true");
                Place();
            }
        }
        if (sprintneed && mayDrain)
        {
            GameManager.GetManager<EnergyManager>().RemoveNeeds((1 * Time.deltaTime) * needsModifier);
        }
        Scroll();
        HighLightObjectRay();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameManager.pause)
            {
                GameManager.instance.DeactivateMenu();
                GameManager.PauseGame(false);
            }
            else
            {
                GameManager.PauseGame(true);
                GameManager.instance.ActivateMenu();
            }

        }
    }
    public IEnumerator LightsOut()
    {
        yield return new WaitForSeconds(1f);
        GameManager.instance.phoneLight.enabled = false;

    }
    public void Place()
    {

        var _gameObject = GameManager.GetManager<InventoryManager>().items[(int)GameManager.GetManager<InventoryManager>().selectedSlot];

        if (_gameObject == null)
        {
            return;
        }
        else
        {
            _gameObject.SetActive(true);
            GameManager.GetManager<InventoryManager>().RemoveItem(_gameObject);
        }
    }

    public void Interaction()
    {
        /* if (leftHandLocation.childCount > 0 && hasObject)
         {
             var childObject = leftHandLocation.GetChild(0).gameObject.transform;
             if (childObject != null)
             {
                 childObject.GetComponent<Rigidbody>().isKinematic = false;
                 childObject.GetComponent<Rigidbody>().AddForce(cam.transform.forward * throwSpeed);
                 childObject.SetParent(null);
                 hasObject = false;
             }
         }*/
        if (Physics.Raycast(cam.transform.position, cam.transform.TransformDirection(transform.forward), out hit, 2))
        {

            switch (hit.transform.tag)
            {
                case "Interactable":
                    if (Generator.CanDrain())
                    {

                    }
                    break;
                case "Sink":
                    if (Generator.CanDrain())
                    {
                        hit.transform.gameObject.GetComponent<Interactable>().Interact(false, false, null);
                        GameManager.GetManager<EnergyManager>().SubstractEnergy(2f);
                    }
                    break;
                case "Fridge":
                    if (Generator.CanDrain())
                    {

                        hit.transform.gameObject.GetComponent<Fridge>().PlayAnimation();
                        hit.transform.gameObject.GetComponent<Interactable>().Interact(false, true, null);
                        hit.transform.gameObject.GetComponent<Interactable>().interaction_UI.firstTime = true;
                    }
                    break;
                case "Lights":
                    if (Generator.CanDrain())
                    {
                        hit.transform.gameObject.GetComponent<Lights>().ToggleLights();
                        hit.transform.gameObject.GetComponent<Interactable>().Interact(false, true, null);
                        if (hit.transform.gameObject.GetComponent<Interactable>().interaction_UI != null)
                        {
                            hit.transform.gameObject.GetComponent<Interactable>().interaction_UI.firstTime = true;
                        }
                    }
                    break;
                case "Generator":
                    print("Hits generator");
                    hit.transform.gameObject.GetComponent<Generator>().ToggleDrain();
                    if (!Generator.CanDrain())
                    {
                        foreach (Light i in GameManager.instance.lights)
                        {
                            if (i.isActiveAndEnabled)
                            {
                                i.transform.gameObject.SetActive(Generator.CanDrain());
                                GameManager.GetManager<EnergyManager>().RemoveDrainage(0.1f);
                            }

                        }
                        ComputerScreen.Instance.SetToggleScreen(false);
                    }
                    if (hit.transform.gameObject.GetComponent<Interactable>().interaction_UI != null)
                    {
                        hit.transform.gameObject.GetComponent<Interactable>().interaction_UI.firstTime = true;
                    }
                    break;
                case "Pickup":
                    hit.transform.gameObject.GetComponent<Interactable>().Interact(true, false, hit.transform.gameObject);

                    break;
                case "Door":
                    //it checks if the object is a door and play the animation from the animator
                    hit.transform.gameObject.GetComponent<Doors>().PlayAnimation();
                    if (hit.transform.gameObject.GetComponent<Interactable>().interaction_UI != null)
                    {

                        hit.transform.gameObject.GetComponent<Interactable>().interaction_UI.firstTime = true;
                    }
                    break;
                case "Screen":
                    if (Generator.CanDrain())
                    {
                        ComputerScreen.Instance.ToggleScreen();
                        hit.transform.gameObject.GetComponent<Interactable>().Interact(false, true, null);
                        if (hit.transform.gameObject.GetComponent<Interactable>().interaction_UI != null)
                        {
                            hit.transform.gameObject.GetComponent<Interactable>().interaction_UI.firstTime = true;
                        }
                    }
                    break;
                case "GenScreen":
                    if (Generator.CanDrain())
                    {
                        GeneratorScreen.Instance.ToggleScreen();
                        hit.transform.gameObject.GetComponent<Interactable>().Interact(false, true, null);
                        if (hit.transform.gameObject.GetComponent<Interactable>().interaction_UI != null)
                        {
                            hit.transform.gameObject.GetComponent<Interactable>().interaction_UI.firstTime = true;
                        }
                    }
                    break;
                case "Collect":
                    hit.transform.gameObject.GetComponent<PowerCollector>().CollectPower();
                    break;
                case "Stove":
                    if (Generator.CanDrain())
                    {
                        hit.transform.gameObject.GetComponent<Interactable>().Interact(false, false, null);
                        GameManager.GetManager<EnergyManager>().SubstractEnergy(2f);
                    }
                    break;
                default:
                    break;
            }

        }
        Debug.DrawRay(cam.transform.position, cam.transform.TransformDirection(transform.forward), Color.white, 200f);
    }
    public void movement()
    {
        CharacterController controller = GetComponent<CharacterController>();

        if (controller.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = cam.transform.TransformDirection(moveDirection);
            moveDirection.y = 0.0f;
            moveDirection *= speed;
        }

        turner = Input.GetAxis("Mouse X") * sensitivity;
        looker = -Input.GetAxis("Mouse Y") * sensitivity;
        if (turner != 0)
        {
            transform.localEulerAngles += new Vector3(0, turner, 0);
        }
        if (looker != 0)
        {
            transform.localEulerAngles += new Vector3(looker, 0, 0);
        }

        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
    }
    public void Scroll()
    {
        if (GameManager.GetManager<InventoryManager>().selectedSlot > 0 && GameManager.GetManager<InventoryManager>().selectedSlot < 5)
        {
            GameManager.GetManager<InventoryManager>().selectedSlot += Input.mouseScrollDelta.y;
        }
        if (GameManager.GetManager<InventoryManager>().selectedSlot <= 1)
        {
            GameManager.GetManager<InventoryManager>().selectedSlot = 1;
        }
        if (GameManager.GetManager<InventoryManager>().selectedSlot >= 4)
        {
            GameManager.GetManager<InventoryManager>().selectedSlot = 4;
        }
    }


    public void HighLightObjectRay()
    {
        int layerMask = 1 << 0 | 1 << 8;

        if (Physics.Raycast(cam.transform.position, cam.transform.TransformDirection(transform.forward), out hit, 2, layerMask))
        {
            var tag = hit.transform.gameObject.tag;
            Debug.LogWarning(hit.transform.gameObject + " " + tag);

            if (tag == "Interactable" || tag == "Fridge" || tag == "Lights" || tag == "Generator" || tag == "Screen" || tag == "Pickup" || tag == "Door" || tag == "Stove" || tag == "Sink")
            {
                HighLightObject(hit.transform.gameObject);
                Debug.LogWarning("gets here");
                switch (tag)
                {
                    case "Fridge":

                        if (!hit.transform.gameObject.GetComponent<Interactable>().interaction_UI.firstTime)
                        {
                            lastrayObject = hit.transform.gameObject;
                            hit.transform.gameObject.GetComponent<Interactable>().interactableText.transform.gameObject.SetActive(true);
                            hit.transform.gameObject.GetComponent<Interactable>().interactableText.text = string.Format(hit.transform.gameObject.GetComponent<Interactable>().interaction_UI.text);
                        }
                        break;
                    case "Door":
                        if (hit.transform.gameObject.GetComponent<Interactable>().interaction_UI != null)
                        {
                            if (!hit.transform.gameObject.GetComponent<Interactable>().interaction_UI.firstTime)
                            {
                                lastrayObject = hit.transform.gameObject;
                                hit.transform.gameObject.GetComponent<Interactable>().interactableText.transform.gameObject.SetActive(true);
                                hit.transform.gameObject.GetComponent<Interactable>().interactableText.text = hit.transform.gameObject.GetComponent<Interactable>().interaction_UI.text;
                            }
                        }
                        break;
                    case "Lights":
                        if (hit.transform.gameObject.GetComponent<Interactable>().interaction_UI != null)
                        {
                            if (!hit.transform.gameObject.GetComponent<Interactable>().interaction_UI.firstTime)
                            {
                                lastrayObject = hit.transform.gameObject;
                                hit.transform.gameObject.GetComponent<Interactable>().interactableText.transform.gameObject.SetActive(true);
                                hit.transform.gameObject.GetComponent<Interactable>().interactableText.text = hit.transform.gameObject.GetComponent<Interactable>().interaction_UI.text;
                            }
                        }
                        break;
                    case "Pickup":
                        if (hit.transform.gameObject.GetComponent<Interactable>().interaction_UI != null)
                        {
                            if (!hit.transform.gameObject.GetComponent<Interactable>().interaction_UI.firstTime)
                            {
                                lastrayObject = hit.transform.gameObject;
                                hit.transform.gameObject.GetComponent<Interactable>().interactableText.transform.gameObject.SetActive(true);
                                hit.transform.gameObject.GetComponent<Interactable>().interactableText.text = hit.transform.gameObject.GetComponent<Interactable>().interaction_UI.text;
                            }
                        }
                        break;
                    case "Generator":
                        if (hit.transform.gameObject.GetComponent<Interactable>().interaction_UI != null)
                        {
                            if (!hit.transform.gameObject.GetComponent<Interactable>().interaction_UI.firstTime)
                            {
                                lastrayObject = hit.transform.gameObject;
                                hit.transform.gameObject.GetComponent<Interactable>().interactableText.transform.gameObject.SetActive(true);
                                hit.transform.gameObject.GetComponent<Interactable>().interactableText.text = string.Format(hit.transform.gameObject.GetComponent<Interactable>().interaction_UI.text);
                            }
                        }
                        break;
                    case "Screen":
                        if (hit.transform.gameObject.GetComponent<Interactable>().interaction_UI != null)
                        {
                            if (!hit.transform.gameObject.GetComponent<Interactable>().interaction_UI.firstTime)
                            {
                                lastrayObject = hit.transform.gameObject;
                                hit.transform.gameObject.GetComponent<Interactable>().interactableText.transform.gameObject.SetActive(true);
                                hit.transform.gameObject.GetComponent<Interactable>().interactableText.text = string.Format(hit.transform.gameObject.GetComponent<Interactable>().interaction_UI.text);
                            }
                        }
                        break;
                    case "GenScreen":
                        if (hit.transform.gameObject.GetComponent<Interactable>().interaction_UI != null)
                        {
                            if (!hit.transform.gameObject.GetComponent<Interactable>().interaction_UI.firstTime)
                            {
                                lastrayObject = hit.transform.gameObject;
                                hit.transform.gameObject.GetComponent<Interactable>().interactableText.transform.gameObject.SetActive(true);
                                hit.transform.gameObject.GetComponent<Interactable>().interactableText.text = string.Format(hit.transform.gameObject.GetComponent<Interactable>().interaction_UI.text);
                            }
                        }
                        break;
                    case "Sink":
                        if (!hit.transform.gameObject.GetComponent<Interactable>().interaction_UI.firstTime)
                        {
                            lastrayObject = hit.transform.gameObject;
                            hit.transform.gameObject.GetComponent<Interactable>().interactableText.transform.gameObject.SetActive(true);
                            hit.transform.gameObject.GetComponent<Interactable>().interactableText.text = string.Format(hit.transform.gameObject.GetComponent<Interactable>().interaction_UI.text);
                        }
                        break;
                    case "Stove":
                        if (!hit.transform.gameObject.GetComponent<Interactable>().interaction_UI.firstTime)
                        {
                            lastrayObject = hit.transform.gameObject;
                            hit.transform.gameObject.GetComponent<Interactable>().interactableText.transform.gameObject.SetActive(true);
                            hit.transform.gameObject.GetComponent<Interactable>().interactableText.text = string.Format(hit.transform.gameObject.GetComponent<Interactable>().interaction_UI.text);
                        }
                        break;
                    default:
                        break;
                }
            }
            else
            {
                ClearText();
                ClearHighLight();
            }
            //this switch case allows text to pop up the first time the player looks at an object until the first interaction
        }
    }

    public void ClearText()
    {
        if (lastrayObject != null)
        {
            lastrayObject.transform.gameObject.GetComponent<Interactable>().interactableText.transform.gameObject.SetActive(false);
            lastrayObject = null;
        }
    }
    public void HighLightObject(GameObject highlightedObject)
    {
        if (lasthighlightedObject != highlightedObject)
        {
            ClearHighLight();
            ogMat = highlightedObject.GetComponent<MeshRenderer>().sharedMaterial;
            highlightedObject.GetComponent<MeshRenderer>().sharedMaterial = highlightmat;
            highlightedObject.GetComponent<MeshRenderer>().sharedMaterial.SetFloat("_Thickness", highlightedObject.GetComponent<Interactable>().shaderThickness);
            highlightedObject.transform.gameObject.AddComponent<OutlineNormalsCalculator>();
            lasthighlightedObject = highlightedObject;
        }
    }
    public void ClearHighLight()
    {
        if (lasthighlightedObject != null)
        {

            lasthighlightedObject.GetComponent<MeshRenderer>().sharedMaterial = ogMat;
            Destroy(lasthighlightedObject.GetComponent<OutlineNormalsCalculator>());
            lasthighlightedObject = null;
        }
    }
}
public enum highLight
{
    Small,
    Medium,
    Large,
    Screen,
}


