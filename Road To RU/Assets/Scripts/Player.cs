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
        if(other.CompareTag("Moveable"))
            Debug.Log("AIE");
        if (other.CompareTag("Static"))
        {
            Debug.Log("Entree d'object");
            if (transform.position.z < other.transform.position.z)
            {
                blockForward = true;
                Debug.Log("Bloqué devant");
            } 
            if (transform.position.z > other.transform.position.z)
            {
                blockBack = true;
                Debug.Log("Bloqué derriere");
            }
             
            if (transform.position.x < other.transform.position.x)
            {
                blockRight = true;
                Debug.Log("Bloqué a droite");
            }
            
            if (transform.position.x > other.transform.position.x)
            {
                blockLeft = true;
                Debug.Log("Bloqué a gauche");
            }
               
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Static"))
        {
            Debug.Log("Sortie d'objet");
            if (transform.position.z < other.transform.position.z)
            {
                blockForward = false;
                Debug.Log("Plus bloqué devant");
            }
            if (transform.position.z > other.transform.position.z)
            {
                blockBack = false;
                Debug.Log("Plus bloqué derriere");
            }
            if (transform.position.x < other.transform.position.x)
            {
                blockRight = false;
                Debug.Log("Plus bloqué a droite");
            }
            if (transform.position.x > other.transform.position.x)
            {
                blockLeft = false;
                Debug.Log("Plus bloqué a gauche");
            }
        }
    }
}
