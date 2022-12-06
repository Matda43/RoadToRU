using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    public int sizeMap = 40;
    public GameObject table;

    [SerializeField] public Part[] parts;

    public GameObject bird;

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

    void PlayBirdAnimation()
    {
        player.GetComponent<Player>().PartiePerdu();
        Instantiate(bird, player.transform.position + new Vector3(-0.25f,10,20), new Quaternion(0, 180, 0, 1));
    }

}
