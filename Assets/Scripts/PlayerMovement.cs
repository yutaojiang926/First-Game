using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // references to components of the player
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;
    private BoxCollider2D coll;
    private ItemCollector items;

    // number of frames you can still jump after leaving the edge
    [SerializeField] private int SafeFrames = 10;
    private int ForgivenessFrames = 10;


    // animation states
    private enum AnimationState { idle, running, jumping, falling };
    //private AnimationState state = AnimationState.idle; 

    // vars determining move and jump speed
    [SerializeField] private float moveSpeed = 0.5f;
    [SerializeField] private float maxMoveSpeed = 5f;
    [SerializeField] private float jumpSpeed = 10f;

    // reduced gravity when jumping
    [SerializeField] private float jumpGravity = 2;
    [SerializeField] private float normalGravity = 3;

    //ground thats considered jumpable
    [SerializeField] private LayerMask jumpableGround;

    // sound effects for player
    [SerializeField] private AudioSource jumpSoundEffect;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        coll = GetComponent<BoxCollider2D>();
        items = GetComponent<ItemCollector>();
    }

    // Update is called once per frame
    void Update()
    {
        // if game is paused dont do anything
        if (PauseMenu.gamePaused)
        {
            return;
        }

        //moving left and right
        float dirX = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveSpeed * dirX, rb.velocity.y);

        //jumping
        if (Input.GetButtonDown("Jump") && (isGrounded() || ForgivenessFrames > 0))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed + items.getCoin());
            rb.gravityScale = jumpGravity;
            jumpSoundEffect.Play();
        }
        // if we have coins, can consume one to jump in the air
        else if (Input.GetButtonDown("Jump") && items.getCoin() > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed + items.getCoin());
            rb.gravityScale = jumpGravity;
            items.setCoin(items.getCoin() - 1);
            jumpSoundEffect.Play();
        }
        // if we arent holding the jump key, return gravity to normal
        else if (!Input.GetButton("Jump") && !isGrounded())
        {
            rb.gravityScale = normalGravity;
        }

        // falls off the map
        if(transform.position.y < -5)
        {
            GetComponent<PlayerLife>().Die();
        }
        UpdateAnimationState();
        Framed();
    }

    // animation update
    // anim object uses bools "isRunning"
    // sprite object uses bools "flipX
    private void UpdateAnimationState()
    {
        AnimationState state;
        //moving right
        if(rb.velocity.x > 0.1f)
        {
            state = AnimationState.running;
            sprite.flipX = false;
        }
        //moving left
        else if(rb.velocity.x < -0.1f)
        {
            state = AnimationState.running;
            sprite.flipX = true;
        }
        else 
        {
            state = AnimationState.idle;
        }

        // jumping animation takes priority, so state will be overwritten
        if(rb.velocity.y > 0.1f)
        {
            state = AnimationState.jumping;
        }else if(rb.velocity.y < -0.1f)
        {
            state = AnimationState.falling;
        }

        // sets the animation integer "state" as state var
        // need to cast since state is enum AnimationState
        anim.SetInteger("state", (int)state);

        

        // calculate current speed in abs value
        float currSpeed;
        if(rb.velocity.x < 0)
        {
            currSpeed = rb.velocity.x * -1;
        }
        else
        {
            currSpeed = rb.velocity.x;
        }
        // faster running speed = faster animations
        float animSpeed = currSpeed / maxMoveSpeed;
        anim.SetFloat("runningSpeed", animSpeed);
    }

    private void Framed()
    {
        ForgivenessFrames--;
    }
    // returns true if touching ground, false otherwise
    private bool isGrounded()
    {
        if( Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, 0.1f, jumpableGround))
        {
            ForgivenessFrames = SafeFrames;
            return true;
        }
        return false;
    }
}
