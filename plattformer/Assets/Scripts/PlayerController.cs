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
    private bool isTouchingGround = true;

    //ladder vars
    [HideInInspector] public bool canClimb = false;
    private float naturalGravity;
    [SerializeField] float climbSpeed = 3f;
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

        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.up, whatIsLadder);

    
        if (hitInfo.collider.tag == "Ladder" && Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow)){
            canClimb = true;
            Climb(canClimb);
        }
        else
        {
            canClimb = false;
            Climb(canClimb);
        }

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
        if (normalColl.IsTouchingLayers(ground))
            isTouchingGround = true;

        //Jump
        if (Input.GetButtonDown("Jump") && isTouchingGround)
        {
            rb.velocity = new Vector2(rb.velocity.x, 30);
            isTouchingGround = false;
            state = State.jumping;
        }
        else if (Input.GetButtonUp("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, -5);
            state = State.idle;
        }

        //Crouch
        if (Input.GetButtonDown("Crouch") && isTouchingGround)
        {
            normalColl.enabled = false;
            crouchColl.enabled = true;
            state = State.crouching;
            if (Mathf.Abs(rb.velocity.x) > .1f)
            {
                state = State.running;
            }
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
            {
                Flip();
            }
        }
        else if (hDirection > 0) //right
        {
            rb.velocity = new Vector2(5, rb.velocity.y);
            if (!facingRight)
            {
                Flip();
            }
        }


        //Shooting
        if (Input.GetButtonDown("Fire1") && state == State.idle)
        {
            state = State.shooting;
        }
        else if (Input.GetButtonUp("Fire1"))
        {
            state = State.idle;
        }


        if (Input.GetButtonDown("Fire1") && state == State.running)
        {
            state = State.run_shooting;
        }
        else if (Input.GetButtonUp("Fire1"))
        {
            state = State.running;
        }

        if (rb.position.y < -7f)
        {
            Die();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            if (state == State.falling)
            {
                //Al caer en uno enemigo eliminarlo o hacer daño
            }
            else
            {
                state = State.damaged;
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
    }

    private void Climb(bool canClimb)
    {

        if (canClimb)
        {
            rb.gravityScale = 0;
            float vDirection = Input.GetAxis("Vertical");
            rb.velocity = new Vector2(rb.position.x, vDirection * climbSpeed);
        }
        else
        {
            rb.gravityScale = naturalGravity;
        }
    }

    public void TakeDamage(int damage)
    {
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
        // Cambia a la dirección opuesta
        facingRight = !facingRight;

        // Rotando al jugador
        transform.Rotate(0f, 180f, 0f);
    }



    private void StateSwitch()
    {
        //Todo: simplificar los if statements
        if (state == State.jumping)
        {

        }
        else if (state == State.crouching)
        {

        }
        else if (state == State.climbing)
        {

        }
        else if (state == State.shooting)
        {

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
            //running
            state = State.running;
            if (Input.GetButtonDown("Fire1"))
            {
                state = State.run_shooting;
                print("state: " + state);
            }

        }
        else
        {
            state = State.idle;
        }
    }
}
