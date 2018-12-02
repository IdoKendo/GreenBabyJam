using Assets.Scripts.PlayerScript;
using UnityEngine;

namespace Assets.Scripts.Shared.States
{
    public class PlayerState : MonoBehaviour
    {
        public PlayerStatistics localPlayerData = new PlayerStatistics();

        //At start, load data from Player Manager.
        void Start()
        {
            localPlayerData = Game_Manager.PlayerManager.savedPlayerData;
        }

        //Save data to Player Manager   
        public void SavePlayer()
        {
            Game_Manager.PlayerManager.savedPlayerData = localPlayerData;
        }
    }
}
