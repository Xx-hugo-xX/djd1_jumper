using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CamerasManager : MonoBehaviour
{
    [SerializeField] Collider2D playerCollider;

    [SerializeField] Camera     subwayEntryCamera;
    [SerializeField] Camera     subwayStation1Camera;
    [SerializeField] Camera     subwayStation2Camera;
    [SerializeField] Camera     lairFloor1Camera;
    [SerializeField] Camera     lairFloor2Camera;
    [SerializeField] Camera     finalRoomCamera;

    List<Camera> cameraList = new List<Camera>();

    private void Start()
    {
        cameraList.Add(subwayEntryCamera);
        cameraList.Add(subwayStation1Camera);
        cameraList.Add(subwayStation2Camera);
        cameraList.Add(lairFloor1Camera);
        cameraList.Add(lairFloor2Camera);
        cameraList.Add(finalRoomCamera);
    }


    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "SubwayEntryCollider")
        {
            DisableAllCameras();
            subwayEntryCamera.enabled = true;
        }

        if (collider.tag == "SubwayStation1Collider")
        {
            DisableAllCameras();
            subwayStation1Camera.enabled = true;
        }

        if (collider.tag == "SubwayStation2Collider")
        {
            DisableAllCameras();
            subwayStation2Camera.enabled = true;
        }

        if (collider.tag == "LairFloor1Collider")
        {
            DisableAllCameras();
            lairFloor1Camera.enabled = true;
        }

        if (collider.tag == "LairFloor2Collider")
        {
            DisableAllCameras();
            lairFloor2Camera.enabled = true;
        }

        if (collider.tag == "FinalRoomCollider")
        {
            DisableAllCameras();
            finalRoomCamera.enabled = true;
        }
    }

    public void DisableAllCameras()
    {
        foreach (Camera cam in cameraList)
        {
            cam.enabled = false;
        }
    }
}
