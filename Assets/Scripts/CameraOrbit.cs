using UnityEngine;


using System;
using System.Collections;

public class CameraOrbit : MonoBehaviour
{

    public Transform lookingAt;
    public Transform camTransform;

    private float scrollSensetivity = 30.0f;
    private float mouseSensetivity = 2.0f;
    private float currentX;
    private float currentY;
    public int minY = -25;
    public int maxY = 89;
    public float distance;
    public float maxDistance = 150.0f;
    public float minDistance = 30.0f;

    public bool CameraActive = false;


    // Use this for initialization
    void Start()
    {
        camTransform = transform;
        currentX = TheGame.cX;
        currentY = TheGame.cY;
        distance = TheGame.cD;
    }

    void Update()
    {
        CameraActive = Input.GetMouseButton(1);
        if (CameraActive)
        {
            currentX += Input.GetAxis("Mouse X")* mouseSensetivity;
            currentY += Input.GetAxis("Mouse Y")* mouseSensetivity;
            if(currentY >= maxY)
            {
                currentY = maxY;
            }
            if (currentY <= minY)
            {
                currentY = minY;
            }
        }
        distance -= Input.GetAxis("Mouse ScrollWheel")*scrollSensetivity;
        if (distance < minDistance)
        {
            distance = minDistance;
        }
        if(distance > maxDistance)
        {
            distance = maxDistance;
        }
        TheGame.cX = currentX;
        TheGame.cY = currentY;
        TheGame.cD = distance;
    }

    void LateUpdate()
    {

        Vector3 direction = new Vector3(0, 0, -distance);
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        camTransform.position = rotation*direction;
        camTransform.LookAt(lookingAt.position);

    }
}