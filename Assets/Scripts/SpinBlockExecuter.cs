using Assets.DataStructure;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    /// <summary>
    /// Provides the functionality to start all gameobjects <c>SpinBlock</c> within groups moving by its given <c>SpinSettings</c>.
    /// </summary>
    public class SpinBlockExecuter : MonoBehaviour
    {
        private List<List<BlockMaterial>> _result = new List<List<BlockMaterial>>();
        private RunInfo _info = new RunInfo();

        private Transform lastWheel = null;
        private SpinBlock lastBlock = null;
        private GameChecker _gameChecker = new GameChecker();

        private int wheelCounter = 0;
        private int slotCounter = 0;
        private int counter = 0;

        public static SpinBlockExecuter executer;

        private void Awake()
        {
            if (executer == null)
            {
                DontDestroyOnLoad(gameObject);
                executer = this;
            }
            else if (executer != this)
            {
                Destroy(gameObject);
            }
        }
        
        private void SpinBlock_SpinFinished(object sender, BlockDataEventArgs args)
        {
            counter++;            
            _result[args.GroupIndex][args.BlockIndex] = args.Material;

            if (counter >= 15)
            {
                var topLine = new List<BlockMaterial>();
                var middleLine = new List<BlockMaterial>();
                var bottomLine = new List<BlockMaterial>();

                var topDiagonal = new List<BlockMaterial>();
                var bottomDiagonal = new List<BlockMaterial>();

                topLine.Add(_result[0][1]);
                topLine.Add(_result[1][1]);
                topLine.Add(_result[2][1]);

                middleLine.Add(_result[0][2]);
                middleLine.Add(_result[1][2]);
                middleLine.Add(_result[2][2]);

                bottomLine.Add(_result[0][3]);
                bottomLine.Add(_result[1][3]);
                bottomLine.Add(_result[2][3]);

                topDiagonal.Add(_result[0][1]);
                topDiagonal.Add(_result[1][2]);
                topDiagonal.Add(_result[2][3]);

                bottomDiagonal.Add(_result[0][3]);
                bottomDiagonal.Add(_result[1][2]);
                bottomDiagonal.Add(_result[2][1]);

                var topWin = _gameChecker.CalculateWinLine(topLine);
                var middleWin = _gameChecker.CalculateWinLine(middleLine);
                var bottomWin = _gameChecker.CalculateWinLine(bottomLine);
                var topDiagonalWin = _gameChecker.CalculateWinLine(topDiagonal);
                var bottomDiagonalWin = _gameChecker.CalculateWinLine(bottomDiagonal);

                CheckWin(topWin);
                CheckWin(middleWin);
                CheckWin(bottomWin);
                CheckWin(topDiagonalWin);
                CheckWin(bottomDiagonalWin);

                if (topWin.IsWin || middleWin.IsWin || bottomWin.IsWin || topDiagonalWin.IsWin || bottomDiagonalWin.IsWin)
                {
                    //GameObject.Find("HorizontalWinline").GetComponent<Renderer>().enabled = true;

                    SoundManager.manager.PlayWinningSound();
                }

                GameManager.manager.SaveGame();
            }
        }
        private void CheckWin(WinLineData winData)
        {
            if (!winData.IsWin)
            {
                return;
            }

            switch (winData.BlockMaterialData.MaterialIdentifier)
            {
                case 1:
                    GameManager.manager.AddCredits(50);
                    break;
                case 2:
                    GameManager.manager.AddCredits(20);
                    break;
                case 3:
                    GameManager.manager.AddCredits(20);
                    break;
                case 4:
                    GameManager.manager.AddCredits(20);
                    break;
                case 5:
                    GameManager.manager.AddCredits(20);
                    break;
                case 6:
                    GameManager.manager.AddCredits(20);
                    break;
                case 7:
                    GameManager.manager.AddCredits(500);
                    break;
                case 8:
                    GameManager.manager.AddCredits(20);
                    break;
                case 9:
                    GameManager.manager.AddCredits(20);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Event handler which will be executed when a spin has finished.
        /// </summary>
        public event EventHandler<GameFinishEventArgs> GameFinished;
        protected virtual void OnGameFinished(GameFinishEventArgs args)
        {
            EventHandler<GameFinishEventArgs> handler = GameFinished;
            if (handler != null)
            {
                handler(this, args);
            }
        }

        /// <summary>
        /// Initializes the <c>SpinBlock</c> with the needed images and load last game data.
        /// </summary>
        public void Initialize()
        {
            lastWheel = transform.GetChild(transform.childCount - 1);
            lastBlock = lastWheel.transform.GetChild(lastWheel.transform.childCount - 1).gameObject.GetComponent<SpinBlock>();

            GameManager.manager.LoadGame();

            _result = new List<List<BlockMaterial>>();

            for (int blockGroupIndex = 0; blockGroupIndex < transform.childCount; blockGroupIndex++)
            {
                _result.Add(new List<BlockMaterial>());
                for (int blockIndex = 0; blockIndex < transform.GetChild(0).childCount; blockIndex++)
                {
                    _result[blockGroupIndex].Add(new BlockMaterial("7", 7));
                }
            }

            for (int blockGroupIndex = 0; blockGroupIndex < transform.childCount; blockGroupIndex++)
            {
                var blockGroup = transform.GetChild(blockGroupIndex);
                for (int blockIndex = 0; blockIndex < blockGroup.transform.childCount; blockIndex++)
                {
                    var block = blockGroup.transform.GetChild(blockIndex).gameObject.GetComponent<SpinBlock>();
                    block.SetTexture(new BlockMaterial("7", 7));
                }
            }
        }
        /// <summary>
        /// Starts the spin execution.
        /// </summary>
        public void StartSpin()
        {            
            StartCoroutine(ExecuteRunner());
        }
        private IEnumerator ExecuteRunner()
        {
            if (_info.count > 0)
            {
                yield break;
            }

            SoundManager.manager.PlayStartSound();
            SoundManager.manager.PlaySpinningSound();
            GameManager.manager.AddCredits(-10);

            slotCounter = 0;
            counter = 0;
            wheelCounter = 0;

            for (int wheelIndex = 0; wheelIndex < transform.childCount; wheelIndex++)
            {
                var blockGroup = transform.GetChild(wheelIndex);

                // Avoids starting the wheels multiple times.
                if (lastBlock.IsRunning)
                {
                    break;
                }

                for (int slotIndex = 0; slotIndex < blockGroup.transform.childCount; slotIndex++)
                {
                    var block = blockGroup.transform.GetChild(slotIndex).gameObject.GetComponent<SpinBlock>();

                    block.SpinFinished -= SpinBlock_SpinFinished;
                    block.SpinFinished += SpinBlock_SpinFinished;

                    _info = block.StartMovement().ParallelCoroutine();
                }
            }

            while (_info.count > 0)
            {
                yield return null;
            }

            OnGameFinished(new GameFinishEventArgs(_result));
        }
    }
}
