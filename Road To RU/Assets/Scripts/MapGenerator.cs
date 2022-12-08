using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class MapGenerator
/// </summary>
public class MapGenerator
{
    // Size of the map during the game
    int sizeMap;

    // Parts table : correspond to the different world in which the player run
    Part[] parts;

    // Index of the 1st part
    int indexPart = 0;

    // Initialisation by default of the number of the plateform created (a plateform take a width of 1 bloc) 
    int indexPlateform = -1;

    // Number of the plateform to create
    int nbPlateformToCreate = 0;

    // Current position of the plateform created
    Vector3 currentPosition;

    // Boolean to indicate if it is the end of the game
    bool end = false;

    // Position of the first plateform
    Vector3 start;

    // List of plateforms gameobject instantiate
    List<GameObject> plateforms;

    // Table
    GameObject table;
    
    // Boolean indicated if the table is added at the good position in the game 
    bool tableadded = false;

    /// <summary>
    /// Constructor of the class MapGenerator
    /// </summary>
    /// <param name="sizeMap">Size of the map</param>
    /// <param name="parts">Parts table necessary to fix the rules of the game</param>
    /// <param name="player">Player</param>
    /// <param name="table">Table</param>
    public MapGenerator(int sizeMap, Part[] parts, GameObject player, GameObject table)
    {
        this.sizeMap = sizeMap;
        this.parts = parts;
        this.table = table;
        this.currentPosition = getPosition(player) + new Vector3(0,0,8);
        this.start = this.currentPosition;
        calculateRealWidth();
        initialisation();
    }

    /// <summary>
    /// Get the position in the world according to the player's position
    /// </summary>
    /// <param name="player">Player</param>
    /// <returns>New position calculate</returns>
    Vector3 getPosition(GameObject player)
    {
        Vector3 position = player.transform.position;
        Vector3 scale = player.transform.localScale;
        return new Vector3(position.x, position.y - (scale.y / 2) - 0.25f, position.z);
    }

    /// <summary>
    ///  Calcul the real width of the plateform depending on the other plateforms to be created
    /// </summary>
    void calculateRealWidth()
    {
        for(int i = 1; i < parts.Length; i++)
        {
            parts[i].width += parts[i - 1].width; 
        }
    }

    /// <summary>
    /// Create the plateforms indicated by the parameter sizeMap
    /// </summary>
    void initialisation()
    {
        plateforms = new List<GameObject>();
        for(int i = 0; i < sizeMap; i++)
        {
            addPlateform();
        }
    }

    /// <summary>
    /// Allows to create next plateform according to the position of the camera and in the same time to destroy hidden plateform to far
    /// </summary>
    /// <param name="positionCamera">Current position of the camera</param>
    public void movePlateform(Vector3 positionCamera)
    {
        if (anotherPlateformShouldBeAdded(positionCamera))
        {
            addPlateform();
            destroyFirstPlateform();
        }
    }

    /// <summary>
    /// Destroy the older plateform
    /// </summary>
    void destroyFirstPlateform()
    {
        if(plateforms != null && plateforms.Count > 0)
        {
            GameObject.Destroy(plateforms[0]);
            plateforms.RemoveAt(0);
        }
    }

    /// <summary>
    /// Check the plateform to be created according to the number of the current part in which the player it is located
    /// </summary>
    void chooseAPlateform()
    {
        if (nbPlateformToCreate == 0)
        {
            int index = Random.Range(0, parts[indexPart].plateforms.Length);
            if (parts[indexPart].plateforms.Length > 1)
            {
                while (index == indexPlateform)
                {
                    index = Random.Range(0, parts[indexPart].plateforms.Length);
                }
            }
            indexPlateform = index;
            Plateform plateform = parts[indexPart].plateforms[indexPlateform].GetComponent<Plateform>();
            nbPlateformToCreate = Random.Range(plateform.minOccurence, plateform.maxOccurence);
        }
    }

    /// <summary>
    /// Instanciate a new plateform in the game while it is not the end of the game
    /// </summary>
    void addPlateform()
    {        
        if (!end)
        {
            chooseAPlateform();
            GameObject plateform = GameObject.Instantiate(parts[indexPart].plateforms[indexPlateform], currentPosition, Quaternion.identity);
            plateforms.Add(plateform);

            nbPlateformToCreate--;
            currentPosition.z += plateform.transform.localScale.z;
            
            if (start.z + parts[indexPart].width == currentPosition.z)
            {
                indexPart++;
                nbPlateformToCreate = 0;
                end = indexPart == parts.Length;

                if(indexPart + 1 == parts.Length && !tableadded)
                {
                    table.SetActive(true);
                    table.transform.position = currentPosition;
                    tableadded = true;
                }
            }

            if (indexPart == 0 && indexPlateform == 0)
            {
                currentPosition.y -= plateform.transform.localScale.y;
            }
        }
    }

    /// <summary>
    /// Check if a plateform should be instanciate and added in the world according to the position of the camera 
    /// </summary>
    /// <param name="positionCamera">Position of the camera</param>
    /// <returns>True if the current position is too near of the camera's position</returns>
    public bool anotherPlateformShouldBeAdded(Vector3 positionCamera)
    {
        return (currentPosition.z - sizeMap < positionCamera.z - 10) && !end;
    }
}
