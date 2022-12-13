using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class CondorMovement
/// </summary>
public class CondorMovement : MonoBehaviour
{
    // Speed of the condor
    float speed = 10;

    // Player
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
        if (transform.position.z < player.transform.position.z - 100)
            speed = 0;
        if(transform.position.z <= player.transform.position.z)
        {
            if(transform.childCount > 2)
            {
                GameObject dripping = transform.GetChild(2).gameObject;
                dripping.transform.parent = null;
                dripping.GetComponent<Rigidbody>().useGravity = true;
            }
        }
    }
}
