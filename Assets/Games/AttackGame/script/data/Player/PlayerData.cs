using UnityEngine;

namespace AttackGame
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "Data/Player", order = 0)]
    public class PlayerData : ScriptableObject
    {
        public float move_speed;
        
    }
}