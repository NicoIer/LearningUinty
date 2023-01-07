using System.Collections.Generic;
using Cinemachine;
using Cysharp.Threading.Tasks;
using Games.CricketGame.Code.Cricket_;
using Games.CricketGame.Code.UI.Attack;
using Games.CricketGame.Cricket_;
using Games.CricketGame.Npc_;
using Games.CricketGame.Player_;
using Games.CricketGame.UI;
using Nico.Common;
using UnityEngine;
using UnityEngine.Serialization;

namespace Games.CricketGame.Manager
{
    public static class PriorityCompare
    {
        //Todo 把它删除掉
        private static readonly Dictionary<PriorityEnum, int> _speedMap = new()
        {
            { PriorityEnum.超先手, 2 },
            { PriorityEnum.先手, 1 },
            { PriorityEnum.正常手, 0 },
            { PriorityEnum.后手, -1 }
        };

        public static bool Compare(PriorityEnum p1, PriorityEnum p2)
        {
            return _speedMap[p1] > _speedMap[p2];
        }
    }

    /// <summary>
    /// 战斗管理 
    /// </summary>
    public class AttackSceneManager : MonoBehaviour
    {
        //ToDo 还有战斗场景地图啥的 没有设计 
        private bool _round_start; //回合是否开始
        private bool _attack_over; //战斗是否结束
        public Player player; //玩家是需要持有的
        public Npc npc;
        private CinemachineVirtualCamera virtualCamera;
        public AttackPanel ui;
        public Transform p1;
        public Transform p2;


        #region 在场上的精灵

        private Cricket _self;
        private Cricket _other;

        #endregion

        private void Update()
        {
            _update_rotation(); //更新场景内的2D物体,使面朝相机
            if (!_attack_over)
            {
                if (!_round_start)
                {
                    print("开启UniTask执行回合逻辑");
                    //如果回合没有开始 则开始回合
                    MainLogic().Forget();
                    _round_start = true;
                }
            }
        }


        #region 战斗逻辑

        private async UniTask MainLogic()
        {
            print("回合开始");
            ui.RoundStart(); //显示ui
            var s1 = await PlayerSelect(); //等待玩家输入
            var s2 = NpcSelect(); //Npc立即选择一个输入
            print("回合结算");
            ui.RoundPlaying();
            bool player_first = PlayerFirst(_self.data, _other.data, s1, s2);

            if (player_first)
            {
                await s1.Apply(_self, _other);
                if (_other.data.healthAbility <= 0)
                {
                    _other.Dead();
                    //判断npc是否还可以战斗
                    NpcLose();
                    _attack_over = true; //战斗结束
                    _round_start = false; //
                }

                print("玩家攻击完毕");
                await s2.Apply(_other, _self);
                if (_self.data.healthAbility <= 0)
                {
                    _self.Dead();
                    if (!player.have_next())
                    {
                        print("玩家没有可用的Cricket了!!!");
                        PlayerLose();
                        _attack_over = true; //战斗结束
                        _round_start = false; //
                    }
                }

                print("敌人攻击完毕");
            }
            else
            {
                await s2.Apply(_other, _self);
                print("敌人攻击完毕");
                if (_self.data.healthAbility <= 0)
                {
                    _self.Dead();
                    if (!player.have_next())
                    {
                        print("玩家没有可用的Cricket了!!!");
                        PlayerLose();
                        _attack_over = true; //战斗结束
                        _round_start = false; //
                    }
                }

                await s1.Apply(_self, _other);
                if (_other.data.healthAbility <= 0)
                {
                    _other.Dead();
                    //判断npc是否还可以战斗
                    NpcLose();
                    _attack_over = true; //战斗结束
                    _round_start = false; //
                }

                print("玩家攻击完毕");
            }

            //回合逻辑结束
            print("回合结束");
            _round_start = false;
        }

        private void PlayerLose()
        {
            print("玩家输了,现在要返回大地图(基地)");
            _attack_over = true;
            ExitAttack();
        }

        private void NpcLose()
        {
            print("npc输了,现在为玩家结算奖励");
            _attack_over = true;
            GameManager.instance.ExitAttackMap();
        }

        private bool PlayerFirst(CricketData self, CricketData other, Skill s1, Skill s2)
        {
            if (PriorityCompare.Compare(s1.meta.priority, s2.meta.priority))
            {
                //技能优先级高?
                return true;
            }

            if (self.speedAbility > other.speedAbility)
            {
                //速度快?
                return true;
            }

            if (self.speedAbility == other.speedAbility)
            {
                return RandomManager.Probability(0, 1) > 0.5;
            }

            return false;
        }

        private async UniTask<Skill> PlayerSelect()
        {
            while (true)
            {
                if (AttackInputHandler.player_input != null)
                {
                    var temp = AttackInputHandler.player_input;
                    AttackInputHandler.player_input = null;
                    print($"玩家选择了技能:{temp.meta.name}");
                    return temp;
                }

                print("等待玩家输入");
                await UniTask.WaitForFixedUpdate();
            }
        }

        private Skill NpcSelect()
        {
            return npc.random_skill();
        }

        #endregion

        public void ExitAttack()
        {
            print("战斗结束");
            player = null;
            npc = null;
            ui.DisConnect();
            ui.gameObject.SetActive(false);
            // gameObject.SetActive(false);
            GameManager.instance.ExitAttackMap();
        }

        public void EnterAttack(Player player, Npc npc, CinemachineVirtualCamera virtualCamera)
        {
            ui.gameObject.SetActive(true);
            //获取战斗相机的位置
            this.virtualCamera = virtualCamera;
            this.npc = npc;
            this.player = player;
            //获取玩家的和Npc的首发精灵
            _self = Instantiate(player.crickets[0], p1);
            // _self = player.crickets[0];
            _other = Instantiate(npc.crickets[0], p2);
            // _other = npc.crickets[0];
            //激活他们
            _self.gameObject.SetActive(true);
            _other.gameObject.SetActive(true);
            //将数据连接到ui
            ui.Connect(_self.data, _other.data);
            _attack_over = false;
        }

        #region 场景相关

        private void _update_rotation()
        {
            var rotation = virtualCamera.transform.rotation;
            _self.transform.localRotation = rotation;
            _other.transform.localRotation = rotation;
        }

        #endregion
    }
}