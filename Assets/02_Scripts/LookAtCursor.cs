﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LookAtCursor : MonoBehaviour
{
    int cursorDectect;
    Rigidbody RD;
    public Vector3 playertomouse;
    public Quaternion rotationangle;
    void Start()
    {
        cursorDectect = LayerMask.GetMask("CursorDectect");
        RD = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float cameraraylength = 100;
        Ray cameraray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit floorcross;
        if (Physics.Raycast(cameraray, out floorcross, cameraraylength, cursorDectect))
        {
            playertomouse = floorcross.point - transform.position;
            playertomouse.y = 0;
            rotationangle = Quaternion.LookRotation(playertomouse);
            RD.MoveRotation(rotationangle);
        }

        //會變成只轉z軸
        //transform.LookAt(Input.mousePosition);

        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }
}
