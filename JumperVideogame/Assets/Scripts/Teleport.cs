using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    [SerializeField] SpriteRenderer teleportIndicatorSR;
    [SerializeField] Transform      teleportIndicatorT;

    [SerializeField] float maxDistance = 110.0f;


    new Transform   transform;
    Vector3     wantedPosition;


    private bool isTeleportPossible { get; set; }
    private bool isInsideTilemap { get; set; }

    void Start()
    {
        transform = GetComponent<Transform>();
        teleportIndicatorSR.enabled = false;
    }
    // Update is called once per frame
    void Update()
    {
        // Set wantedPosition as the mouse position
        wantedPosition = new Vector3(Camera.main.ScreenToWorldPoint
                        (Input.mousePosition).x, Camera.main.ScreenToWorldPoint
                        (Input.mousePosition).y, transform.position.z);

        teleportIndicatorT.transform.position = wantedPosition;


        isTeleportPossible =  CheckTeleport();


        // Activate teleport when Right Mouse Button is being pressed
        if (Input.GetMouseButton(1))
        {
            //Debug.Log(Vector2.Distance(wantedPosition, transform.position));

            teleportIndicatorSR.enabled = true;


            if (isTeleportPossible)
            {
                teleportIndicatorSR.color = new Color(0.0f, 103.0f, 255.0f, 1.0f);

                //Teleport to position when Left Mouse Button is released
                if (Input.GetMouseButtonUp(0))
                {
                    // Set new player position as the wantedPosition
                    transform.position = wantedPosition;
                }
            }
            else
            {
                teleportIndicatorSR.color = new Color(154.0f, 0.0f, 0.0f, 1.0f);
            }

        }
        else
        {
            teleportIndicatorSR.enabled = false;
        }
    }


    // When the wanted position collides with an object, if the object is a tilemap ("Tilemap" tag), sets isInsideTilemap to true
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Tilemap")
        {
            isInsideTilemap = true;
        }
        else isInsideTilemap = false;
    }
    
    // When the wanted position stops colliding with an object, if the object is a tilemap ("Tilemap" tag), sets isInsideTilemap to false
    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.tag == "Tilemap")
        {
            isInsideTilemap = false;
        }
    }
    

    public bool CheckTeleport()
    {
        // Returns false if the teleport distance is greater than the allowed distance
        if (Vector2.Distance(wantedPosition, transform.position)
            > maxDistance) return false;


        // Returns false if wanted position is inside the Tilemap
        else if (isInsideTilemap) return false;

        // Returns true if it none of the restrictions apply
        else return true;
    }

}