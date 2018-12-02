using Assets.Scripts.Shared.Enums;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Shared.Managers
{
    public class MainMenu_Manager : MonoBehaviour
    {
        private Game_Manager _gameManager = null;

        public void Awake()
        {
            _gameManager = FindObjectOfType<Game_Manager>();
        }

        public void LoadGame()
        {
            Debug.Log("Loaded Game");
        }

        public void NewGame()
        {
            SceneManager.LoadScene((int)SceneType.LEVEL1);
            _gameManager.NewGame();
        }

        public void Quit()
        {
            Application.Quit();
        }
    }
}
