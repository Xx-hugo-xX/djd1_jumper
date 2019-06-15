using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public enum Mode {PositionLock, FeedbackLoop, FixedSpeed};

    public Mode      mode;
    public Transform target;
    public Vector3   offset;
    public float     cameraSpeed = 0.1f;
    public bool      enforceBounds;
    public bool      onlyXaxis;
    public bool      onlyYaxis;
    public Rect      bounds;
    new    Camera    camera;

    void Start()
    {
        camera = GetComponent<Camera>();
    }

    void FixedUpdate()
    {
        Vector3 newPosition = target.position + offset;

        Vector3 delta = newPosition - transform.position;

        switch (mode)
        {
            case Mode.PositionLock:
                break;
            case Mode.FeedbackLoop:
                newPosition = transform.position + delta * cameraSpeed *
                    Time.fixedDeltaTime;
                break;
            case Mode.FixedSpeed:
                newPosition = Vector3.MoveTowards(transform.position, newPosition,
                    cameraSpeed * Time.fixedDeltaTime);
                break;
        }

        if (enforceBounds)
        {
            float sizeY = camera.orthographicSize;
            float sizeX = sizeY * camera.aspect;

            newPosition.x = Mathf.Clamp(newPosition.x, bounds.xMin + sizeX,
                bounds.xMax - sizeX);
            newPosition.y = Mathf.Clamp(newPosition.y, bounds.yMin + sizeY,
                bounds.yMax - sizeY);
        }

        newPosition.z = transform.position.z;
        if (onlyXaxis)
        {
            newPosition.y = transform.position.y;
            transform.position = newPosition;
        }
        else if(onlyYaxis)
        {
            newPosition.x = transform.position.x;
            transform.position = newPosition;
        }

        else transform.position = newPosition;
    }

    private void OnDrawGizmosSelected()
    {
        if (enforceBounds)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(bounds.center, bounds.size);
        }
    }
}
