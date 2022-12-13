using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class Main
/// </summary>
public class Main : MonoBehaviour
{
    // Initial size of the map at the beginning of the game
    public int sizeMap = 40;

    // Table
    public GameObject table;

    // Parts table 
    [SerializeField] public Part[] parts;

    // Condor
    public GameObject bird;

    // Player gameObject
    GameObject goPlayer;

    // Player class
    Player player;

    // Main camera
    new Camera camera;

    // Generator of the map
    MapGenerator mapGenerator;

    // Start is called before the first frame update
    void Start()
    {
        goPlayer = GameObject.Find("Player");
        player = goPlayer.GetComponent<Player>();
        camera = Camera.main;

        mapGenerator = new MapGenerator(sizeMap, parts, goPlayer, table);
    }

    // Update is called once per frame
    void Update()
    {
        mapGenerator.movePlateform(camera.transform.position);

        int roadMapSize = 7 + parts[2].width;
        if (goPlayer.transform.position.z >= roadMapSize && !player.getPlateActive())
            goPlayer.transform.GetChild(0).gameObject.SetActive(true);

        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(camera);
        if (!GeometryUtility.TestPlanesAABB(planes, player.GetComponent<BoxCollider>().bounds) && !player.GetComponent<Player>().isDead)
            PlayBirdAnimation();
    }

    /// <summary>
    /// Run the animation of the condor when the player go out of the screen
    /// </summary>
    void PlayBirdAnimation()
    {
        player.GetComponent<Player>().PartiePerdu();
        Instantiate(bird, player.transform.position + new Vector3(-0.25f,10,20), new Quaternion(0, 180, 0, 1));
    }
}
