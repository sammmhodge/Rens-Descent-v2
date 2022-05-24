using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelAdvance : MonoBehaviour
{
    public Camera camera;
    
    public float newCameraPos;
    Vector3 startpos, endpos;

    public float speed = 1f;

    private float startTime;
    private float Distance;
    private bool canMove;
    // Start is called before the first frame update
    void Start()
    {
        endpos = new Vector3(0, newCameraPos, 0);
    }
    private void Update()
    {
        if(canMove)
        {
            float distanceCovered = (Time.time - startTime) * speed;

            float fractionTravelled = distanceCovered / Distance;

            camera.transform.position = Vector3.Lerp(startpos, endpos, fractionTravelled);
        }
        float completeCheck = Vector3.Distance(camera.transform.position, endpos);
        if (completeCheck <= 0.01f) canMove = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            startpos = camera.transform.position;
            startTime = Time.time;
            Distance = Vector3.Distance(startpos, endpos);
            canMove = true;
        }
    }


}
