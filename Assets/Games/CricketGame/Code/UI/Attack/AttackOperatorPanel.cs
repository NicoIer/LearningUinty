using Games.CricketGame.Code.Cricket_;
using UnityEngine;

namespace Games.CricketGame.UI
{
    public class AttackOperatorPanel : MonoBehaviour
    {
        public SelectPanel selectPanel;
        public SkillPanel skillPanel;
        private bool _initialized;
        private CricketData _data;
        public void Initialize(CricketData data)
        {
            _initialized = true;
            _data = data;
            skillPanel.Band(data);
        }

    }
}