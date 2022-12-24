using UnityEngine;

namespace AttackGame.script.npc
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
    }
}