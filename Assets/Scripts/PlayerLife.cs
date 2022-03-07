using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{

    private Animator anim;
    private BoxCollider2D coll;
    private Rigidbody2D rb;
    private PolygonCollider2D pcoll;
    private ItemCollector items;

    // sound effects for deaths
    [SerializeField] private AudioSource spikeDeath;
    private void Start()
    {
        anim = GetComponent<Animator>();
        coll = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        pcoll = GetComponent<PolygonCollider2D>();
        // sets up waiter function
        StartCoroutine(wait(0f));
    }

    IEnumerator wait(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
    }
    // on collision
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Spike"))
        {
            spikeDeath.Play();
            Die();
        }
        
    }

    // when we die, make player bounce and fall thru the map
    // also disables player movement (needs to be reenabled when level restarts
    private void Die()
    {
        // turns on death animation
        anim.SetTrigger("death");

        // makes player sprite fall thru map
        coll.enabled = false;
        pcoll.enabled = false;

        // stops player inputs
        GetComponent<PlayerMovement>().enabled = false;

        // sends our player up
        rb.velocity = new Vector2(rb.velocity.x, 10);

        // stops camera from moving
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>().enabled = false;
    }

    // restarts current level
    // currently called by events in the player death animation
    private void RestartLevel()
    {
        Debug.Log(SceneManager.GetActiveScene().name);
        // restarts from level 1
        SceneManager.LoadScene("Level 1");
        // reload active scene
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
