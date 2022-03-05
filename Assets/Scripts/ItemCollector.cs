using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    // coin counter
    private int coins = 0;
    [SerializeField] private Text coinText;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // if we collided with an object with the "Coin" tag, destroy it
        if (collision.gameObject.CompareTag("Coin"))
        {
            Destroy(collision.gameObject);
            coins++;
            coinText.text = "Coins " + coins;
        }
    }
}
