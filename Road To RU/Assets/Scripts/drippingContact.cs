using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drippingContact : MonoBehaviour
{

    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Player")
        {
            Destroy(transform.GetComponent<Rigidbody>());
            transform.localScale = new Vector3(2,0.5f,2);
            transform.position = other.transform.position + new Vector3(0,1,0);
        }
    }
}
