using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    public int sizeMap = 40;

    [SerializeField] public Part[] parts;

    GameObject goPlayer;
    Player player;
    new Camera camera;

    MapGenerator mapGenerator;

    // Start is called before the first frame update
    void Start()
    {
        goPlayer = GameObject.Find("Player");
        player = goPlayer.GetComponent<Player>();
        camera = Camera.main;

        mapGenerator = new MapGenerator(sizeMap, parts, goPlayer);
    }

    // Update is called once per frame
    void Update()
    {
        mapGenerator.movePlateform(camera.transform.position);

        int roadMapSize = 7 + parts[2].width;
        if (player.transform.position.z >= roadMapSize && !player.getPlateActive())
            player.activePlate();

        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(camera);
        if (!GeometryUtility.TestPlanesAABB(planes, player.GetComponent<BoxCollider>().bounds))
            Debug.Log("EH TA DISPARU !");
    }

}
