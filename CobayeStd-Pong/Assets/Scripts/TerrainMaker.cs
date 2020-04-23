using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainMaker : MonoBehaviour
{
    public static Vector2 PingPosition;
    public static Vector2 PongPosition;

    public static Vector2 TerrainSize;
    public static Vector2 BarreSize;
    public static float MargeSize;
    public static float BallSize;


    [SerializeField]
    private GameObject playArea = null;
    [SerializeField]
    private GameObject ball = null;
    [SerializeField]
    private GameObject ping = null;
    [SerializeField]
    private GameObject pong = null;

    [SerializeField]
    private Vector2 targetAreaSize = new Vector2(50, 25);
    [SerializeField]
    private Vector2 targetBarreSize = new Vector2(1, 5);
    [SerializeField]
    private Vector2 targetMargeSize = new Vector2(5, 0);
    [SerializeField]
    private int targetBalleSize = 1;




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
        Vector2 demiTerrainSize = TerrainSize / 2;


        // Create area scene gameObject
        GameObject terrain = new GameObject("Terrain");        

        GameObject top = CreateWall("TopWall", new Vector2(TerrainSize.x, 1));
        top.transform.position = new Vector2(0, demiTerrainSize.y + 0.5f);
        top.transform.parent = terrain.transform;

        GameObject bottom = CreateWall("BottomWall", new Vector2(TerrainSize.x, 1));
        bottom.transform.position = new Vector2(0, -demiTerrainSize.y - 0.5f);
        bottom.transform.parent = terrain.transform;
        

        GameObject leftDetector = CreateGoalDetector("LeftDetector", new Vector2(1, TerrainSize.y));
        leftDetector.transform.position = new Vector2(-demiTerrainSize.x - 0.5f, 0);
        leftDetector.transform.parent = terrain.transform;

        GameObject rightDetector = CreateGoalDetector("RightDetector", new Vector2(1, TerrainSize.y));
        rightDetector.transform.position = new Vector2(demiTerrainSize.x + 0.5f, 0);
        rightDetector.transform.parent = terrain.transform;
        

        GameObject pA = Instantiate(playArea, terrain.transform);        
        pA.transform.localScale = new Vector3(TerrainSize.x / 2.56f, TerrainSize.y / 2.56f, 1);     // Image 256px * 256px, ratio 100px / 1 => 2.56 

        
        // Scale Ping and Pong
        ping.transform.localScale = new Vector3(BarreSize.x / 0.37f, BarreSize.y / 1.97f, 1);       // Image 37px * 197px, ratio 100px / 1
        pong.transform.localScale = new Vector3(BarreSize.x / 0.37f, BarreSize.y / 1.97f, 1);       // Image 37px * 197px, ratio 100px / 1
        if (ping.GetComponent<IAplayer>().isActiveAndEnabled)
            ping.GetComponent<IAplayer>().Init();
        if (pong.GetComponent<IAplayer>().isActiveAndEnabled)
            pong.GetComponent<IAplayer>().Init();



        // Position Ping and Pong
        PingPosition = ping.transform.position = new Vector2(-demiTerrainSize.x + MargeSize, 0);
        PongPosition = pong.transform.position = new Vector2(demiTerrainSize.x - MargeSize, 0);


        // Scale ball
        ball.transform.localScale = new Vector3(BallSize/100, BallSize/100, 1);             // Image 40px * 40px, ratio 100px / 1


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
        Vector2 screenSizePix = new Vector2(Screen.width, Screen.height);
        Vector2 usedScreenSizePix = screenSizePix;

        // Define UNIT PIX
        float screenRatio = screenSizePix.x / screenSizePix.y;
        float targetRatio = (targetAreaSize.x + 2 * targetBarreSize.x + 2 * targetMargeSize.x) / (targetAreaSize.y + 2 * targetMargeSize.y);

        if (screenRatio > targetRatio)
        {
            usedScreenSizePix.y = screenSizePix.y;
            usedScreenSizePix.x = usedScreenSizePix.y * targetRatio;
        }
        else if (screenRatio < targetRatio)
        {
            usedScreenSizePix.x = screenSizePix.x;
            usedScreenSizePix.y = usedScreenSizePix.x / targetRatio;
        }

        float unitPix = usedScreenSizePix.x / (targetAreaSize.x + 2 * targetBarreSize.x + 2 * targetMargeSize.x);

        Debug.Log("ScreenR=" + screenRatio + " / targetR=" + targetRatio + " / 1U=" + unitPix);


        // Define the AREA SIZE
        Vector2 areaSizePix = targetAreaSize * unitPix;
        Vector2 rightArea = Camera.main.ScreenToWorldPoint(new Vector2(areaSizePix.x, 0));
        Vector2 topArea = Camera.main.ScreenToWorldPoint(new Vector2(0, areaSizePix.y));
        TerrainSize = new Vector2(rightArea.x - bottomLeft.x, topArea.y - bottomLeft.y);


        // Define the BARRE SIZE
        Vector2 barreSizePix = targetBarreSize * unitPix;
        Vector2 rightBarre = Camera.main.ScreenToWorldPoint(new Vector2(barreSizePix.x, 0));
        Vector2 topBarre = Camera.main.ScreenToWorldPoint(new Vector2(0, barreSizePix.y));
        BarreSize = new Vector2(rightBarre.x - bottomLeft.x, topBarre.y - bottomLeft.y);


        // Define the MARGE SIZE
        float margeSizePix = targetMargeSize.x * unitPix;
        Vector2 rightMarge = Camera.main.ScreenToWorldPoint(new Vector2(margeSizePix, 0));
        MargeSize = rightMarge.x - bottomLeft.x;


        // Define the BALL SIZE
        //float balleSizePix = targetBalleSize * unitPix;
        //Vector2 rightBall = Camera.main.ScreenToWorldPoint(new Vector2(balleSizePix, 0));
        //BallSize = rightBall.x - bottomLeft.x;
        BallSize = targetBalleSize * unitPix;
    }

}
