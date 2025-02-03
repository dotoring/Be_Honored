using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    
    public GameObject mainCamera;
    public Transform point1;
    public Transform point2;

    private Vector3 xyz;
    private float far = 0.5f;
    private float lerp = 0.5f;
    private Quaternion rotate;
    private bool isRotating = false;
    private float a;
    private float b;



    void Start()
    {
        rotate = gameObject.transform.localRotation;
        xyz = new Vector3(0f, 0f, 0f);
    }

    void Update()
    {
        cameraZoom();
        cameraRotation();
    }






    void cameraZoom()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (far < 1f)
            {
                far += 0.1f;
            }
            if (far > 1f)
            {
                far = 1f;
            }
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (far > 0f)
            {
                far -= 0.1f;
            }
            if (far < 0f)
            {
                far = 0f;
            }
        }

        lerp = Mathf.Lerp(lerp, far, Time.deltaTime * 10f);
        mainCamera.transform.position = Vector3.Lerp(point2.position, point1.position, lerp);
    }

    void cameraRotation()
    {
        if (Input.GetMouseButton(0))
        {
            isRotating = true;
        }
        
        else
        {
            isRotating = false;
        }

        if (isRotating == true)
        {
            rotate = gameObject.transform.localRotation;
            a = rotate.eulerAngles.x + Input.GetAxis("Mouse Y") * 2f;

            if (a > 40f && a < 180f)
            {
                a = 40f;
            }
            if (a < 360f && a > 180f)
            {
                a = 360f;
            }
            b = rotate.eulerAngles.y + Input.GetAxis("Mouse X") * 2f;
            xyz.Set(a, b, 0f);
            rotate.eulerAngles = xyz;
            gameObject.transform.localRotation = rotate;
        }
    }







}//Class Code end























