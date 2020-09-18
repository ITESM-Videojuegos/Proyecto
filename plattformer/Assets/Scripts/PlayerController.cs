using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Jump or climb
        if (Input.GetKeyDown(KeyCode.UpArrow)){
            rb.velocity = new Vector2(rb.velocity.x, 20);
            anim.SetBool("jumping", true);

        }
        else if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            rb.velocity = new Vector2(rb.velocity.x, -5);
            anim.SetBool("jumping", false);
        }


        //Crouch
        if (Input.GetKeyDown(KeyCode.DownArrow)){
            rb.velocity = new Vector2(0, -5);
        }

        //Left
        if (Input.GetKey(KeyCode.LeftArrow)){
            rb.velocity = new Vector2(-5, rb.velocity.y);
            transform.localScale = new Vector2(-1, 1);
            anim.SetBool("running", true);
        }else if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            anim.SetBool("running", false);
        }


        //Right
        if (Input.GetKey(KeyCode.RightArrow)){
            rb.velocity = new Vector2(5, rb.velocity.y);
            transform.localScale = new Vector2(1, 1);
            anim.SetBool("running", true);
        }else if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            anim.SetBool("running", false);
        }

    }
}
