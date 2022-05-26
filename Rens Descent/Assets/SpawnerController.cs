using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    public Transform[] spawnPoints;
    public float minTime, maxTime;
    public float minVel, maxVel;
    float passedTime;
    float rTime;
    public enum dirs
    {
        Up,
        Down,
        Left,
        Right
    };

    public dirs Directions = dirs.Down;

    public GameObject objectSpawned;

    private void Start()
    {
        passedTime = 0f;
        rTime = Random.Range(minTime, maxTime);
    }
    // Update is called once per frame
    void Update()
    {
        
        float rVel = Random.Range(minVel, maxVel);
        int rSpawner = Random.Range(0, spawnPoints.Length);

        if (passedTime > rTime)
        {
            GameObject rObject = GameObject.Instantiate(objectSpawned);
            rObject.transform.position = spawnPoints[rSpawner].position;
            rObject.GetComponent<burgerController>().gainSpeed(rVel);
            Debug.Log("rVel is " + rVel);
            passedTime = 0f;
            rTime = Random.Range(minTime, maxTime);
        }
        else passedTime += Time.deltaTime;
        
    }
}
