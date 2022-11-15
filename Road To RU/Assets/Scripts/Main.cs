using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    public int sizeMap = 40;

    [SerializeField] public Part[] parts;

    GameObject player;
    new Camera camera;

    MapGenerator mapGenerator;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        camera = Camera.main;

        mapGenerator = new MapGenerator(sizeMap, parts, player);
    }

    // Update is called once per frame
    void Update()
    {
        if (mapGenerator.anotherPlateformShouldBeAdded(camera.transform.position))
        {
            mapGenerator.movePlateform();
        }
    }
}
