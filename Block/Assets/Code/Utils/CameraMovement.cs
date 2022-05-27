using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    private enum RotationAxes { mouseXandY = 0, mouseX = 1, mouseY = 2 }
    private RotationAxes axes = RotationAxes.mouseXandY;
    private float sensitivityX = 5f;
    private float sensitivityY = 5f;
    private float rotationY = 0f;
    private float minimumY = -60F;
    private float maximumY = 60F;
    // Start is called before the first frame update
   public void Start()
    {
        
    }

    // Update is called once per frame
    public void Update()
    
    {
        if (!GameManager.pause) 
        {
            CameraMove();
        }
        
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
