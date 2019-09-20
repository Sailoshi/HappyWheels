using System.Collections;
using System.Collections.Generic;
using Assets.DataStructure;
using Assets.Scripts;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Assets.Editor
{
    public class BlockAnimationTests {

        [UnityTest]
        public IEnumerator TestProbability()
        {
            MonoBehaviour.Instantiate(Resources.Load("Scenery"));

            

            yield return null;
        }

        private void SpinBlockExecuter_GameFinish(object sender, GameFinishEventArgs e)
        {
            var test = e;
        }

        [UnityTest]
        public IEnumerator TestAllHorizontalWinLines()
        {
            var counter = 0;

            for (var i = 0; i < 10; i++)
            {
                var result = new bool[3][];
                var input = new BlockMaterial[3][];
                for (var groupIndex = 0; groupIndex < input.Length; groupIndex++)
                {
                    input[groupIndex] = new BlockMaterial[3];
                    for (var blockIndex = 0; blockIndex < input[groupIndex].Length; blockIndex++)
                    {
                        var random = UnityEngine.Random.Range(1, 10);
                        input[groupIndex][blockIndex] = new BlockMaterial(random.ToString(), random);
                    }
                }

                //input[0][0] = new BlockMaterial("1", 1);
                //input[0][1] = new BlockMaterial("1", 1);
                //input[0][2] = new BlockMaterial("1", 1);

                //input[1][0] = new BlockMaterial("2", 2);
                //input[1][1] = new BlockMaterial("2", 2);
                //input[1][2] = new BlockMaterial("2", 2);

                //input[2][0] = new BlockMaterial("3", 3);
                //input[2][1] = new BlockMaterial("3", 3);
                //input[2][2] = new BlockMaterial("3", 3);

                var topLine = new List<BlockMaterial>();
                var middleLine = new List<BlockMaterial>();
                var bottomLine = new List<BlockMaterial>();

                var topDiagonal = new List<BlockMaterial>();
                var bottomDiagonal = new List<BlockMaterial>();

                topLine.Add(input[0][0]);
                topLine.Add(input[1][0]);
                topLine.Add(input[2][0]);

                middleLine.Add(input[0][1]);
                middleLine.Add(input[1][1]);
                middleLine.Add(input[2][1]);

                bottomLine.Add(input[0][2]);
                bottomLine.Add(input[1][2]);
                bottomLine.Add(input[2][2]);

                topDiagonal.Add(input[0][0]);
                topDiagonal.Add(input[1][1]);
                topDiagonal.Add(input[2][2]);

                bottomDiagonal.Add(input[0][2]);
                bottomDiagonal.Add(input[1][1]);
                bottomDiagonal.Add(input[2][0]);

                var _gameChecker = new GameChecker();

                var topWin = _gameChecker.CalculateWinLine(topLine);
                var middleWin = _gameChecker.CalculateWinLine(middleLine);
                var bottomWin = _gameChecker.CalculateWinLine(bottomLine);
                var topDiagonalWin = _gameChecker.CalculateWinLine(topDiagonal);
                var bottomDiagonalWin = _gameChecker.CalculateWinLine(bottomDiagonal);
                
                if (topWin.IsWin || middleWin.IsWin || bottomWin.IsWin || topDiagonalWin.IsWin || bottomDiagonalWin.IsWin)
                {
                    counter++;
                }
            }

            Assert.AreEqual(0, counter);

            //Assert.IsTrue(result[0][0]);
            //Assert.IsTrue(result[0][1]);
            //Assert.IsTrue(result[0][2]);

            //Assert.IsTrue(result[1][0]);
            //Assert.IsTrue(result[1][1]);
            //Assert.IsTrue(result[1][2]);

            //Assert.IsTrue(result[2][0]);
            //Assert.IsTrue(result[2][1]);
            //Assert.IsTrue(result[2][2]);

            yield return null;
        }

        [UnityTest]
        public IEnumerator TestAllDiagonalWinLines()
        {
            var result = new bool[3][];
            var input = new BlockMaterial[3][];
            for (var groupIndex = 0; groupIndex < input.Length; groupIndex++)
            {
                input[groupIndex] = new BlockMaterial[3];
                for (var blockIndex = 0; blockIndex < input[groupIndex].Length; blockIndex++)
                {
                    var random = UnityEngine.Random.Range(1, 10);
                    input[groupIndex][blockIndex] = new BlockMaterial(random.ToString(), random);
                }
            }

            input[0][0] = new BlockMaterial("1", 1);
            input[1][1] = new BlockMaterial("1", 1);
            input[2][2] = new BlockMaterial("1", 1);

            input[2][2] = new BlockMaterial("1", 1);
            input[2][0] = new BlockMaterial("1", 1);

            //result = GameChecker.CheckWinPossibility(input);

            //Assert.IsTrue(result[0][0]);
            //Assert.IsTrue(result[1][1]);
            //Assert.IsTrue(result[2][2]);

            //Assert.IsTrue(result[0][0]);
            //Assert.IsTrue(result[2][0]);

            yield return null;
        }
    }
}
