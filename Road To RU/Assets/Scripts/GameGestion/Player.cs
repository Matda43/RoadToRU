using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Class Player
/// </summary>
public class Player : MonoBehaviour
{
    // Initial position of the Player
    Vector3 spawn = Vector3.zero - new Vector3(0,0,6);

    // Player's direction
    Vector3 direction;

    // Boolean to indicate if the player in in movement
    bool move = false;

    // Boolean to indicate if there is an obstable forwrd the player
    bool blockForward = false;

    // Boolean to indicate if there is an obstable in the back of the player
    bool blockBack = false;

    // Boolean to indicate if there is an obstable at player's left
    bool blockLeft = false;

    // Boolean to indicate if there is an obstable at player's right
    bool blockRight = false;

    // Boolean to indicate if the Player is dead
    public bool isDead = false;

    // Boolean to indicate if the game is started
    public bool gameStart = false;

    // Boolean to indicate if the mode debug camera is active
    public bool debugModeCamera = true;

    void Avake()
    {
        direction = Vector3.zero;
        initialisation();
    }

    /// <summary>
    /// Initialise the position of the player with the spawn position
    /// </summary>
    public void initialisation()
    {
        this.transform.position = spawn;
    }

    // Update is called once per frame
    void Update()
    {
        //Remove the 2 first if
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("World");
        }
        if (Input.GetKeyDown(KeyCode.Z) && !blockForward && !isDead) {
            direction = Vector3.forward;
            transform.rotation = Quaternion.Euler(0, 0, 0);
            move = true;
        }else if (Input.GetKeyDown(KeyCode.S) && transform.position.z > -2 && !blockBack && !isDead)
        {
            direction = Vector3.back;
            transform.rotation = Quaternion.Euler(0, 180, 0);
            move = true;
        }else if (Input.GetKeyDown(KeyCode.Q) && transform.position.x > -10 && !blockLeft && !isDead)
        {
            direction = Vector3.left;
            transform.rotation = Quaternion.Euler(0, 270, 0);
            move = true;
        }else if (Input.GetKeyDown(KeyCode.D) && transform.position.x < 10 && !blockRight && !isDead)
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

        if (this.transform.position.z >= 2 && this.transform.position.z < 5 && gameStart == false)
            gameStart = true;
    }

    //Called if there is a collision with a collider of another object
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Moveable"))
        {
            PartiePerdu();
            playerCrash();
        }
        if (other.CompareTag("Static"))
        {
            if (transform.position.z < other.transform.position.z - 0.4f)
                blockForward = true;
            if (transform.position.z > other.transform.position.z + 0.4f)
                blockBack = true;    
            if (transform.position.x < other.transform.position.x - 0.4f)
                blockRight = true;
            if (transform.position.x > other.transform.position.x + 0.4f)
                blockLeft = true;  
        }
        if (other.CompareTag("Arrivee"))
            PartieGagne();
    }

    // Check if the player go out of the collider of another object
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

    /// <summary>
    /// Check if the plate is active or no
    /// </summary>
    /// <returns>State of the Player's plate</returns>
    public bool getPlateActive()
    {
        return transform.GetChild(0).gameObject.activeSelf;
    }

    /// <summary>
    /// Check is the game is lost
    /// </summary>
    public void PartiePerdu()
    {
        gameStart = false;
        isDead = true;
    }

    /// <summary>
    /// Update the player's position
    /// </summary>
    void playerCrash()
    {
        transform.localScale = new Vector3(1, 0.1f, 1);
    }

    /// <summary>
    /// Set the parameter gameStart to false
    /// </summary>
    void PartieGagne()
    {
        
        gameStart = false;
        Debug.Log("U win !");
        //Si appuie sur bouton mais en attendant : 
        SceneManager.LoadScene("World");
    }
}
