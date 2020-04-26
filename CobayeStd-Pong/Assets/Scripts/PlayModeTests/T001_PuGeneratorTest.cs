using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;


namespace Tests
{
    public class T001_PuGeneratorTest
    {
        // A Test behaves as an ordinary method
        [Test]
        public void T001_createPuGenerator()
        {
            int PowerUpsToGenerate = 100;
            int PowerUpsNumbers = 6;
            int Minprobability = Mathf.FloorToInt(0.8f * PowerUpsToGenerate / PowerUpsNumbers); // 20%



            TerrainMaker field = new TerrainMaker();
            GameObject g0 = new GameObject();
            PowerUpGenerator PuGenerator = g0.AddComponent<PowerUpGenerator>();

            PuGenerator.minAreaBoundariesDU = new Vector2Int(-25,-12);
            PuGenerator.maxAreaBoundariesDU = new Vector2Int(25,12);

            PuGenerator.powerUps = new List<GameObject>(PowerUpsNumbers);

            GameObject g1 = new GameObject("BonusTailleBarre");
            g1.AddComponent<BonusTailleBarre>();
            PuGenerator.powerUps.Add(g1);

            GameObject g2 = new GameObject("MalusTailleBarre");
            g2.AddComponent<MalusTailleBarre>();
            PuGenerator.powerUps.Add(g2);

            GameObject g3 = new GameObject("BonusVitesseBarre");
            g3.AddComponent<BonusVitesseBarre>();
            PuGenerator.powerUps.Add(g3);

            GameObject g4 = new GameObject("MalusVitesseBarre");
            g4.AddComponent<MalusVitesseBarre>();
            PuGenerator.powerUps.Add(g4);

            GameObject g5 = new GameObject("VitesseBalleUp");
            g5.AddComponent<VitesseBalleUp>();
            PuGenerator.powerUps.Add(g5);

            GameObject g6 = new GameObject("VitesseBalleDown");
            g6.AddComponent<VitesseBalleDown>();
            PuGenerator.powerUps.Add(g6);

            Assert.AreEqual(PuGenerator.powerUps.Count, PowerUpsNumbers);
            /*
            List<int> numberGenerationResults = new List<int>(PowerUpsNumbers);
            numberGenerationResults[0] = numberGenerationResults[1] = numberGenerationResults[2] = numberGenerationResults[3] = numberGenerationResults[4] = numberGenerationResults[5] = 0;

            for (int i=1; i<=PowerUpsToGenerate; i++)
            {
                int res = PuGenerator.GeneratePowerUpNumber();
                numberGenerationResults[res]++;
            }

            for (int i = 0; i < PowerUpsNumbers; i++)
                Assert.GreaterOrEqual(numberGenerationResults[i], Minprobability);*/

        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator NewTestScriptWithEnumeratorPasses()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return null;
        }
    }
}