using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parralax : MonoBehaviour
{
    //public Transform[] layers;
    private Transform cameraTransform;
    private float lastCameraY;

    public float parralaxSpeed;
    // Start is called before the first frame update
    void Start()
    {
        cameraTransform = Camera.main.transform;
        lastCameraY = cameraTransform.position.y;
        Debug.Log("Last Camera Y in Start" + lastCameraY);

    }

    // Update is called once per frame
    void Update()
    {
        float deltaY = cameraTransform.position.y - lastCameraY;
        Debug.Log(cameraTransform.position.y + " " + lastCameraY + " = " + deltaY); ;
        transform.position += Vector3.down * (deltaY * parralaxSpeed);
        lastCameraY = cameraTransform.position.y;
    }
}
