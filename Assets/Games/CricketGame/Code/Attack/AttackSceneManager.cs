using System;
using Cinemachine;
using Cysharp.Threading.Tasks;
using Games.CricketGame.Attack;
using Games.CricketGame.Code.Attack;
using Games.CricketGame.Code.Cricket_;
using Games.CricketGame.Cricket_;
using Games.CricketGame.Npc_;
using Games.CricketGame.Player_;
using Games.CricketGame.UI;
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
        private bool _attack_over = true; //战斗是否结束
        private Player _player; //玩家是需要持有的
        private Npc _npc;
        private CinemachineVirtualCamera _virtualCamera;
        private AttackPanel attackPanel => UIManager.instance.attackPanel;
        public AttackEnvironment environment;
        public AttackPosition p1;
        public AttackPosition p2;
        private AttackInputHandler _inputHandler;

        private void Awake()
        {
            _inputHandler = new();
        }

        private void Update()
        {
            if (!_attack_over)
            {
                //战斗没有结束
                _update_rotation(); //更新场景内的2D物体,使面朝相机
                if (!_round_start)
                {
                    //如果回合没有开始 则开始回合
                    MainLogic().Forget();
                    _round_start = true;
                }
            }
        }


        #region 战斗逻辑

        private async UniTask<(AttackInputStruct, AttackInputStruct)> _get_attack_input()
        {
            var s1 = await _inputHandler.GetPlayerInput();
            var s2 = new AttackInputStruct(SelectTypeEnum.使用技能, typeof(Skill), _npc.random_skill(p2.cricket.data));
            return (s1, s2);
        }

        private async UniTask RoundPlaying()
        {
            //ToDo 这里有很多种不同的输入可能 后面来考虑
            //等待输入
            var (playerInput, npcInput) = await _get_attack_input();
            print("输入完毕");
            attackPanel.RoundPlaying();
            if (playerInput.typeEnum == SelectTypeEnum.使用技能)
            {
                switch (npcInput.typeEnum)
                {
                    //双方都决定使用技能进行攻击
                    case SelectTypeEnum.使用技能:
                    {
                        Skill playerSkill = AttackInputStruct.CastToType<Skill>(playerInput);
                        Skill npcSkill = AttackInputStruct.CastToType<Skill>(npcInput);
                        await _attack_together(playerSkill, npcSkill);
                        break;
                    }
                    case SelectTypeEnum.使用道具:
                        break;
                    case SelectTypeEnum.切换精灵:
                        break;
                    case SelectTypeEnum.逃跑:
                        if (CompareManager.CompareNpcRun(p1.data, p2.data))
                        {
                            PlayerWin();
                            return;
                        }
                        break;
                }
            }
            else if (playerInput.typeEnum == SelectTypeEnum.使用道具)
            {
                if (npcInput.typeEnum == SelectTypeEnum.使用技能)
                {
                }
                else if (npcInput.typeEnum == SelectTypeEnum.使用道具)
                {
                }
                else if (npcInput.typeEnum == SelectTypeEnum.切换精灵)
                {
                }
                else if (npcInput.typeEnum == SelectTypeEnum.逃跑)
                {
                }
            }
            else if (playerInput.typeEnum == SelectTypeEnum.切换精灵)
            {
                if (npcInput.typeEnum == SelectTypeEnum.使用技能)
                {
                }
                else if (npcInput.typeEnum == SelectTypeEnum.使用道具)
                {
                }
                else if (npcInput.typeEnum == SelectTypeEnum.切换精灵)
                {
                }
                else if (npcInput.typeEnum == SelectTypeEnum.逃跑)
                {
                }
            }
            else if (playerInput.typeEnum == SelectTypeEnum.逃跑)
            {
                if (CompareManager.ComparePlayerRun(_npc, p1.data, p2.data))
                {
                    //判断玩家是否能够逃跑 ToDo 当面对特定NPC时不可以逃跑
                    PlayerLose();
                    return;
                }

                //没有 跑掉
                switch (npcInput.typeEnum)
                {
                    case SelectTypeEnum.使用技能:
                        var skill = AttackInputStruct.CastToType<Skill>(npcInput);
                        await _npc_attack(p2.cricket, p1.cricket, skill);
                        break;
                    case SelectTypeEnum.使用道具:
                        break;
                    case SelectTypeEnum.切换精灵:
                        break;
                    case SelectTypeEnum.逃跑:
                        break;
                }
            }

            _round_start = false;
        }

        private async UniTask _npc_attack(Cricket attacker, Cricket defenser, Skill skill)
        {
            //Npc释放技能攻击
            bool attacker_dead;
            bool defender_dead;
            (attacker_dead, defender_dead) = await _apply(skill, attacker, defenser); //玩家攻击
            if ((attacker_dead && !CanNpcContinue()) || (defender_dead && !CanPlayerContinue()))
            {
                //有一方不能够再战斗
                return;
            }
        }

        private async UniTask _attack_together(Skill playerSkill, Skill npcSkill)
        {
            bool player_first =
                CompareManager.CompareSkillSpeed(p1.data, p2.data, playerSkill, npcSkill);
            bool attacked_dead;
            bool defender_dead;

            if (player_first)
            {
                (attacked_dead, defender_dead) = await _apply(playerSkill, p1.cricket, p2.cricket); //玩家攻击
                if (attacked_dead && !CanPlayerContinue())
                {
                    return;
                }

                if (defender_dead && !CanNpcContinue())
                {
                    return;
                }

                //npc当前精灵没有GG
                (attacked_dead, defender_dead) = await _apply(npcSkill, p2.cricket, p1.cricket);
                if (attacked_dead && !CanNpcContinue())
                {
                    return;
                }

                if (defender_dead && !CanPlayerContinue())
                {
                    return;
                }
            }
            else
            {
                (attacked_dead, defender_dead) = await _apply(npcSkill, p2.cricket, p1.cricket);
                if (attacked_dead && !CanNpcContinue())
                {
                    return;
                }

                if (defender_dead && !CanPlayerContinue())
                {
                    return;
                }


                (attacked_dead, defender_dead) = await _apply(playerSkill, p1.cricket, p2.cricket);
                if (attacked_dead && !CanPlayerContinue())
                {
                    return;
                }

                if (defender_dead && !CanNpcContinue())
                {
                    return;
                }
            }
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
            await _change_cricket_if_needed(); //回合开始时,如果当前精灵在上一回合被击败 则需要进行替换
            attackPanel.RoundStart(); //显示ui
            await RoundPlaying(); //进行回合
        }

        private async UniTask _change_cricket_if_needed()
        {
            if (p1.cricket.data.healthAbility <= 0)
            {
                //玩家当前的精灵已经GG
                //等待玩家选择新的精灵
                var next_cricket = await _inputHandler.GetPlayerNextCricket();
                print($"我方派{next_cricket.name}上场了");
                p1.ResetData(next_cricket);
                attackPanel.DisConnect();
                attackPanel.Connect(p1.cricket.data, p2.cricket.data);
            }
            else if (p2.cricket.data.healthAbility <= 0)
            {
                //npc精灵GG
                var next_cricket = await _inputHandler.GetNpcNextCricket(_npc);
                print($"敌方派上:{next_cricket.name}");
                p2.ResetData(next_cricket);
                attackPanel.DisConnect();
                attackPanel.Connect(p1.cricket.data, p2.cricket.data);
            }
        }

        #region 胜利/失败逻辑

        #region 判断是否还能继续战斗

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
            //ToDo 这个函数的命名不好
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

        #endregion

        private void ExitAttack()
        {
            UIManager.instance.cricketPanel.DisConnect();
            _attack_over = true;
            Destroy(_npc); //销毁npc游戏物体
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
            //ToDo 暂时这样写 后面修改
            UIManager.instance.cricketPanel.Connect(player);

            attackPanel.Open();
            //获取战斗相机的位置
            _virtualCamera = virtualCamera;
            _player = player;
            _npc = npc;
            _npc.gameObject.SetActive(false); //隐藏npc游戏物体
            //激活他们
            p1.gameObject.SetActive(true);
            p2.gameObject.SetActive(true);
            //
            p1.ResetData(_player.FirstAvailableCricket());
            p2.ResetData(_npc.FirstAvailableCricket());
            attackPanel.Connect(p1.cricket.data, p2.cricket.data); //将数据连接到ui
        }

        #region 场景相关

        private Quaternion previous_rotaion;

        private void _update_rotation()
        {
            var rotation = _virtualCamera.transform.rotation;
            if (rotation == previous_rotaion) return;
            p1.transform.localRotation = rotation;
            p2.transform.localRotation = rotation;
            environment.UpdateRotation(rotation);
            previous_rotaion = rotation;
        }

        #endregion
    }
}