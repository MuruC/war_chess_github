﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    public float panSpeed;
    public float panBorderThickness;
    public Vector2 panLimit;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        mouseScroller();
        checkMouseBound();

    }

    void mouseScroller()
    {
        //Zoom out
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (Camera.main.orthographicSize <= 9)
            {
                Camera.main.orthographicSize += 2;
            }
        }
        //Zoom in
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (Camera.main.orthographicSize > 3)
            {
                Camera.main.orthographicSize -= 2;
            }
        }
    }

    void checkMouseBound()
    {
        Vector3 pos = transform.position;
        //Debug.Log(Input.mousePosition.x);
        if (Input.GetKey("a") || Input.mousePosition.x <= panBorderThickness)
        {
            pos.x -= panSpeed * Time.deltaTime;
        }
        if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBorderThickness)
        {
            pos.x += panSpeed * Time.deltaTime;
        }

            if (Input.GetKey("s") || Input.mousePosition.y <= panBorderThickness)
            {
                pos.y -= panSpeed * Time.deltaTime;
            }
            if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panBorderThickness)
            {
                pos.y += panSpeed * Time.deltaTime;
            }

        pos.x = Mathf.Clamp(pos.x,-panLimit.x,panLimit.x);
        pos.y = Mathf.Clamp(pos.y, -panLimit.y+12, panLimit.y);
        transform.position = pos;
        }
    
}

