using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class DrippingContact
/// </summary>
public class DrippingContact : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

    }

    // Called if there is a collision with the collider of the player
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Player")
        {
            Destroy(transform.GetComponent<Rigidbody>());
            transform.localScale = new Vector3(2,0.5f,2);
            transform.position = other.transform.position + new Vector3(0,1,0);
            other.GetComponent<Player>().CanRetry = true;
            other.GetComponent<Player>().textFin = "You should take a shower...";
        }
    }
}
