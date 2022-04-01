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
    private float speed = 6.0F;
    [HideInInspector]
    public Camera cam;
    

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

        if (Input.GetKeyDown(KeyCode.E))
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

        if (Physics.Raycast(cam.transform.position, cam.transform.TransformDirection(transform.forward), out hit, Mathf.Infinity))
        {
            Debug.DrawRay(cam.transform.position, cam.transform.TransformDirection(transform.forward), Color.black, 200f);
            if (hit.transform.tag == "Interactable")
            {
                hit.transform.gameObject.GetComponent<Interactable>().Interact(true, false);
                hit.transform.gameObject.SetActive(false);
            }
            if (hit.transform.tag == "Fridge")
            {
                print("hit fridge");
                hit.transform.gameObject.GetComponent<Fridge>().PlayAnimation();
                hit.transform.gameObject.GetComponent<Interactable>().Interact(false, true);
            }
            if (hit.transform.tag == "Lights")
            {
                if (hit.transform.gameObject.GetComponent<Lights>().GetState())
                {
                    hit.transform.gameObject.GetComponent<Lights>().ToggleLights(false);
                }
                else
                {
                    hit.transform.gameObject.GetComponent<Lights>().ToggleLights(true);
                }
              
            }
            
        }
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

}

