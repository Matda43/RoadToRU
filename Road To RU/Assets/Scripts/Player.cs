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
            transform.rotation = Quaternion.Euler(0, 0, 0);
            move = true;
        }else if (Input.GetKeyDown(KeyCode.S) && transform.position.z > -2 && !blockBack)
        {
            direction = Vector3.back;
            transform.rotation = Quaternion.Euler(0, 180, 0);
            move = true;
        }else if (Input.GetKeyDown(KeyCode.Q) && transform.position.x > -10 && !blockLeft)
        {
            direction = Vector3.left;
            transform.rotation = Quaternion.Euler(0, 270, 0);
            move = true;
        }else if (Input.GetKeyDown(KeyCode.D) && transform.position.x < 10 && !blockRight)
        {
            direction = Vector3.right;
            transform.rotation = Quaternion.Euler(0, 90, 0);
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
        if (other.CompareTag("Moveable"))
            PartiePerdu();
        if (other.CompareTag("Static"))
        {
            if (transform.position.z < other.transform.position.z - 0.5f)
                blockForward = true;
            if (transform.position.z > other.transform.position.z + 0.5f)
                blockBack = true;    
            if (transform.position.x < other.transform.position.x - 0.5f)
                blockRight = true;
            if (transform.position.x > other.transform.position.x + 0.5f)
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

    public void activePlate()
    {
        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetComponent<BoxCollider>().center = new Vector3(0, 0, 0.15f);
        transform.GetComponent<BoxCollider>().size = new Vector3(0.9f, 0.9f, 1.1f);
    }

    public bool getPlateActive()
    {
        return transform.GetChild(0).gameObject.activeSelf;
    }

    void PartiePerdu()
    {
        Debug.Log("Aie !");
    }

    void PartieGagne()
    {
        Debug.Log("U win !");
    }
}
