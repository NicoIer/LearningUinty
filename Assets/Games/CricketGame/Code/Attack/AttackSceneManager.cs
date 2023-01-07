using System.Collections.Generic;
using Cinemachine;
using Cysharp.Threading.Tasks;
using Games.CricketGame.Code.Attack;
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
        public AttackPosition p1;
        public AttackPosition p2;

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

        private async UniTask<(Skill, Skill)> _get_input()
        {
            var s1 = await PlayerSelect();
            var s2 = npc.random_skill();
            return (s1, s2);
        }

        private async UniTask RoundPlaying()
        {
            //等待输入
            var (player_skill, npc_skill) = await _get_input();
            ui.RoundPlaying();
            bool player_first = PlayerFirst(p1.cricket.data, p2.cricket.data, player_skill, npc_skill);
            var (attacked_dead, defenser_dead) = (false, false);


            if (player_first)
            {
                #region 玩家先手

                #region 玩家攻击

                (attacked_dead, defenser_dead) = await _apply(player_skill, p1.cricket, p2.cricket); //玩家攻击
                if (attacked_dead)
                {
                    //玩家GG
                    PlayerLose();
                    return;
                }

                if (defenser_dead)
                {
                    //npc GG
                    PlayerWin();
                    return;
                }

                #endregion

                #region npc攻击

                //都没有gg 则 轮到 Npc释放技能
                (attacked_dead, defenser_dead) = await _apply(npc_skill, p2.cricket, p1.cricket);
                if (attacked_dead)
                {
                    //Npc场上精灵gg 
                    if (npc.HaveAvaliableCricket())
                    {
                        //还有可用的精灵
                        print("npc可以换下一个精灵!!");
                        return;
                    }
                    else
                    {
                        PlayerWin();
                        return;
                    }
                }

                if (defenser_dead)
                {
                    if (player.HavaAvalizableCricket())
                    {
                        print("玩家可以换下一个精灵");
                        return;
                    }

                    PlayerLose();
                    return;
                }

                #endregion

                #endregion
            }

            else
            {
                #region 玩家后手

                #region 敌方攻击

                (attacked_dead, defenser_dead) = await _apply(npc_skill, p2.cricket, p1.cricket);
                if (attacked_dead)
                {
                    PlayerWin();
                    return;
                }

                if (defenser_dead)
                {
                    PlayerLose();
                    return;
                }

                #endregion


                #region 玩家攻击

                (attacked_dead, defenser_dead) = await _apply(player_skill, p1.cricket, p2.cricket);
                if (attacked_dead)
                {
                    PlayerLose();
                    return;
                }

                if (defenser_dead)
                {
                    PlayerWin();
                    return;
                }

                #endregion

                #endregion
            }


            _round_start = false;
        }

        /// <summary>
        /// 应用技能效果
        /// </summary>
        /// <param name="skill">使用的技能</param>
        /// <param name="user">使用者</param>
        /// <param name="defenser">防御者</param>
        /// <returns>使用者死亡,防御者死亡</returns>
        private async UniTask<(bool, bool)> _apply(Skill skill, Cricket user, Cricket defenser)
        {
            await skill.Apply(user, defenser);
            var (defenser_dead, attacker_dead) = (false, false);
            if (defenser.data.healthAbility <= 0)
            {
                defenser.Dead();
                defenser_dead = true;
            }

            if (user.data.healthAbility <= 0)
            {
                user.Dead();
                attacker_dead = true;
            }

            return (attacker_dead, defenser_dead);
        }

        private async UniTask MainLogic()
        {
            //新的回合开始 先判断玩家的精灵是否gg            
            if (p1.cricket.data.healthAbility <= 0)
            {//玩家当前的精灵已经GG
                //等待玩家选择新的精灵
                var next_cricket = await _change_cricket();
                p1.ResetData(next_cricket);
                ui.DisConnect();
                ui.Connect(p1.cricket.data,p2.cricket.data);
            }

            if (p2.cricket.data.healthAbility <= 0)
            {
                //ToDo 完善NPC的部分
            }
            
            ui.RoundStart(); //显示ui
            await RoundPlaying(); //进行回合
        }

        private async UniTask<CricketData> _change_cricket()
        {
            while (true)
            {
                if (AttackInputHandler.player_next != null)
                {
                    var temp = AttackInputHandler.player_next;
                    AttackInputHandler.player_next = null;
                    return temp;
                }
                print("等待玩家切换精灵");
                await UniTask.WaitForFixedUpdate();
            }
        }
        private void PlayerCricketLose()
        {
            print($"玩家当前:{p1.cricket.name}战败了");
            if (player.HavaAvalizableCricket())
            {
                print("玩家还有可以使用的精灵");
            }
            else
            {
                print("玩家没有可用的精灵了");
                PlayerLose();
            }
        }

        private void PlayerLose()
        {
            print("玩家输了,现在要返回大地图(基地)");
            _attack_over = true;
            _round_start = false;
            ExitAttack();
        }

        private void PlayerWin()
        {
            print("npc输了,现在为玩家结算奖励");
            _attack_over = true;
            _round_start = false;
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

        #endregion

        public void ExitAttack()
        {
            player = null;
            npc = null;
            ui.DisConnect();
            ui.gameObject.SetActive(false);
            p1.ResetData(null);
            p2.ResetData(null);
            GameManager.instance.ExitAttackMap();
        }

        public void EnterAttack(Player player, Npc npc, CinemachineVirtualCamera virtualCamera)
        {
            ui.gameObject.SetActive(true);
            //获取战斗相机的位置
            this.virtualCamera = virtualCamera;
            this.npc = npc;
            this.player = player;
            //获取玩家的和Npc的首发精灵数据 并赋值给预先准备好的Cricket
            p1.ResetData(player.crickets[0]);
            p2.ResetData(npc.crickets[0]);
            //激活他们
            p1.gameObject.SetActive(true);
            p2.gameObject.SetActive(true);
            //将数据连接到ui
            ui.Connect(p1.cricket.data, p2.cricket.data);
            _attack_over = false;
        }

        #region 场景相关

        private void _update_rotation()
        {
            var rotation = virtualCamera.transform.rotation;
            p1.transform.localRotation = rotation;
            p2.transform.localRotation = rotation;
        }

        #endregion
    }
}