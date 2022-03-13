using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator anim;
    public Collider2D col;
    public float speed = 10;
    public float Jump = 1;
    public LayerMask ground;
     
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
            SceneManager.LoadScene(0);
        }
        Movement();
        SwitchAnim();
    }

    void Movement()
    {  
        float horizontalMove = Input.GetAxis("Horizontal");
        float faceDirection = Input.GetAxisRaw("Horizontal");
        //Player turn around
        if(faceDirection != 0)
        {
            transform.localScale = new Vector3(faceDirection,1,1);
        }

        //Player movement
        rb.velocity = new Vector2(horizontalMove * speed, rb.velocity.y);

        //Animate Moving
        anim.SetFloat("Walking", Mathf.Abs(horizontalMove));

        //Player jump
        if(Input.GetButton("Jump") && col.IsTouchingLayers(ground))
        {
            anim.SetBool("Jumping", true);
            rb.velocity = new Vector2(rb.velocity.x, Jump);
        }
    }

    void SwitchAnim()
    {
        anim.SetBool("Idle", false);
        if(anim.GetBool("Jumping"))
        {
            if(rb.velocity.y < 0)
            {
                anim.SetBool("Jumping", false);
                anim.SetBool("Droping", true);
            }
        }else if(col.IsTouchingLayers(ground))
        {
            anim.SetBool("Droping", false);
            anim.SetBool("Idle", true);
        }
    }

}
