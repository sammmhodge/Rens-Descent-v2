using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ghostController : MonoBehaviour
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
        transform.position = transform.position + (Vector3.up * 0.01f);
        Debug.Log(velocity);
        int rSprite = Random.Range(0, sprites.Length);
        if (passedTime > 1f)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = sprites[rSprite];
            passedTime = 0f;
        }
        else passedTime += Time.deltaTime;
    }

    public void gainSpeed(float rVel)
    {
        velocity = rVel;
    }

}
