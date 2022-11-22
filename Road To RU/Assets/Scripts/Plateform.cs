using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Plateform : MonoBehaviour
{
    public int minOccurence;
    public int maxOccurence;

    public float MAX_TIME_LEFT = 5;
    float timeLeft;
    public enum Direction { Left, Right, Both, Top };
    public Direction direction;

    public GameObject[] movableElements;
    public GameObject[] staticElements;

    List<int> plateformPosition = new List<int>();

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

    public void updateTime(float timePassed)
    {
        timeLeft -= timePassed;
        if (timeLeft <= 0)
        {
            instanciate();
            timeLeft = Random.Range(MAX_TIME_LEFT - 3, MAX_TIME_LEFT);
        }
    }

    void initStaticObject()
    {
        int length = staticElements.Length;
        if (length > 0)
        {
            int nbStaticObject = Random.Range(1, 12);
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

    void instanciate()
    {
        int length = movableElements.Length;
        if (length > 0)
        {
            int random = Random.Range(0, length);
            GameObject go = Instantiate(movableElements[random], this.transform.position + Vector3.up * 0.5f, Quaternion.identity);
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

    void FillList()
    {
        int sizePlatform = (int)transform.localScale.x;
        for (int i = (-sizePlatform/2)+1; i < sizePlatform/2; i++)
        {
            plateformPosition.Add(i);
        }
    }

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
