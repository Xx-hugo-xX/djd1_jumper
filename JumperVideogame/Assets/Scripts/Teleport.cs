using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    [SerializeField] Transform teleportIndicatorT;
    [SerializeField] SpriteRenderer teleportIndicatorSR;


    Rigidbody2D rigidBody;
    bool canTeleport;

    Vector3 wantedPosition;


    bool isTeleportPossible
    {
        get
        {
            return canTeleport;
        }
        set
        {
            canTeleport = value;
        }
    }

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        teleportIndicatorSR.enabled = false;
    }
    // Update is called once per frame
    void Update()
    {
        // Set wantedPosition as the mouse position
        wantedPosition = new Vector3(Camera.main.ScreenToWorldPoint
                        (Input.mousePosition).x, Camera.main.ScreenToWorldPoint
                        (Input.mousePosition).y, transform.position.z);

        // Activate teleport when Right Mouse Button is being pressed
        if (Input.GetMouseButton(1))
        {
            teleportIndicatorT.transform.position = wantedPosition;
            teleportIndicatorSR.enabled = true;

            if (Input.GetButtonDown("Teleport"))
            {
                if (isTeleportPossible) canTeleport = false;
                else canTeleport = true;
            }


            if (isTeleportPossible)
            {
                Debug.Log("TELEPORT IS POSSIBLE");
                //Teleport to position when Left Mouse Button is released
                if (Input.GetMouseButtonUp(0))
                {
                    teleportIndicatorSR.color = new Color(0.0f, 103.0f, 255.0f, 1.0f);

                    // Set new player position as the wantedPosition
                    transform.position = wantedPosition;
                }
            }
            else
            {
                Debug.Log("teleport is not possible");

                teleportIndicatorSR.color = new Color(154.0f, 0.0f, 0.0f, 1.0f);
            }

        }
        else
        {
            teleportIndicatorSR.enabled = false;
        }
    }
    /*
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(collision.gameObject);
        }
    }
    */
}