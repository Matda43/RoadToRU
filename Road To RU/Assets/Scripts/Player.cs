using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Player : MonoBehaviour
{
    Vector3 spawn = Vector3.zero - new Vector3(0,0,6);
    Vector3 direction;
    bool move = false;

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
        if (Input.GetKeyDown(KeyCode.Z)) {
            direction = Vector3.forward;
            move = true;
        }else if (Input.GetKeyDown(KeyCode.S))
        {
            direction = Vector3.back;
            move = true;
        }else if (Input.GetKeyDown(KeyCode.Q))
        {
            direction = Vector3.left;
            move = true;
        }else if (Input.GetKeyDown(KeyCode.D))
        {
            direction = Vector3.right;
            move = true;
        }
        if (move)
        {
            this.transform.position += direction;
            move = false;
        }
    }
}
