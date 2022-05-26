using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public Transform resetPoint;

    public GameObject[] RespawnObjects;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.gameObject.CompareTag("Player")) collision.GetComponent<MovementInput>().updateCheckpoint(gameObject);

    }

    public void Respawning()
    {
        for (int i = 0; i < RespawnObjects.Length; i++)
        {
            if(RespawnObjects[i] != null) RespawnObjects[i].SetActive(true);
        }
    }
}
