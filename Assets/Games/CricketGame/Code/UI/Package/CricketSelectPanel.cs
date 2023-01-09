using System.Collections.Generic;
using Games.CricketGame.Attack;
using Games.CricketGame.Code.Package;
using Games.CricketGame.Cricket_;
using Games.CricketGame.Player_;
using UnityEngine;
using UnityEngine.UI;

namespace Games.CricketGame.UI.Package
{
    public class CricketSelectPanel : MonoBehaviour
    {
        //查看精灵的页面 ToDo (当前只支持战斗时查看)
        [SerializeField] private List<CricketBagCell> cricketBagCells;
        public int cur;
        public List<SkillBagCell> skillBagCells;
        [SerializeField] private CharacterInfoPanel characterInfoPanel;
        public Text topText;
        public PropertyIcon icon1;
        public PropertyIcon icon2;
        private bool _connected = false;
        private Player _player;

        private void Awake()
        {
            var cricketsPanel = transform.GetChild(0).GetChild(0);
            cricketBagCells = new List<CricketBagCell>();
            for (int i = 0; i < cricketsPanel.childCount; i++)
            {
                cricketBagCells.Add(cricketsPanel.GetChild(i).GetComponent<CricketBagCell>());
                cricketBagCells[i].idx = i;
                cricketBagCells[i].onClickedAction += OnCricketCellClicked;
            }

            var skillInfoPanel = transform.GetChild(1).GetChild(1).GetChild(0);
            skillBagCells = new List<SkillBagCell>();
            for (int i = 0; i < skillInfoPanel.childCount; i++)
            {
                skillBagCells.Add(skillInfoPanel.GetChild(i).GetComponent<SkillBagCell>());
            }
        }

        public void Connect(Player player)
        {
            if (_connected)
            {
                DisConnect();
            }

            _player = player;
            //默认选中第一个宝可梦

            for (int i = 0; i != 6; i++)
            {
                if (i < player.crickets.Count)
                {
                    //玩家i个宝可梦
                    cricketBagCells[i].Connect(player.crickets[i]);
                }
                else
                {
                    print($"玩家没有第{i + 1}个cricket");
                    cricketBagCells[i].Clear();
                }
            }

            cur = 0;
            var cur_cricket = player.crickets[cur];
            for (int i = 0; i != 4; ++i)
            {
                if (i < cur_cricket.skills.Count)
                {
                    skillBagCells[i].Connect(cur_cricket.skills[i]);
                }
                else
                {
                    skillBagCells[i].Clear();
                }
            }

            characterInfoPanel._update(cur_cricket.character);
        }

        public void DisConnect()
        {
            if (!_connected)
            {
                return;
            }

            _connected = false;
        }

        private void OnCricketCellClicked(int idx)
        {
            if (idx >= _player.crickets.Count)
                return;
            var player_next = _player.crickets[idx];
            if (player_next.healthAbility > 0)
            {
                print($"{cricketBagCells[idx].name}被点击,可以战斗");
                var inputStruct = new AttackInputStruct(SelectTypeEnum.切换精灵,player_next.GetType(),player_next);
                InputStorage.player_input = inputStruct;
            }
            else
            {
                print($"{cricketBagCells[idx].name}被点击,不可以战斗");
            }
        }
    }
}