using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class burgerController : MonoBehaviour
{
    float velocity;
    float startTime, passedTime;
    public Sprite[] sprites;

    private void Awake()
    {
        startTime = 0f;
    }

    void Update()
    {
        transform.position = transform.position + (Vector3.down * 0.01f);
        Debug.Log(velocity);
        int rSprite = Random.Range(0, sprites.Length);
        if (passedTime > 1f)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = sprites[rSprite];
        }
        else passedTime += Time.deltaTime;
    }

    public void gainSpeed(float rVel)
    {
        velocity = rVel; 
    }    
}
