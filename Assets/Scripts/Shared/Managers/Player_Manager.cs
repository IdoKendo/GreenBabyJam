﻿using Assets.Scripts.Shared.Enums;

namespace Assets.Scripts.Shared.Managers
{
    public class Player_Manager
    {
        public bool Fireballs { get; set; }
        public SceneType Scene { get; set; }
        public bool Shield { get; set; }
        public float Health { get; set; }
        public float MaxHealth { get; set; }

        public Player_Manager()
        {
            Fireballs = false;
            MaxHealth = 400;
            Health = MaxHealth;
            Scene = SceneType.LEVEL1;
            Shield = false;
        }
    }
}
