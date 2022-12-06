using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    float speed = 0.008f;
    float offsetSpeed = 0;
    GameObject player;
    Vector3 offset = new Vector3(1, 14, -5);

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        this.transform.position = player.transform.position + offset;
        this.transform.Rotate(new Vector3(65, -10, 0));
    }

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

    public void incSpeed()
    {
        speed += 0.001f;
    }

    public void centerPlayer()
    {
        Vector3 playerPosition = player.transform.position + offset;
        transform.position = Vector3.MoveTowards(transform.position, playerPosition, 0.1f);
    }
}
