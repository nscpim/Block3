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
    private float speed = 3.0F;
    [HideInInspector]
    public Camera cam;
    public Transform handLocation;
    public bool hasObject = false;
    private float throwSpeed;
    [SerializeField] private GameObject pauseMenuUI;

    

    [Header("Materials")]
    Material ogMat;
    public Material highlightmat;
    GameObject lasthighlightedObject;


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

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            if (GameManager.instance.phoneanim.GetBool("Phone"))
            {
                StartCoroutine(LightsOut());
                GameManager.instance.phoneanim.SetBool("Phone", false);
            }
            else
            {

                GameManager.instance.phoneLight.enabled = true;
                GameManager.instance.phoneanim.SetBool("Phone", true);
            }
        }

        if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Mouse0))
        {
            Interaction();
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
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            GameManager.GetManager<InventoryManager>().selectedSlot = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            GameManager.GetManager<InventoryManager>().selectedSlot = 2;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            GameManager.GetManager<InventoryManager>().selectedSlot = 3;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            GameManager.GetManager<InventoryManager>().selectedSlot = 4;
        }
        Scroll();
        HighLightObjectRay();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameManager.PauseGame(true);
        }

        if (GameManager.pause)
        {
            ActivateMenu();
        }
        else
        {
            DeactivateMenu();
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
        if (handLocation.childCount > 0 && hasObject)
        {
            var childObject = handLocation.GetChild(0).gameObject.transform;
            if (childObject != null)
            {
                childObject.GetComponent<Rigidbody>().isKinematic = false;
                childObject.GetComponent<Rigidbody>().AddForce(cam.transform.forward * throwSpeed);
                childObject.SetParent(null);
                hasObject = false;
            }
        }
        if (Physics.Raycast(cam.transform.position, cam.transform.TransformDirection(transform.forward), out hit, 5))
        {
            switch (hit.transform.tag)
            {
                case "Interactable":
                    if (Generator.CanDrain())
                    {
                    }
                    break;
                case "Fridge":
                    if (Generator.CanDrain())
                    {
                        InteractionUI ui = hit.transform.gameObject.GetComponent<Interactable>().interaction_UI;
                        hit.transform.gameObject.GetComponent<Fridge>().PlayAnimation();
                        hit.transform.gameObject.GetComponent<Interactable>().Interact(false, true, null);
                        ui.firstTime = true;
                    }
                    break;
                case "Lights":
                    if (Generator.CanDrain())
                    {
                        hit.transform.gameObject.GetComponent<Lights>().ToggleLights();
                        hit.transform.gameObject.GetComponent<Interactable>().Interact(false, true, null);
                    }
                    break;
                case "Generator":
                    print("Hits generator");
                    hit.transform.gameObject.GetComponent<Generator>().ToggleDrain();
                    if (!Generator.CanDrain())
                    {
                        foreach (Light i in GameManager.instance.lights)
                        {
                            i.transform.gameObject.SetActive(false);
                        }
                    }
                    break;
                case "Pickup":
                    hit.transform.gameObject.GetComponent<Interactable>().Interact(true, false, hit.transform.gameObject);
                    break;
                case "Door":
                    //it checks if the object is a door and play the animation from the animator
                    hit.transform.gameObject.GetComponent<Doors>().PlayAnimation();
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

        if (Physics.Raycast(cam.transform.position, cam.transform.TransformDirection(transform.forward), out hit, 10))
        {
            var tag = hit.transform.gameObject.tag;
            //Layer 8 == Outlined
            if (tag == "Outlining" || tag == "Fridge" || tag == "Lights" || tag == "Generator" || tag == "Screen")
            {
                HighLightObject(hit.transform.gameObject);
            }
            else
            {
                ClearHighLight();
                hit.transform.gameObject.GetComponent<Interactable>().interactableText.transform.gameObject.SetActive(false);
            }
          
            
            //this switch case allows text to pop up the first time the player looks at an object until the first interaction
           
                switch (tag)
                {
                    case "Fridge":
                        InteractionUI ui = hit.transform.gameObject.GetComponent<Interactable>().interaction_UI;
                    if (!ui.firstTime)
                    {
                        hit.transform.gameObject.GetComponent<Interactable>().interactableText.transform.gameObject.SetActive(true);
                        hit.transform.gameObject.GetComponent<Interactable>().interactableText.text = ui.text;
                    }
                        break;
                default:
                   
                    break;
                }
        }
      
    }
    public void HighLightObject(GameObject highlightedObject)
    {
        if (lasthighlightedObject != highlightedObject)
        {
            ClearHighLight();
            ogMat = highlightedObject.GetComponent<MeshRenderer>().sharedMaterial;
            highlightedObject.GetComponent<MeshRenderer>().sharedMaterial = highlightmat;
            if (highlightedObject.GetComponent<Interactable>() == null || highlightedObject.GetComponent<Interactable>().type == highLight.Small)
            {
                highlightedObject.GetComponent<MeshRenderer>().sharedMaterial.SetFloat("_Thickness", 0.05f);
            }
            else if (highlightedObject.GetComponent<Interactable>().type == highLight.Medium)
            {
                highlightedObject.GetComponent<MeshRenderer>().sharedMaterial.SetFloat("_Thickness", 0.1f);
            }
            else if (highlightedObject.GetComponent<Interactable>().type == highLight.Large)
            {
                highlightedObject.GetComponent<MeshRenderer>().sharedMaterial.SetFloat("_Thickness", 0.001f);
            }
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
    void ActivateMenu()
    {
        pauseMenuUI.SetActive(true);
    }

    public void DeactivateMenu()
    {
        GameManager.PauseGame(false);
        pauseMenuUI.SetActive(false);
    }

}
public enum highLight
{
    Small,
    Medium,
    Large,
}


