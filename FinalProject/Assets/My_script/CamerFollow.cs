using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerFollow : MonoBehaviour
{
    
    public Vector3 offset;
    public Transform target;
    private float speed = 1.5f;
   

    void FixedUpdate()
    {
        RotateCamera();
        
    }


    void RotateCamera()
    {
        transform.position = target.position + offset;
        if (Input.GetMouseButton(0))
        {
            
            transform.RotateAround(target.position, Vector3.right, Input.GetAxis("Mouse Y") * speed);
           
        }
    }

   
}
