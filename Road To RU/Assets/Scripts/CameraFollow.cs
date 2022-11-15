using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    float speed = 0.005f;
    float offsetSpeed = 0;
    GameObject player;
    Vector3 offset = new Vector3(3, 12, -5);


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        this.transform.position = player.transform.position + offset;
        this.transform.Rotate(new Vector3(65, -10, 0));
    }

    void FixedUpdate()
    {
        Vector3 playerPosition = player.transform.position;
        Vector3 newPosition = transform.position + Vector3.forward * 5;
        newPosition.y = playerPosition.y + offset.y;
        
        if(newPosition.z < playerPosition.z + 3)
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

    public void incSpeed()
    {
        speed += 0.001f;
    }
}
