using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainMaker : MonoBehaviour
{
    [SerializeField]
    private GameObject playArea = null;
    [SerializeField]
    private GameObject ball = null;
    [SerializeField]
    private GameObject ping = null;
    [SerializeField]
    private GameObject pong = null;

    // toutes les valeurs en DU 'Display Unit'
    [SerializeField]
    private Vector2 targetAreaSizeDU = new Vector2(50, 25);
    [SerializeField]
    private Vector2 targetBarreSizeDU = new Vector2(1, 5);
    [SerializeField]
    private Vector2 targetMargeSizeDU = new Vector2(4, 0);
    [SerializeField]
    private Vector2 targetBalleSizeDU = new Vector2(1, 1);


    public static Vector2 PingPosition;
    public static Vector2 PongPosition;

    public static Vector2 UsedScreenSizePix;
    public static Vector2 TargetAreaSizePix;     // play area wanted size in pixels
    public static Vector2 TargetBarreSizePix;    // barre wanted size in pixels
    public static Vector2 TargetMargeSizePix;    // marge wanted size in pixels
    public static Vector2 TargetBallSizePix;     // balle wanted size in pixels
    public static int PixelsPerDU;                  // pixels per Display Units




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
        Vector2 halfUsedScreenSize = UsedScreenSizePix / 2;

        // Create area scene gameObject
        GameObject terrain = new GameObject("Terrain");        

        GameObject top = CreateWall("TopWall", new Vector2(UsedScreenSizePix.x, 10));
        top.transform.position = new Vector2(0, halfUsedScreenSize.y + 5f);
        top.transform.parent = terrain.transform;

        GameObject bottom = CreateWall("BottomWall", new Vector2(UsedScreenSizePix.x, 10));
        bottom.transform.position = new Vector2(0, -halfUsedScreenSize.y - 5f);
        bottom.transform.parent = terrain.transform;
        
        GameObject leftDetector = CreateGoalDetector("LeftDetector", new Vector2(10, UsedScreenSizePix.y));
        leftDetector.transform.position = new Vector2(-halfUsedScreenSize.x - 5f, 0);
        leftDetector.transform.parent = terrain.transform;

        GameObject rightDetector = CreateGoalDetector("RightDetector", new Vector2(10, UsedScreenSizePix.y));
        rightDetector.transform.position = new Vector2(halfUsedScreenSize.x + 5f, 0);
        rightDetector.transform.parent = terrain.transform;

        Vector3 spriteSize;

        GameObject pA = Instantiate(playArea, terrain.transform);
        spriteSize = playArea.GetComponent<SpriteRenderer>().bounds.extents*2;
        pA.transform.localScale = UsedScreenSizePix / spriteSize;

        // Scale Ping and Pong
        spriteSize = ping.GetComponent<SpriteRenderer>().bounds.extents * 2;

        ping.transform.localScale = TargetBarreSizePix / spriteSize;
        pong.transform.localScale = TargetBarreSizePix / spriteSize;

        // Position Ping and Pong
        PingPosition = ping.transform.position = new Vector2(-TargetAreaSizePix.x/2 - TargetBarreSizePix.x/2, 0);
        PongPosition = pong.transform.position = new Vector2(TargetAreaSizePix.x/2 + TargetBarreSizePix.x / 2, 0);

        // Scale ball
        spriteSize = ball.GetComponent<SpriteRenderer>().bounds.extents * 2;
        ball.transform.localScale = TargetBallSizePix / spriteSize;


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
        float targetRatio = (targetAreaSizeDU.x + 2 * targetBarreSizeDU.x + 2 * targetMargeSizeDU.x) / (targetAreaSizeDU.y + 2 * targetMargeSizeDU.y);

        // the part of the screen we will actually use
        UsedScreenSizePix = screenSizePix;

        if (screenRatio > targetRatio)
        {
            UsedScreenSizePix.y = screenSizePix.y;  // we use the whole screen height
            UsedScreenSizePix.x = UsedScreenSizePix.y * targetRatio;
        }
        else if (screenRatio < targetRatio)
        {
            UsedScreenSizePix.x = screenSizePix.x;  // we use the whole screen width
            UsedScreenSizePix.y = UsedScreenSizePix.x / targetRatio;
        }

        // Define the size of a Display Unit in pixels (pixels/DU) (maybe to cast as int ???)
        PixelsPerDU = (int)Mathf.Floor(UsedScreenSizePix.x / (targetAreaSizeDU.x + 2 * targetBarreSizeDU.x + 2 * targetMargeSizeDU.x));

        Debug.Log("ScreenR=" + screenRatio + " / targetR=" + targetRatio + " / 1DU=" + PixelsPerDU);


        // Define the AREA SIZE
        TargetAreaSizePix = targetAreaSizeDU * PixelsPerDU;

        // Define the BARRE SIZE
        TargetBarreSizePix = targetBarreSizeDU * PixelsPerDU;

        // Define the MARGE SIZE
        TargetMargeSizePix = targetMargeSizeDU * PixelsPerDU;

        // Define the BALL SIZE
        TargetBallSizePix = targetBalleSizeDU * PixelsPerDU;
    }

}
