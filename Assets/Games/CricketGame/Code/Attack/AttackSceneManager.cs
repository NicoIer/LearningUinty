using System;
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

namespace Games.CricketGame.Manager
{
    /// <summary>
    /// 战斗管理 
    /// </summary>
    public class AttackSceneManager : MonoBehaviour
    {
        //ToDo 还有战斗场景地图啥的 没有设计 
        private bool _round_start; //回合是否开始
        private bool _attack_over; //战斗是否结束
        private Player _player; //玩家是需要持有的
        private Npc _npc;
        private CinemachineVirtualCamera _virtualCamera;
        private AttackPanel attackPanel => UIManager.instance.attackPanel;
        public AttackPosition p1;
        public AttackPosition p2;

        private void Update()
        {
            _update_rotation(); //更新场景内的2D物体,使面朝相机
            if (!_attack_over)
            {
                //战斗没有结束
                if (!_round_start)
                {
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
            var s2 = _npc.random_skill(p2.cricket.data);
            return (s1, s2);
        }

        private async UniTask RoundPlaying()
        {
            //等待输入
            var (player_skill, npc_skill) = await _get_input();
            attackPanel.RoundPlaying();
            bool player_first = PlayerFirst(p1.cricket.data, p2.cricket.data, player_skill, npc_skill);
            bool attacked_dead;
            bool defender;

            if (player_first)
            {
                #region 玩家先手

                #region 玩家攻击

                (attacked_dead, defender) = await _apply(player_skill, p1.cricket, p2.cricket); //玩家攻击
                if (attacked_dead)
                {
                    //玩家当前精灵GG
                    if (!CanPlayerContinue())
                    {
                        //玩家不能再战斗
                        return;
                    }

                    _attack_over = false;
                    _round_start = false;
                    return;
                }

                if (defender)
                {
                    //NPC当前精灵GG 回合结束
                    if (!CanNpcContinue())
                    {
                        //Npc不能再战斗
                        return;
                    }

                    _attack_over = false;
                    _round_start = false;
                    return;
                }

                #endregion

                #region npc攻击

                //npc当前精灵没有GG
                (attacked_dead, defender) = await _apply(npc_skill, p2.cricket, p1.cricket);
                if (attacked_dead)
                {
                    //npc 精灵GG 
                    if (!CanNpcContinue())
                    {
                        return;
                    }

                    //还有可用的精灵
                    _attack_over = false;
                    _round_start = false;
                    return;
                }

                if (defender)
                {
                    if (!CanPlayerContinue())
                    {
                        return;
                    }

                    _attack_over = false;
                    _round_start = false;
                    return;
                }

                #endregion

                #endregion
            }

            else
            {
                #region 玩家后手

                #region 敌方攻击

                (attacked_dead, defender) = await _apply(npc_skill, p2.cricket, p1.cricket);
                if (attacked_dead)
                {
                    if (!CanNpcContinue())
                    {
                        return;
                    }

                    _attack_over = false;
                    _round_start = false;
                    return;
                }

                if (defender)
                {
                    if (!CanPlayerContinue())
                    {
                        return;
                    }

                    _attack_over = false;
                    _round_start = false;
                    return;
                }

                #endregion


                #region 玩家攻击

                (attacked_dead, defender) = await _apply(player_skill, p1.cricket, p2.cricket);
                if (attacked_dead)
                {
                    if (!CanPlayerContinue())
                    {
                        return;
                    }

                    _attack_over = false;
                    _round_start = false;
                    return;
                }

                if (defender)
                {
                    if (!CanNpcContinue())
                    {
                        return;
                    }

                    _attack_over = false;
                    _round_start = false;
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
            //进入回合逻辑 说明我方和敌方都有能够战斗的精灵
            //新的回合开始 先判断玩家的当前精灵是否gg            
            if (p1.cricket.data.healthAbility <= 0)
            {
                //玩家当前的精灵已经GG
                //等待玩家选择新的精灵
                var next_cricket = await _player_change_cricket();
                print($"我方派{next_cricket.name}上场了");
                p1.ResetData(next_cricket);
                attackPanel.DisConnect();
                attackPanel.Connect(p1.cricket.data, p2.cricket.data);
            }
            else if (p2.cricket.data.healthAbility <= 0)
            {
                //npc精灵GG
                var next_cricket = await _npc_change_cricket();
                print($"敌方派上:{next_cricket.name}");
                p2.ResetData(next_cricket);
                attackPanel.DisConnect();
                attackPanel.Connect(p1.cricket.data, p2.cricket.data);
            }

            attackPanel.RoundStart(); //显示ui
            await RoundPlaying(); //进行回合
        }

        private async UniTask<CricketData> _npc_change_cricket()
        {
            await UniTask.WaitForFixedUpdate();
            return _npc.FirstAvailableCricket();
        }

        private async UniTask<CricketData> _player_change_cricket()
        {
            UIManager.instance.cricketPanel.OpenSelectPanel();
            while (true)
            {
                if (AttackInputHandler.player_next != null)
                {
                    var temp = AttackInputHandler.player_next;
                    AttackInputHandler.player_next = null;
                    UIManager.instance.cricketPanel.CloseSelectPanel();
                    return temp;
                }

                print("等待玩家切换精灵");
                await UniTask.WaitForFixedUpdate();
            }
        }

        #region 胜利/失败逻辑

        #region 场上精灵战败

        /// <summary>
        /// 判断玩家(判断是否还有血量>0的cricket)是否还能继续战斗 
        /// </summary>
        /// <returns>能:true,不能:false</returns>
        private bool CanPlayerContinue()
        {
            print($"玩家当前:{p1.cricket.name}战败了");
            if (_player.HavaAvailableCricket())
            {
                print("玩家还有可以使用的精灵");
                return true;
            }

            print("玩家没有可用的精灵了");
            PlayerLose();
            return false;
        }

        private bool CanNpcContinue()
        {
            print($"Npc场上精灵{p2.cricket.name}战败了");
            if (_npc.HaveAvailableCricket())
            {
                print("Npc还有可用的精灵");
                return true;
            }

            print("npc没有可用的精灵了");
            PlayerWin();
            return false;
        }

        #endregion

        #region 战斗结束

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
            ExitAttack();
        }

        #endregion

        #endregion

        private bool PlayerFirst(CricketData self, CricketData other, Skill s1, Skill s2)
        {
            if (CompareManager.ComparePriority(s1.meta.priority, s2.meta.priority))
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

        private void ExitAttack()
        {
            _attack_over = true;
            Destroy(_npc);//销毁npc游戏物体
            _player = null;
            _npc = null;
            attackPanel.DisConnect();
            attackPanel.Close();
            p1.ResetData(null);
            p2.ResetData(null);
            GameManager.instance.ExitAttackMap();
        }

        public void EnterAttack(Player player, Npc npc, CinemachineVirtualCamera virtualCamera)
        {
            if (!_attack_over)
            {
                throw new ArgumentException();
            }

            _attack_over = false;
            attackPanel.Open();
            //获取战斗相机的位置
            _virtualCamera = virtualCamera;
            _player = player;
            _npc = npc;
            _npc.gameObject.SetActive(false);//隐藏npc游戏物体
            _active_scene(); //激活场景
            attackPanel.Connect(p1.cricket.data, p2.cricket.data);//将数据连接到ui
        }

        private void _active_scene()
        {
            if (!_attack_over)
            {
                throw new ArgumentException();
            }

            //激活他们
            p1.gameObject.SetActive(true);
            p2.gameObject.SetActive(true);
            //
            p1.ResetData(_player.FirstAvailableCricket());
            p2.ResetData(_npc.FirstAvailableCricket());
        }

        #region 场景相关

        private void _update_rotation()
        {
            var rotation = _virtualCamera.transform.rotation;
            p1.transform.localRotation = rotation;
            p2.transform.localRotation = rotation;
        }

        #endregion
    }
}