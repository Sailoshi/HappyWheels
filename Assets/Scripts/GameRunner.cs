using Assets.DataStructure;
using UnityEngine;

namespace Assets.Scripts
{
    /// <summary>
    /// Provides the functionality of the game itself. Must be used on top of the root gameobject.
    /// </summary>
    public class GameRunner : MonoBehaviour
    {
        private void Start()
        {
            SpinBlockExecuter.executer.Initialize();
            SpinBlockExecuter.executer.GameFinished += SpinBlockExecuter_GameFinish;
            //GameObject.Find("HorizontalWinline").GetComponent<Renderer>().enabled = false;
        }

        private void SpinBlockExecuter_GameFinish(object sender, GameFinishEventArgs e)
        {
            var test = e;
        }

        private void Update()
        {
            if (Input.GetKeyDown("space") || Input.GetKey("space") || Input.GetMouseButtonDown(0))
            {
                SpinBlockExecuter.executer.StartSpin();
            }            
        }
    }
}
