using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpGenerator : MonoBehaviour
{
    // generator setup
    public float generationDelaySec = 5f;
    public Vector2Int minAreaBoundariesDU;
    public Vector2Int maxAreaBoundariesDU;
    public List<GameObject> powerUps;   // list of available powerups

    private bool IsActive = false;
    private float lastTime = 0f;        // last time we generated a powerup
    private Vector2 minAreaBoundaries;
    private Vector2 maxAreaBoundaries;



    // Start is called before the first frame update
    void Start()
    {

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

        //Debug.Log("PowerUpGenerator.generate() "+ selectedpower + "@ "+powerUpPosition);

        // generate power up from prefab (TODO)
        newPowerUp = Instantiate(powerUps[selectedpower], this.transform);
        newPowerUp.transform.position = powerUpPosition;

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
        KillAllChildrens();
        TranslateAreaBoundaries();
        lastTime = Time.time;
        IsActive = true;
    }

    public void StopGenerator()
    {
        KillAllChildrens();
        IsActive = false;
    }

    private void KillAllChildrens()
    {
        foreach (Transform child in this.transform)
        {
            GameObject trash = child.gameObject;
            PowerUp trashPowerUp = trash.GetComponent<PowerUp>();

            // if triggered, reverse it's effect
            if (trashPowerUp.Triggered)
                trashPowerUp.RevertEffect();

            // kill it
            Destroy(trash);
        }
    }

    private void TranslateAreaBoundaries()
    {
        minAreaBoundaries = minAreaBoundariesDU * TerrainMaker.PixelsPerDU;
        maxAreaBoundaries = maxAreaBoundariesDU * TerrainMaker.PixelsPerDU;
    }

}
