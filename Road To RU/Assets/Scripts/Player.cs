using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Player : MonoBehaviour
{
    Vector3 spawn = Vector3.zero - new Vector3(0,0,6);
    Vector3 direction;
    bool move = false;

    bool blockForward = false;
    bool blockBack = false;
    bool blockLeft = false;
    bool blockRight = false;

    public bool gameStart = false;
    public bool debugModeCamera = true;
    // Start is called before the first frame update
    void Avake()
    {
        direction = Vector3.zero;
        initialisation();
    }

    public void initialisation()
    {
        this.transform.position = spawn;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && !blockForward) {
            direction = Vector3.forward;
            move = true;
        }else if (Input.GetKeyDown(KeyCode.S) && transform.position.z > -2 && !blockBack)
        {
            direction = Vector3.back;
            move = true;
        }else if (Input.GetKeyDown(KeyCode.Q) && transform.position.x > -10 && !blockLeft)
        {
            direction = Vector3.left;
            move = true;
        }else if (Input.GetKeyDown(KeyCode.D) && transform.position.x < 10 && !blockRight)
        {
            direction = Vector3.right;
            move = true;
        }
        if (move)
        {
            this.transform.position += direction;
            move = false;
        }

        if (this.transform.position.z >= 2 && gameStart == false)
            gameStart = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("J'ai trigger un truc !");
        if (other.CompareTag("Moveable"))
            PartiePerdu();
        if (other.CompareTag("Static"))
        {
            if (transform.position.z < other.transform.position.z)
                blockForward = true;
            if (transform.position.z > other.transform.position.z)
                blockBack = true;    
            if (transform.position.x < other.transform.position.x)
                blockRight = true;
            if (transform.position.x > other.transform.position.x)
                blockLeft = true;  
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Static"))
        {
            if (transform.position.z < other.transform.position.z)
                blockForward = false;
            if (transform.position.z > other.transform.position.z)
                blockBack = false;
            if (transform.position.x < other.transform.position.x)
                blockRight = false;
            if (transform.position.x > other.transform.position.x)
                blockLeft = false;
        }
    }

    void PartiePerdu()
    {
        Debug.Log("Aie !");
    }
}
