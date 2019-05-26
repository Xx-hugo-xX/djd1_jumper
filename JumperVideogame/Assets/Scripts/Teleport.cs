using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    Rigidbody2D rigidBody;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        // Activate teleport when Right Mouse Button is being pressed
        if (Input.GetMouseButton(1))
        {
            //Teleport to position when Left Mouse Button is released
            if (Input.GetMouseButtonUp(0))
            {
                // Set new player position
                transform.position = new Vector3(Camera.main.ScreenToWorldPoint
                    (Input.mousePosition).x, Camera.main.ScreenToWorldPoint
                    (Input.mousePosition).y, transform.position.z);
            }
        }
    }
}