using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // references to components of the player
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;

    // animation states
    private enum AnimationState { idle, running, jumping, falling };
    //private AnimationState state = AnimationState.idle; 

    // vars determining move and jump speed
    [SerializeField] private float moveSpeed = 0.5f;
    [SerializeField] private float maxMoveSpeed = 5f;
    [SerializeField] private float jumpSpeed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        Debug.Log("Hello world");
    }

    // Update is called once per frame
    void Update()
    {
        //moving left and right
        float dirX = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveSpeed * dirX, rb.velocity.y);
        /*if (dirX == 0)
        {
            if (rb.velocity.x > moveSpeed)
            {
                rb.velocity = new Vector2(rb.velocity.x - 0.5f *moveSpeed, rb.velocity.y);
            }
            else if (rb.velocity.x < -1 * moveSpeed)
            {
                rb.velocity = new Vector2(rb.velocity.x + 0.5f *moveSpeed, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
            }
        }
        else if (rb.velocity.x < maxMoveSpeed - moveSpeed && rb.velocity.x >= 0)
        {
            rb.velocity = new Vector2(rb.velocity.x + dirX * moveSpeed, rb.velocity.y);
        }
        else if (rb.velocity.x > -1 * (maxMoveSpeed - moveSpeed) && rb.velocity.x <= 0)
        {
            rb.velocity = new Vector2(rb.velocity.x + dirX * moveSpeed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(dirX * maxMoveSpeed, rb.velocity.y);
        }*/
        //jumping
        if (Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y + jumpSpeed);
        }

        UpdateAnimationState();
        
    }

    // animation update
    // anim object uses bools "isRunning"
    // sprite object uses bools "flipX
    private void UpdateAnimationState()
    {
        AnimationState state;
        // not moving
        if (rb.velocity.x == 0)
        {
            state = AnimationState.idle;
        }
        //moving right
        else if(rb.velocity.x > 0)
        {
            state = AnimationState.running;
            sprite.flipX = false;
        }
        //moving left
        else
        {
            state = AnimationState.running;
            sprite.flipX = true;
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
}
