using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CamerasManager : MonoBehaviour
{
    [SerializeField] Collider2D playerCollider;

    [SerializeField] Camera     subwayEntryCamera;
    [SerializeField] Camera     subwayStairsCamera;
    [SerializeField] Camera     subwayStation1Camera;
    [SerializeField] Camera     elevatorCamera;
    [SerializeField] Camera     subwayStation2Camera;
    [SerializeField] Camera     lairPathCamera;
    [SerializeField] Camera     lairFloor1Camera;
    [SerializeField] Camera     lairFallCamera;
    [SerializeField] Camera     lairFloor2Camera;
    [SerializeField] Camera     finalRoomCamera;

    [SerializeField] TilemapCollider2D stairs1;
    [SerializeField] TilemapCollider2D stairs2;

    List<Camera> cameraList = new List<Camera>();

    int lastStairCheck;

    private void Start()
    {
        cameraList.Add(subwayEntryCamera);
        cameraList.Add(subwayStairsCamera);
        cameraList.Add(subwayStation1Camera);
        cameraList.Add(elevatorCamera);
        cameraList.Add(subwayStation2Camera);
        cameraList.Add(lairPathCamera);
        cameraList.Add(lairFloor1Camera);
        cameraList.Add(lairFallCamera);
        cameraList.Add(lairFloor2Camera);
        cameraList.Add(finalRoomCamera);
    }

    private void Update()
    {
        if (lastStairCheck == 1)
        {
            stairs1.enabled = true;
            stairs2.enabled = false;
        }
        else
        {
            stairs1.enabled = false;
            stairs2.enabled = true;
        }
    }


    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "SubwayEntryCollider")
        {
            DisableAllCameras();
            subwayEntryCamera.enabled = true;
        }

        if (collider.tag == "SubwayStairsCollider")
        {
            DisableAllCameras();
            subwayStairsCamera.enabled = true;
        }

        if (collider.tag == "SubwayStation1Collider")
        {
            DisableAllCameras();
            subwayStation1Camera.enabled = true;
        }

        if (collider.tag == "ElevatorCollider")
        {
            DisableAllCameras();
            elevatorCamera.enabled = true;
        }

        if (collider.tag == "SubwayStation2Collider")
        {
            DisableAllCameras();
            subwayStation2Camera.enabled = true;
        }

        if (collider.tag == "LairPathCollider")
        {
            DisableAllCameras();
            lairPathCamera.enabled = true;
        }

        if (collider.tag == "LairFloor1Collider")
        {
            DisableAllCameras();
            lairFloor1Camera.enabled = true;
        }

        if (collider.tag == "LairFallCollider")
        {
            DisableAllCameras();
            lairFallCamera.enabled = true;
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

        if (collider.tag == "StairCheck1") lastStairCheck = 1;
        if (collider.tag == "StairCheck2") lastStairCheck = 2;

    }

    public void DisableAllCameras()
    {
        foreach (Camera cam in cameraList)
        {
            cam.enabled = false;
        }
    }
}
