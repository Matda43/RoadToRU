using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using TMPro;

/// <summary>
/// Class Player
/// </summary>
public class Player : MonoBehaviour
{
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

    // Boolean to indicate to the UI that the player is dead and all animation are ended
    public bool CanRetry = false;

    // Boolean to indicate if the game is started
    public bool gameStart = false;

    // Boolean to indicate if the mode debug camera is active
    public bool debugModeCamera = true;

    // Ui to the end of the game
    public GameObject panelUI;

    // Text display at the end of the game
    public string textFin = "Textfin";

    // Update is called once per frame
    void Update()
    {
        if (CanRetry)
        {
            panelUI.transform.position = new Vector3(panelUI.transform.position.x, Screen.height + 100, panelUI.transform.position.z);
            panelUI.SetActive(true);
            CanRetry = false;
            panelUI.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = textFin;
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
        if (other.CompareTag("Moveable") && gameStart)
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
    /// Lose of the player in case of a crash with something
    /// </summary>
    void playerCrash()
    {
        transform.localScale = new Vector3(1, 0.1f, 1);
        CanRetry = true;
        textFin = "You got knocked down !";
    }

    /// <summary>
    /// Set the parameter gameStart to false
    /// </summary>
    void PartieGagne()
    {
        textFin = "You came to the table !";
        CanRetry = true;
        gameStart = false;
    }
}
