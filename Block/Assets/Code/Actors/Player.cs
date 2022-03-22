using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Actor
{
    public int needsBar { get; private set; }
    private RaycastHit hit;
    private enum RotationAxes { mouseXandY = 0, mouseX = 1, mouseY = 2 }
    private RotationAxes axes = RotationAxes.mouseXandY;
    private float sensitivityX = 5f;
    private float sensitivityY = 5f;
    private float minimumY = -60F;
    private float maximumY = 60F;
    public float gravity = 40.0f;
    private float rotationY = 0f;
    private Vector3 moveDirection = Vector3.zero;
    private float turner;
    private float looker;
    public float sensitivity;
    private float speed = 6.0F;


    public void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    public void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

    }

    // Update is called once per frame
    public void Update()
    {

        movement();
        CameraMove();
        var energyManager = GameManager.GetManager<EnergyManager>();
      //  if (Input.GetKeyDown(KeyCode.A))
      //  {
      //      GameManager.GetManager<UIManager>().ShowTempUI(5, "Test", 400, 400);
     //   }
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

        if (Input.GetKey(KeyCode.F))
        {
            Interaction();
        }
    }

    public void Interaction() 
    {

        if (Physics.Raycast(transform.position, transform.TransformDirection(transform.forward), out hit, 5))
        {
            if(hit.transform.tag == "Interactable")
            {
                hit.transform.gameObject.GetComponent<Interactable>().Interact();
            }
        }
    }
    public void movement() 
    {
        CharacterController controller = GetComponent<CharacterController>();

        if (controller.isGrounded)
        {

            moveDirection = new Vector3(Input.GetAxis("Horizontal") * 0.4f, 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);

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
    private void CameraMove()
    {
        if (axes == RotationAxes.mouseXandY)
        {
            float rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivityX;

            rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
            rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

            transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);


        }
        else if (axes == RotationAxes.mouseX)
        {
            transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityX, 0);
        }
        else
        {
            rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
            rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

            transform.localEulerAngles = new Vector3(-rotationY, transform.localEulerAngles.y, 0);
        }
    }

   
}

