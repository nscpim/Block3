using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Actor
{
    public int needsBar { get; private set; }
    private RaycastHit hit;

    
    public Animator[] anim;
    public float gravity = 40.0f;

    private Vector3 moveDirection = Vector3.zero;
    private float turner;
    private float looker;
    public float sensitivity;
    private float speed = 6.0F;
    private Camera cam;
    private InventoryManager inventory = GameManager.GetManager<InventoryManager>();

    public void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    public void Start()
    {
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


                gameObject.transform.DetachChildren();
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

            if (!inventory.HasItem())
            {
                return;
            }
            else
            {
                
                Place();
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            inventory.selectedSlot = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            inventory.selectedSlot = 2;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            inventory.selectedSlot = 3;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            inventory.selectedSlot = 4;
        }

        Scroll();
        print((int)inventory.selectedSlot);

    }

    public void Place()
    {

        var _gameObject = inventory.items[(int)inventory.selectedSlot];

        if (_gameObject == null)
        {
            return;
        }
        else
        {
            Instantiate(_gameObject, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z), Quaternion.identity);
        }
    }

       


    public void Interaction()
    {

        if (Physics.Raycast(transform.position, transform.TransformDirection(transform.forward), out hit, 200))
        {
            Debug.DrawRay(gameObject.transform.position, transform.TransformDirection(transform.forward), Color.black, 5f);
            if (hit.transform.tag == "Interactable")
            {
                hit.transform.gameObject.GetComponent<Interactable>().Interact(true);
                hit.transform.gameObject.SetActive(false);
            }
            if (hit.transform.tag == "Fridge")
            {
                hit.transform.gameObject.GetComponent<Fridge>().PlayAnimation();
                
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
        if (inventory.selectedSlot > 0 && inventory.selectedSlot < 5)
        {
            inventory.selectedSlot += Input.mouseScrollDelta.y;
        }
        if (inventory.selectedSlot <= 1)
        {
            inventory.selectedSlot = 1;
        }
        if (inventory.selectedSlot >= 4)
        {
            inventory.selectedSlot = 4;
        }
       
        
        
        
        
    }

}

