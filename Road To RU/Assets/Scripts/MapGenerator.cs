using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator
{
    int sizeMap;

    Part[] parts;

    int indexPart = 0;
    int indexPlateform = -1;
    int nbPlateformToCreate = 0;

    Vector3 currentPosition;

    bool end = false;

    Vector3 start;

    List<GameObject> plateforms;

    public MapGenerator(int sizeMap, Part[] parts, GameObject player)
    {
        this.sizeMap = sizeMap;
        this.parts = parts;
        this.currentPosition = getPosition(player) + new Vector3(0,0,8);
        this.start = this.currentPosition;
        calculateRealWidth();
        initialisation();
    }

    Vector3 getPosition(GameObject player)
    {
        Vector3 position = player.transform.position;
        Vector3 scale = player.transform.localScale;
        return new Vector3(position.x, position.y - (scale.y / 2) - 0.25f, position.z);
    }

    void calculateRealWidth()
    {
        for(int i = 1; i < parts.Length; i++)
        {
            parts[i].width += parts[i - 1].width; 
        }
    }

    void initialisation()
    {
        plateforms = new List<GameObject>();
        for(int i = 0; i < sizeMap; i++)
        {
            addPlateform();
        }
    }

    public void movePlateform(Vector3 positionCamera)
    {
        if (anotherPlateformShouldBeAdded(positionCamera))
        {
            addPlateform();
            destroyFirstPlateform();
        }
    }

    void destroyFirstPlateform()
    {
        if(plateforms != null && plateforms.Count > 0)
        {
            GameObject.Destroy(plateforms[0]);
            plateforms.RemoveAt(0);
        }
    }

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
            nbPlateformToCreate = Random.Range(plateform.minOccurence, plateform.maxOccurence + 1);
        }
    }

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

                Debug.Log("C'est la fin !");
            }

            if (indexPart == 0 && indexPlateform == 0)
            {
                currentPosition.y -= plateform.transform.localScale.y;
            }
        }
    }


    public bool anotherPlateformShouldBeAdded(Vector3 positionCamera)
    {
        return (currentPosition.z - sizeMap < positionCamera.z - 5) && !end;
    }
}
