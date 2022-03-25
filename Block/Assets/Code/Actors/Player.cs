using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Actor
{
    public int needsBar { get; private set; }
    private RaycastHit hit;
   
   
   
    public float gravity = 40.0f;
   
    private Vector3 moveDirection = Vector3.zero;
    private float turner;
    private float looker;
    public float sensitivity;
    private float speed = 6.0F;
    private Camera cam;


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

        if (Input.GetKeyDown(KeyCode.F))
        {
            Interaction();
        }
    }

    public void Interaction() 
    {

        if (Physics.Raycast(transform.position, transform.TransformDirection(transform.forward), out hit, 200))
        {
            Debug.DrawRay(gameObject.transform.position, transform.TransformDirection(transform.forward), Color.black, 5f, true);
            if (hit.transform.tag == "Interactable")
            {
                hit.transform.gameObject.GetComponent<Interactable>().Interact();
                hit.transform.gameObject.SetActive(false);
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
   
   
}

