using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubwayCar : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] Transform startPosition;
    [SerializeField] Transform endPosition;
    [SerializeField] Camera wantedCamera;
    [SerializeField] SpriteRenderer subwaySprite;


    Rigidbody2D rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        transform.position = startPosition.position;
        subwaySprite.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {   
        Vector2 currentVelocity = rigidBody.velocity;
        currentVelocity.x =  moveSpeed;
        rigidBody.velocity = currentVelocity;

        if (wantedCamera.enabled == true) subwaySprite.enabled = true;
        else subwaySprite.enabled = false;

        if (transform.position.x >= endPosition.position.x) transform.position = startPosition.position;
    }
}
