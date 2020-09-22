using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private enum State {idle, walking, running, jumping, crouching, climbing, shooting, damaged};
    private State state = State.idle;
    private Collider2D coll;
    [SerializeField] private LayerMask ground;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float hDirection = Input.GetAxis("Horizontal");
        float vDirection = Input.GetAxis("Vertical");

        //Jump or climb
        if (Input.GetButtonDown("Jump") && coll.IsTouchingLayers(ground)){
            rb.velocity = new Vector2(rb.velocity.x, 30);
            state = State.jumping;
        }
        else if (Input.GetButtonUp("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, -5);
            state = State.idle;
        }


        //Crouch
        if (Input.GetKeyDown(KeyCode.DownArrow)){
            rb.velocity = new Vector2(0, -5);
        }


        //Left
        if (hDirection < 0){
            rb.velocity = new Vector2(-5, rb.velocity.y);
            transform.localScale = new Vector2(-1, 1);
        }else if (hDirection > 0) //right
        {
            rb.velocity = new Vector2(5, rb.velocity.y);
            transform.localScale = new Vector2(1, 1);
        }
        else
        {

        }

        this.StateSwitch();
        anim.SetInteger("state", (int)state); 
    }

    private void StateSwitch()
    {
        if(state == State.jumping)
        {

        }else if(Mathf.Abs(rb.velocity.x) > Mathf.Epsilon)
        {
            //running
            state = State.running;
        }
        else
        {
            state = State.idle;
        }
    }
}
