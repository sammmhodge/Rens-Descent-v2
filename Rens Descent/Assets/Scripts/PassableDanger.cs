using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassableDanger : MonoBehaviour
{
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
        if(collision.CompareTag("Player"))
        {
            if (collision.GetComponent<MovementInput>()._invuln.activeSelf) Debug.Log("invulnerable"); 
            else if (collision.GetComponent<MovementInput>()._shield.activeSelf) collision.GetComponent<MovementInput>().DisableShield();
            else StartCoroutine(collision.GetComponent<MovementInput>().respawn());
        }
    }
}
