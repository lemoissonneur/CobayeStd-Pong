using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;


namespace Tests
{
    public class T001_PuGeneratorTest
    {
        int PowerUpsToGenerate = 1000;
        int PowerUpsNumbers = 6;
        float tolerance = 0.0f;
        int Minprobability;

        // A Test behaves as an ordinary method
        [SetUp]
        public void T001_Setup()
        {
            Minprobability = Mathf.FloorToInt(tolerance * PowerUpsToGenerate / PowerUpsNumbers);
        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator T001_TestNumberGenerator()
        {
            SceneManager.LoadScene("PowerUpGeneratorTestScene");
            yield return null;

            PowerUpGenerator PuGenerator = GameObject.Find("PowerUpGenerator").GetComponent<PowerUpGenerator>();

            PuGenerator.minAreaBoundaries = new Vector2Int(-100, -100);
            PuGenerator.maxAreaBoundaries = new Vector2Int(100, 100);

            Assert.AreEqual(PuGenerator.powerUps.Count, PowerUpsNumbers);

            int[] numberGenerationResults = new int[PowerUpsNumbers];

            for (int i = 1; i <= PowerUpsToGenerate; i++)
            {
                int res = PuGenerator.GeneratePowerUpNumber();
                numberGenerationResults[res]++;
            }

            foreach (int v in numberGenerationResults) Debug.Log(v);

            for (int i = 0; i < PowerUpsNumbers; i++)
                Assert.GreaterOrEqual(numberGenerationResults[i], Minprobability);
        }
    }
}