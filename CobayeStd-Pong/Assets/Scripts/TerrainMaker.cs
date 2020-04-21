using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainMaker : MonoBehaviour
{
    public static Vector2 bottomLeft;
    public static Vector2 topRight;
    public static float terrainHeight;
    public static float terrainWidth;


    [SerializeField]
    private GameObject playArea;



    private static TerrainMaker instance;
    public static TerrainMaker Instance
    {
        get { return instance; }
    }

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;


        // Convert screen's pixel coordinate into game's coordinate
        bottomLeft = Camera.main.ScreenToWorldPoint(new Vector2(0, 0));
        topRight = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

        // Calcul the width and the height of the terrain 
        terrainHeight = topRight.y - bottomLeft.y;  Debug.Log("Terrain height : " + terrainHeight);
        terrainWidth = topRight.x - bottomLeft.x;   Debug.Log("Terrain width : " + terrainWidth);
    }

    /**************************************************************************
    *                                 PUBLIC
    /**************************************************************************/
    public void MakeTerrain()
    {
        GameObject terrain = new GameObject("Terrain");

        GameObject top = CreateWall("TopWall", new Vector2(terrainWidth, 2));
        top.transform.position = new Vector2(0, topRight.y + 1);
        top.transform.parent = terrain.transform;

        GameObject bottom = CreateWall("BottomWall", new Vector2(terrainWidth, 2));
        bottom.transform.position = new Vector2(0, bottomLeft.y - 1);
        bottom.transform.parent = terrain.transform;


        GameObject leftDetector = CreateGoalDetector("LeftDetector", new Vector2(2, terrainHeight));
        leftDetector.transform.position = new Vector2(bottomLeft.x - 1, 0);
        leftDetector.transform.parent = terrain.transform;

        GameObject rightDetector = CreateGoalDetector("RightDetector", new Vector2(2, terrainHeight));
        rightDetector.transform.position = new Vector2(topRight.x + 1, 0);
        rightDetector.transform.parent = terrain.transform;


        GameObject pA = Instantiate(playArea, terrain.transform);
        pA.transform.localScale = new Vector3(terrainWidth, terrainHeight, 1);
    }




    /**************************************************************************
    *                                 PRIVATE
    /**************************************************************************/
    private GameObject CreateWall(string name, Vector2 size)
    {
        GameObject wall = new GameObject(name);

        Rigidbody2D rg2D = wall.AddComponent<Rigidbody2D>();
        rg2D.bodyType = RigidbodyType2D.Static;
        rg2D.simulated = true;

        BoxCollider2D bc2D = wall.AddComponent<BoxCollider2D>();
        bc2D.size = size;

        return wall;
    }

    private GameObject CreateGoalDetector(string name, Vector2 size)
    {
        GameObject detector = new GameObject(name);

        Rigidbody2D rg2D = detector.AddComponent<Rigidbody2D>();
        rg2D.bodyType = RigidbodyType2D.Static;
        rg2D.simulated = true;

        BoxCollider2D bc2D = detector.AddComponent<BoxCollider2D>();
        bc2D.isTrigger = true;
        bc2D.size = size;

        detector.AddComponent<GoalDetector>();

        return detector;
    }

}
