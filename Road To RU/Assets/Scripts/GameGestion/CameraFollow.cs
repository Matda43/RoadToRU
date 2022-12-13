using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Class CameraFollow
/// </summary>
public class CameraFollow : MonoBehaviour
{
    // Speed by default of the main camera
    float speed = 0.008f;

    // Speed added to the default speed depending on player distance
    float offsetSpeed = 0;

    // Player
    GameObject player;

    // Initial position of the main camera
    Vector3 offset = new Vector3(1, 14, -5);

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        this.transform.position = player.transform.position + offset;
        this.transform.Rotate(new Vector3(65, -10, 0));
    }

    // Called each frame
    void FixedUpdate()
    {
        if (player.GetComponent<Player>().isDead)
            centerPlayer();
        else if (player.GetComponent<Player>().debugModeCamera)
        {
            Vector3 playerPosition = player.transform.position + offset;
            this.transform.position = playerPosition;
        }
        else if (player.GetComponent<Player>().gameStart)
        {
            Vector3 playerPosition = player.transform.position;
            Vector3 newPosition = transform.position + Vector3.forward * 7;
            newPosition.y = playerPosition.y + offset.y;

            if (newPosition.z < playerPosition.z + 3)
            {
                newPosition.z = playerPosition.z;
                offsetSpeed = 0.005f;
            }
            else
            {
                offsetSpeed = 0;
            }

            Vector3 smoothPosition = Vector3.Lerp(transform.position, newPosition, speed + offsetSpeed);
            this.transform.position = smoothPosition;
        }
        
    }

    /// <summary>
    /// Allows to increase the speed of the camera 
    /// </summary>
    public void incSpeed()
    {
        speed += 0.001f;
    }

    /// <summary>
    /// Allows to center the camera on the player when he dies
    /// </summary>
    public void centerPlayer()
    {
        Vector3 playerPosition = player.transform.position + offset;
        transform.position = Vector3.MoveTowards(transform.position, playerPosition, 0.1f);
    }
}
