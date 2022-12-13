using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// Class Plateform
/// </summary>
public class Plateform : MonoBehaviour
{
    // Minimum number of occurence of a plateform's type that you can found in the game 
    public int minOccurence;

    // Maximum number of occurence of a plateform's type that you can found in the game 
    public int maxOccurence;

    // Time to make the separation of the instanciation of the different gameObjects
    public float MAX_TIME_LEFT = 5;

    // Elapsed time since last creation
    float timeLeft;

    // Possible direction of the movable elements
    public enum Direction { Left, Right, Both, Top };

    // Possible direction of the movable elements
    public Direction direction;

    // Table composed of movable elements
    public GameObject[] movableElements;

    // Table composed of static elements
    public GameObject[] staticElements;

    // Minimum number of objects located on specific plateform 
    public int minObject;

    // Maximum number of objects located on specific plateform 
    public int maxObjet;

    // Possible position on a plateform to place an object
    List<int> plateformPosition = new List<int>();

    // List of movable gameObjects
    List<GameObject> movables;

    void Awake()
    {
        if(minOccurence < 0)
        {
            minOccurence = 0;
        }
        if (maxOccurence < minOccurence)
        {
            maxOccurence = minOccurence;
        }

        if(direction == Direction.Both)
        {
            float random = Random.value;
            direction = random < 0.5 ? Direction.Left : Direction.Right;
        }

        movables = new List<GameObject>();
        FillList();
        initStaticObject();

        timeLeft = Random.Range(MAX_TIME_LEFT - 3, MAX_TIME_LEFT);
    }

    // Update is called once per frame
    void Update()
    {
        updateTime(Time.deltaTime);
        int i = 0;
        while(i < movables.Count)
        {
            Movable movable = movables[i].GetComponent<Movable>();
            if (movable.shouldBeDestroyedByParent())
            {
                Destroy(movables[i]);
                movables.RemoveAt(i);
            }
            else
            {
                i++;
            }
        }
    }

    /// <summary>
    /// Updates the elapsed time since last creation of an object
    /// </summary>
    /// <param name="timePassed">elapsed time since last frame</param>
    public void updateTime(float timePassed)
    {
        timeLeft -= timePassed;
        if (timeLeft <= 0)
        {
            instanciate();
            timeLeft = Random.Range(MAX_TIME_LEFT - 3, MAX_TIME_LEFT);
        }
    }

    /// <summary>
    /// Instanciates the static gameObjects and initializes their position
    /// </summary>
    void initStaticObject()
    {
        int length = staticElements.Length;
        if (length > 0)
        {
            int nbStaticObject = Random.Range(minObject, maxObjet+1);
            for(int i = 0; i < nbStaticObject; i++)
            {
                int randomObject = Random.Range(0, length);
                Vector3 positionObject = new Vector3(GetNonRepeatRandom(), 0.25f, 0) + transform.position;
                GameObject go = Instantiate(staticElements[randomObject], positionObject, Quaternion.identity);
                go.transform.position = new Vector3(go.transform.position.x, go.transform.position.y + staticElements[randomObject].transform.position.y, go.transform.position.z);
                go.transform.parent = this.transform;
            }
        }
    }

    /// <summary>
    /// Instanciates the movalbe gameObjects and initializes their position
    /// </summary>
    void instanciate()
    {
        int length = movableElements.Length;
        if (length > 0)
        {
            int random = Random.Range(0, length);
            GameObject go = Instantiate(movableElements[random], this.transform.position + Vector3.up * 0.5f, Quaternion.identity);
            go.transform.position = new Vector3(go.transform.position.x, go.transform.position.y + movableElements[random].transform.position.y, go.transform.position.z);
            go.transform.parent = this.transform;

            Movable movable = go.GetComponent<Movable>();
            switch (direction) {
                case Direction.Left:
                    go.transform.Rotate(new Vector3(0,180,0));
                    movable.setDirection(Vector3.left, go.transform.position, transform.localScale);
                    break;
                case Direction.Right:
                    movable.setDirection(Vector3.right, go.transform.position, transform.localScale);
                    break;
                default:
                    break;
            }
            movables.Add(go);
        }
    }

    /// <summary>
    /// Determines all possible position to instanciate an object in a plateform
    /// </summary>
    void FillList()
    {
        int sizePlatform = (int)transform.localScale.x;
        for (int i = (-sizePlatform/2)+1; i < sizePlatform/2; i++)
        {
            plateformPosition.Add(i);
        }
    }

    /// <summary>
    /// Decrease the number of possible position determined before
    /// </summary>
    /// <returns>The position to delete in the plateform</returns>
    int GetNonRepeatRandom()
    {
        if (plateformPosition.Count == 0)
        {
            return -1; // Maybe you want to refill
        }
        int rand = Random.Range(0, plateformPosition.Count);
        int value = plateformPosition[rand];
        plateformPosition.RemoveAt(rand);
        return value;
    }
}
