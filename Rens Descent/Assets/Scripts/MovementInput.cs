using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MovementInput : MonoBehaviour
{
    public static MovementInput Instance;
    //Jumping variables
    bool canJump, isJumping;
    public bool canDoubleJump;

    //Changable character control variables
    public float speed, jumpForce;
    //used for some velocity calculations incase.
    float horizontal, vertical;

    bool paused;
    public GameObject pausePanel;
    public Rigidbody2D rb;

    //Animation variables
    public bool facingRight;
    //Animation States
    /*
        0 - Idle 
        1 - Run
        2 - Jump
        3 - Second Jump 
        4 - Falling
        5 - Landing
        6 - Death
    */
    Animator anim;
    public bool offGroundl;
    bool isDead;

    /*
        shield anim states 
        0 - ShieldActivate
        1 - shieldon
        2 - shield break
    */
    public Animator shieldAnim;

    //Score
    public ScoreManager _scoreManager; 

    //Checkpoints
    private Transform _resetPoint;
    private GameObject prevCheckPoint;


    //Powerups
    bool shield;
    public GameObject _shield;
    bool invuln;
    public GameObject _invuln;
    public float timeInvuln;

    void Start()
    {
        canJump = true;
        isJumping = false;
        canDoubleJump = false;
        anim = GetComponent<Animator>();
        anim.SetInteger("animState", 4);
        Instance = this;
        
    }

    void Update()
    {
        if (offGroundl)
        {
            anim.SetInteger("animState", 4);
        }
        else
        {
            anim.SetInteger("animState", 0);
        }
        

        //Resetting the variables
        if(_scoreManager == null)
        {
            _scoreManager = GameObject.Find("/Canvas").GetComponent<ScoreManager>();
        }
        if(pausePanel == null)
        {
            pausePanel = _scoreManager.gameObject.GetComponent<PauseMenu>()._pausePanel;
        }
        if (shieldAnim == null)
        {
            foreach(Transform child in transform)
            {
                Debug.Log(child);
                if (child.name == "PlayerShield") shieldAnim = child.gameObject.GetComponent<Animator>();
            }    
        }

        //horizontal movement 
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        transform.position += movement * Time.deltaTime * speed;

        //animation checks
        if (!paused)
        {
            if (movement.x > 0)
            {
                if(!offGroundl) anim.SetInteger("animState", 1);

                facingRight = true;
                transform.localScale = new Vector3(0.5f, 0.5f, 0.3f);
            }
            else if (movement.x < 0)
            {
                if(!offGroundl) anim.SetInteger("animState", 1);
                facingRight = false;
                transform.localScale = new Vector3(-0.5f, 0.5f, 0.3f);
            }
        }



        //jumping
        if (Input.GetButtonDown("Jump") && !paused)
        {

            if (canJump)
            {
                anim.SetInteger("animState", 2);
                rb.velocity = new Vector2(0f, 0f);
                rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
                canJump = false;
                canDoubleJump = true;
                isJumping = true;
            }
            else if (canDoubleJump)
            {
                anim.SetInteger("animState", 3);
                rb.velocity = new Vector2(0f, 0f);
                rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
                canDoubleJump = false;
            }
        }

        if (Input.GetButtonUp("Jump")) isJumping = false;




        if (Input.GetButtonDown("Pause") || Input.GetKeyDown(KeyCode.Escape))
        {
            if (!paused)
            {
                StartPause();
            }
            else if (paused)
            {
                StartResume();
            }
            
        }

        if (isDead) anim.SetInteger("animState", 6);
    }

    public void StartPause()
    {
        paused = true;
        Time.timeScale = 0f;
        pausePanel.SetActive(true);
    }

    public void StartResume()
    {
        paused = false;
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            offGroundl = false;
            canJump = true;
            isJumping = false;
        }
        else if (collision.gameObject.CompareTag("Danger"))
        {
            if (invuln) Debug.Log("invuln");
            else if (shield)
            {
                DisableShield();
            }
            else StartCoroutine(respawn());
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            offGroundl = true;
            canJump = false;
            canDoubleJump = true;
        }
    }

    //Called by checkpoints when their trigger entered. 
    public void updateCheckpoint(GameObject newCP)
    {
        Debug.Log("triggered");
        if (prevCheckPoint != null)
        {
            prevCheckPoint.GetComponent<Animator>().SetBool("isActive", false);
        }
        prevCheckPoint = newCP;
        prevCheckPoint.GetComponent<Animator>().SetBool("isActive", true);
        _resetPoint = prevCheckPoint.GetComponent<CheckpointManager>().resetPoint;
    }

    //used upon death
    public IEnumerator respawn()
    {
        anim.SetInteger("animState", 6);
        isDead = true;
        yield return new WaitForSeconds(0.5f);
        isDead = false;
        yield return new WaitForSeconds(0.3f);
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        transform.position = _resetPoint.position;
        _scoreManager.GetComponent<ScoreManager>().score -= 100;
        prevCheckPoint.GetComponent<CheckpointManager>().Respawning();
        yield return new WaitForSeconds(0.25f);
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(0.25f);
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(0.25f);
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(0.25f);
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(0.25f);
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(0.25f);
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        Debug.Log("Finished respawn");
    }

    public void ActivateShield()
    {
        shield = true;
        _shield.SetActive(true);
    }

    public void DisableShield()
    {
        StartCoroutine(breakShield());
    }
    
    public void startInvincibility()
    {
        invuln = true;
        _invuln.SetActive(true);
        StartCoroutine(invulnTimer());
    }
    
    IEnumerator invulnTimer()
    {
        yield return new WaitForSeconds(timeInvuln);
        invuln = false;
        _invuln.SetActive(false);
    }

    IEnumerator breakShield()
    {
        shieldAnim.SetBool("isBroken", true);
        yield return new WaitForSeconds(1f);
        shield = false;
        _shield.SetActive(false);
    }
}
