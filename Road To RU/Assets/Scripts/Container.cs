using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Container : MonoBehaviour
{
    GameObject[] movableElements;
    List<GameObject> instanciated;

    // Start is called before the first frame update
    void Start()
    {
        instanciated = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setMovableElements(GameObject[] new_movableElements)
    {
        this.movableElements = new_movableElements;
    }

    public void instanciate()
    {
        if (movableElements.Length > 0)
        {
            GameObject child = GameObject.Instantiate(movableElements[Random.Range(0, movableElements.Length)], new Vector3(15, transform.position.y, transform.position.z), Quaternion.identity);
            Movable movable = child.GetComponent<Movable>();
            movable.setDirection(Vector3.left);
            instanciated.Add(child);
        }
    }


    public void destroyChilds()
    {
        foreach(GameObject go in instanciated)
        {
            Destroy(go);
        }
        instanciated.Clear();
    }
}
