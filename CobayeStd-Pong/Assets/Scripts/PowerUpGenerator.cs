using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpGenerator : MonoBehaviour
{
    // generator setup
    public float generationDelaySec = 5f;
    public Vector2 minAreaBoundaries;
    public Vector2 maxAreaBoundaries;

    private bool IsActive = false;
    private float lastTime = 0f;                                        // last time we generated a powerup
    private int powerUpCpt = 0;                                         // total number of powerup generated
    public List<GameObject> powerUps;                                   // list of available powerups
    private List<GameObject> currentPowerUps = new List<GameObject>();  // list of currently active powerups
    

    // Start is called before the first frame update
    void Start()
    {
        StartGenerator();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsActive && (Time.time - lastTime) > generationDelaySec)
        {
            lastTime = Time.time;
            generate();
        }
    }

    public GameObject generate()
    {
        GameObject newPowerUp;

        // generate random position
        Vector3 powerUpPosition = generateRandomPosition();

        // roll a dice to select the power in the list
        int selectedpower = generatePowerUpNumber();

        Debug.Log("PowerUpGenerator.generate() "+ selectedpower + "@ "+powerUpPosition);

        // generate power up from prefab (TODO)
        newPowerUp = Instantiate(powerUps[selectedpower], this.transform);
        newPowerUp.transform.position = powerUpPosition;

        // add it to the current list
        currentPowerUps.Add(newPowerUp);
        powerUpCpt++;

        return newPowerUp;
    }

    public Vector3 generateRandomPosition()
    {
        Vector3 position = Vector3.zero;
        position.x = Random.Range(minAreaBoundaries.x, maxAreaBoundaries.x);
        position.y = Random.Range(minAreaBoundaries.y, maxAreaBoundaries.y);
        return position;
    }

    public int generatePowerUpNumber()
    {
        return Random.Range(1, powerUps.Count);
    }

    public void StartGenerator()
    {
        StopGenerator();
        lastTime = Time.time;
        currentPowerUps = new List<GameObject>();
        IsActive = true;
    }

    public void StopGenerator()
    {
        IsActive = false;
        Debug.Log(currentPowerUps);
        while (currentPowerUps.Count > 0)
        {
            GameObject trash = currentPowerUps[0];
            currentPowerUps.RemoveAt(0);
            Destroy(trash);
        }
    }

}
