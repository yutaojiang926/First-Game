using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{

    [SerializeField] private AudioSource chest;
    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // colliding with box
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // if we colided with object with the "Chest" tag, animate the chest
        // and move onto next level
        if (collision.gameObject.CompareTag("Player"))
        {
            GetComponent<Animator>().SetBool("Animated", true);
            chest.Play();
            // disable movement
            collision.gameObject.GetComponent<PlayerMovement>().enabled = false;
        }
    }
}
