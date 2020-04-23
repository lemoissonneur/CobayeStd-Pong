using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainMaker : MonoBehaviour
{
    public static Vector2 PingPosition;
    public static Vector2 PongPosition;

    [SerializeField]
    private GameObject playArea = null;
    [SerializeField]
    private GameObject ball = null;
    [SerializeField]
    private GameObject ping = null;
    [SerializeField]
    private GameObject pong = null;

    // all values are in virtual 'Display Unit'
    [SerializeField]
    private Vector2 targetAreaSize = new Vector2(50, 25);
    [SerializeField]
    private Vector2 targetBarreSize = new Vector2(1, 5);
    [SerializeField]
    private Vector2 targetMargeSize = new Vector2(5, 0);
    [SerializeField]
    private Vector2 targetBalleSize = new Vector2(1, 1);

    public static Vector2 usedScreenSize;
    public static Vector2 TerrainSize;
    public static Vector2 BarreSize;
    public static Vector2 MargeSize;
    public static Vector2 BallSize;




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
    }




    /**************************************************************************
    *                                 PUBLIC
    /**************************************************************************/
    public void MakeTerrain()
    {
        SizeGenerator();
        Vector2 halfUsedScreenSize = usedScreenSize / 2;

        // Create area scene gameObject
        GameObject terrain = new GameObject("Terrain");        

        GameObject top = CreateWall("TopWall", new Vector2(usedScreenSize.x, 10));
        top.transform.position = new Vector2(0, halfUsedScreenSize.y + 5f);
        top.transform.parent = terrain.transform;

        GameObject bottom = CreateWall("BottomWall", new Vector2(usedScreenSize.x, 10));
        bottom.transform.position = new Vector2(0, -halfUsedScreenSize.y - 5f);
        bottom.transform.parent = terrain.transform;
        
        GameObject leftDetector = CreateGoalDetector("LeftDetector", new Vector2(10, usedScreenSize.y));
        leftDetector.transform.position = new Vector2(-halfUsedScreenSize.x - 5f, 0);
        leftDetector.transform.parent = terrain.transform;

        GameObject rightDetector = CreateGoalDetector("RightDetector", new Vector2(10, usedScreenSize.y));
        rightDetector.transform.position = new Vector2(halfUsedScreenSize.x + 5f, 0);
        rightDetector.transform.parent = terrain.transform;

        Vector3 spriteSize;

        GameObject pA = Instantiate(playArea, terrain.transform);
        spriteSize = playArea.GetComponent<SpriteRenderer>().bounds.extents*2;
        pA.transform.localScale = usedScreenSize / spriteSize;

        // Scale Ping and Pong
        spriteSize = ping.GetComponent<SpriteRenderer>().bounds.extents * 2;

        ping.transform.localScale = BarreSize / spriteSize;
        pong.transform.localScale = BarreSize / spriteSize;
        if (ping.GetComponent<IAplayer>().isActiveAndEnabled)
            ping.GetComponent<IAplayer>().Init();
        if (pong.GetComponent<IAplayer>().isActiveAndEnabled)
            pong.GetComponent<IAplayer>().Init();

        // Position Ping and Pong
        PingPosition = ping.transform.position = new Vector2(-TerrainSize.x/2 + BarreSize.x/2, 0);
        PongPosition = pong.transform.position = new Vector2(TerrainSize.x/2 + BarreSize.x / 2, 0);

        // Scale ball
        spriteSize = ball.GetComponent<SpriteRenderer>().bounds.extents * 2;
        ball.transform.localScale = BallSize / spriteSize;


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

    private void SizeGenerator()
    {
        Vector2 bottomLeft = Camera.main.ScreenToWorldPoint(new Vector2(0, 0));

        // get screen definition in pixels and ratio
        Vector2 screenSizePix = new Vector2(Screen.width, Screen.height);
        float screenRatio = screenSizePix.x / screenSizePix.y;

        // process the screen ratio we want
        float targetRatio = (targetAreaSize.x + 2 * targetBarreSize.x + 2 * targetMargeSize.x) / (targetAreaSize.y + 2 * targetMargeSize.y);

        // the part of the screen we will actually use
        usedScreenSize = screenSizePix;

        if (screenRatio > targetRatio)
        {
            usedScreenSize.y = screenSizePix.y;  // we use the whole screen height
            usedScreenSize.x = usedScreenSize.y * targetRatio;
        }
        else if (screenRatio < targetRatio)
        {
            usedScreenSize.x = screenSizePix.x;  // we use the whole screen width
            usedScreenSize.y = usedScreenSize.x / targetRatio;
        }

        // Define the size of a Display Unit in pixels (pixels/DU) (maybe to cast as int ???)
        float unitPix = usedScreenSize.x / (targetAreaSize.x + 2 * targetBarreSize.x + 2 * targetMargeSize.x);

        Debug.Log("ScreenR=" + screenRatio + " / targetR=" + targetRatio + " / 1U=" + unitPix);


        // Define the AREA SIZE
        TerrainSize = targetAreaSize * unitPix;


        // Define the BARRE SIZE
        BarreSize = targetBarreSize * unitPix;


        // Define the MARGE SIZE
        MargeSize = targetMargeSize * unitPix;


        // Define the BALL SIZE
        BallSize = targetBalleSize * unitPix;
    }

}
