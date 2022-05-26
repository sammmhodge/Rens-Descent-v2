using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatanController : MonoBehaviour
{
    public bool animFinished;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(animFinished)
        {
            Destroy(gameObject);
                
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) gameObject.GetComponent<Animator>().SetBool("startAnimation", true);
    }
}
