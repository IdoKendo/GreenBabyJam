using Assets.Scripts.Shared.Enums;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Shared.Managers
{
    public class MainMenu_Manager : MonoBehaviour
    {
        private Game_Manager m_gameManager = null;

        public void Awake()
        {
            m_gameManager = FindObjectOfType<Game_Manager>();
        }

        public void LoadGame()
        {
            Debug.Log("Loaded Game");
        }

        public void NewGame()
        {
            SceneManager.LoadScene((int)SceneType.LEVEL1);
            m_gameManager.NewGame();
        }

        public void Quit()
        {
            Application.Quit();
        }
    }
}
