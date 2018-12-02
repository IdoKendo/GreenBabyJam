namespace Assets.Scripts.Shared.Managers
{
    public class Player_Manager
    {
        public float Health { get; set; }
        public float MaxHealth { get; set; }

        public Player_Manager()
        {
            MaxHealth = 100;
            Health = MaxHealth;
        }
    }
}
