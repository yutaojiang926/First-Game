using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // the transform (position) of the player
    [SerializeField] private Transform player;
    // Update is called once per frame
    void Update()
    {
        // transform refers to the parent's transform
        // follow the player, but keep its original z value
        transform.position = new Vector3(player.position.x, player.position.y+2, transform.position.z);
    }
}
