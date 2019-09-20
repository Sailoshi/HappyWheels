using Assets.DataStructure;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    /// <summary>
    /// Provides functions to manage the game credits (points) and persistence.
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        public int Credits = 0;
        public Text CreditsTextFieldName;

        public static GameManager manager;

        private void Awake()
        {
            if (manager == null)
            {
                DontDestroyOnLoad(gameObject);
                manager = this;
            }
            else if (manager != this)
            {
                Destroy(gameObject);
            }
        }
        /// <summary>
        /// Adds the given amount of credits to the total credit state.
        /// </summary>
        /// <param name="credits">The amount of credits.</param>
        public void AddCredits(int credits)
        {
            Credits += credits;
            CreditsTextFieldName.text = Credits.ToString();
        }

        /// <summary>
        /// Changes the credit state to the given.
        /// </summary>
        /// <param name="credits">The credits number.</param>
        public void SetCredits(int credits)
        {
            Credits = credits;
            CreditsTextFieldName.text = Credits.ToString();
        }

        /// <summary>
        /// Saves the game data to a file.
        /// </summary>
        public void SaveGame()
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(Application.persistentDataPath + "/credits.dat");

            PlayerData data = new PlayerData();
            data.credits = Credits;

            bf.Serialize(file, data);
            file.Close();
        }

        /// <summary>
        /// Loads the game data from a file.
        /// </summary>
        public void LoadGame()
        {
            if (!File.Exists(Application.persistentDataPath + "/credits.dat"))
            {
                SetCredits(Credits);
                return;
            }

            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/credits.dat", FileMode.Open);

            PlayerData data = (PlayerData)bf.Deserialize(file);
            file.Close();

            SetCredits(data.credits);
        }
    }
}
