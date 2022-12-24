using AttackGame.Talking;
using UnityEngine;

namespace AttackGame.NPC
{
    /// <summary>
    /// NpcData
    /// 存储包括Npc名字
    /// 对话
    /// </summary>
    [CreateAssetMenu(fileName = "NpcData", menuName = "Data/Npc", order = 0)]
    public class NpcData : ScriptableObject
    {
        public string npc_name;
        public SimpleData taling_data;//ToDo 暂时这样写 明天用JSON解决
    }
}