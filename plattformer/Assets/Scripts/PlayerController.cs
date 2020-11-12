using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private GameMaster gm;

    private Rigidbody2D rb;
    private Animator anim;
    private enum State { idle, walking, running, jumping, crouching, climbing, shooting, run_shooting, falling, damaged };
    private State state = State.idle;
    [SerializeField] Collider2D normalColl;
    [SerializeField] Collider2D crouchColl;
    [SerializeField] private LayerMask ground;


    private bool facingRight = true;
    private bool canShoot = true;

    //ladder vars
    [HideInInspector] public bool canClimb = false;
    private float naturalGravity;
    [SerializeField] private float climbSpeed = 30f;
    [SerializeField] private float rayDistance;
    [SerializeField] private LayerMask whatIsLadder;
    [SerializeField] private int health = 100;
    [SerializeField] private Text healthText;
    [SerializeField] private int lifes = 4;
    [SerializeField] private Text LivesText;
    [SerializeField] private float damageForce = 15f;
    


    PlayerPos playerPos = new PlayerPos();

    private void Awake()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();

        //Text of the canvas
        healthText.text = health.ToString();
        LivesText.text = (gm.playerLifes + lifes).ToString();

    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        crouchColl.enabled = false;
        naturalGravity = rb.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {

        if (state != State.damaged)
        {
            this.Movement();
        }
        this.StateSwitch();
        anim.SetInteger("state", (int)state);
    }

    private void Movement()
    {
        float hDirection = Input.GetAxis("Horizontal");
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.up, whatIsLadder);

        try { 
            if (hitInfo.collider.CompareTag("Ladder")
                && (Input.GetKey(KeyCode.UpArrow)
                || Input.GetKey(KeyCode.DownArrow)))
            {
                print(hitInfo.collider);
                    state = State.climbing;
                    canClimb = true;
                    Climb(canClimb);
            }
            else
            {
                canClimb = false;
                Climb(canClimb);
            }
        }catch(NullReferenceException ex)
        {
            Debug.Log("No pasa nada");
        }

        //Jump
        if (Input.GetButtonDown("Jump") && normalColl.IsTouchingLayers(ground))
        {
            rb.velocity = new Vector2(rb.velocity.x, 30);
            state = State.jumping;
        }
        else if (Input.GetButtonUp("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, -5);
            state = State.idle;
        }

        //Crouch
        if (Input.GetButton("Crouch") && normalColl.IsTouchingLayers(ground))
        {
            normalColl.enabled = false;
            crouchColl.enabled = true;
            state = State.crouching;
        }
        else if (Input.GetButtonUp("Crouch"))
        {
            normalColl.enabled = true;
            crouchColl.enabled = false;
            state = State.idle;
        }

        //Left
        if (hDirection < 0)
        {
            rb.velocity = new Vector2(-5, rb.velocity.y);
            if (facingRight)
                Flip();
        }
        else if (hDirection > 0) //right
        {
            rb.velocity = new Vector2(5, rb.velocity.y);
            if (!facingRight)
                Flip();
        }


        //Shooting
        if (Input.GetButtonDown("Fire1"))
        {
            FindObjectOfType<AudioManager>().Play("playerShoot");
            state = State.shooting;
        }
        else if (Input.GetButtonUp("Fire1"))
        {
            state = State.idle;
        }

        if (rb.position.y < -7f)
        {
            Die();
        }

        anim.SetInteger("state", (int)state);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (other.gameObject.transform.position.x > transform.position.x)
            {
                //Getting bounced left and taking damage
                rb.velocity = new Vector2(-damageForce, rb.velocity.y);
            }
            else
            {
                //Getting bounced right and taking damage
                rb.velocity = new Vector2(damageForce, rb.velocity.y);
            }
            TakeDamage(50);
        }
    }

    private void Climb(bool canClimb)
    {
        if (canClimb)
        {
            state = State.climbing;
            rb.gravityScale = 0;
            float vDirection = Input.GetAxis("Vertical");
            rb.constraints = RigidbodyConstraints2D.FreezePositionX;
            rb.velocity = new Vector2(0, vDirection * climbSpeed);
        }
        else
        {
            state = State.idle;
            rb.gravityScale = naturalGravity;
        }
    }

    public void TakeDamage(int damage)
    {
        FindObjectOfType<AudioManager>().Play("playerDeath");
        state = State.damaged;
        health -= damage;
        healthText.text = health.ToString();
        if (health <= 0)
        {
            health = 100;
            healthText.text = health.ToString();
            Die();
        }
    }

    private void Die()
    {
        FindObjectOfType<AudioManager>().Play("playerDeath");
        --gm.playerLifes;
        LivesText.text = (gm.playerLifes + lifes).ToString();
        playerPos.Respawn();
        if (gm.playerLifes + lifes == 0)
        {
            print("Game over");
        }
    }

    private void Flip()
    {
        // Changes to the opposite direction
        facingRight = !facingRight;
        // rotate the whole player prefab
        transform.Rotate(0f, 180f, 0f);
    }

    //Runs code in different states of the player
    private void StateSwitch()
    {
        if (state == State.jumping)
        {

        }
        else if (state == State.crouching)
        {
            rb.constraints = RigidbodyConstraints2D.FreezePositionX;
        }
        else if (state == State.climbing)
        {

        }
        else if (state == State.shooting)
        {
            rb.constraints = RigidbodyConstraints2D.FreezePositionX;

        }
        else if (state == State.damaged)
        {
            if (Mathf.Abs(rb.velocity.x) < .1f)
            {
                state = State.idle;
            }
        }
        else if (Mathf.Abs(rb.velocity.x) > Mathf.Epsilon)
        {
            state = State.running;
            //FindObjectOfType<AudioManager>().Play("walking");
        }
        else
        {
            state = State.idle;
            rb.constraints = RigidbodyConstraints2D.None;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }
}
