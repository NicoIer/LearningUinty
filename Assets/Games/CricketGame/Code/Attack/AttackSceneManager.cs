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
using Games.CricketGame.UI.Attack;
using UnityEngine;

namespace Games.CricketGame.Manager
{
    /// <summary>
    /// 战斗管理 
    /// </summary>
    public class AttackSceneManager : MonoBehaviour
    {
        //ToDo 还有战斗场景地图啥的 没有设计 
        [Range(0, 1000)] public int consoleDelayTimes;
        private bool _round_start; //回合是否开始
        private bool _attack_over = true; //战斗是否结束
        private Player _player; //玩家是需要持有的
        private Npc _npc;
        private CinemachineVirtualCamera _virtualCamera;
        private AttackPanel ui => UIManager.instance.attackPanel;
        public AttackEnvironment environment;
        [field: SerializeField] public AttackPosition pPlayer { get; private set; }
        [field: SerializeField] public AttackPosition pNpc { get; private set; }
        private AttackInputHandler _inputHandler;

        #region Unity CallBack

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

        #endregion


        #region 战斗逻辑

        private async UniTask<(AttackInputStruct, AttackInputStruct)> _get_attack_input()
        {
            var s1 = await _inputHandler.GetPlayerInput();
            var s2 = new AttackInputStruct(SelectTypeEnum.使用技能, typeof(Skill), _npc.random_skill(pNpc.cricket.data));
            return (s1, s2);
        }

        private async UniTask RoundPlaying()
        {
            //ToDo 这里有很多种不同的输入可能 后面来考虑
            //等待输入
            var (playerInput, npcInput) = await _get_attack_input();
            ui.RoundPlaying();
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
                        if (CompareManager.CompareNpcRun(pPlayer.data, pNpc.data))
                        {
                            await PlayerWin();
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
                if (CompareManager.ComparePlayerRun(_npc, pPlayer.data, pNpc.data))
                {
                    //判断玩家是否能够逃跑 ToDo 当面对特定NPC时不可以逃跑
                    await PlayerLose();
                    return;
                }

                //没有 跑掉
                switch (npcInput.typeEnum)
                {
                    case SelectTypeEnum.使用技能:
                        var skill = AttackInputStruct.CastToType<Skill>(npcInput);
                        await _npc_attack(pNpc.cricket, pPlayer.cricket, skill);
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

        private async UniTask _npc_attack(Cricket attacker, Cricket defender, Skill skill)
        {
            //Npc释放技能攻击
            bool attacker_dead;
            bool defender_dead;
            
            (attacker_dead, defender_dead) = await _apply(skill, attacker, defender); //玩家攻击
            if ((attacker_dead && !await CanNpcContinue()) || (defender_dead && !await CanPlayerContinue()))
            {
                //有一方不能够再战斗
            }
        }

        private async UniTask _attack_together(Skill playerSkill, Skill npcSkill)
        {//ToDo 这里肉眼已经看不清晰了 ...优化它 拆分成小函数
            bool player_first =
                CompareManager.CompareSkillSpeed(pPlayer.data, pNpc.data, playerSkill, npcSkill);
            bool attacked_dead;
            bool defender_dead;

            if (player_first)
            {
                await ui.UpdateConsoleText($"{pPlayer.cricket.data.name}使用技能{playerSkill.meta.name}",
                    consoleDelayTimes);
                (attacked_dead, defender_dead) = await _apply(playerSkill, pPlayer.cricket, pNpc.cricket); //玩家攻击
                if (attacked_dead && !await CanPlayerContinue())
                {
                    return;
                }

                if (defender_dead)
                {
                    await CanNpcContinue();
                    return;
                }

                //npc当前精灵没有GG
                await ui.UpdateConsoleText($"{pNpc.cricket.data.name}使用技能{npcSkill.meta.name}", consoleDelayTimes);
                (attacked_dead, defender_dead) = await _apply(npcSkill, pNpc.cricket, pPlayer.cricket);
                if (attacked_dead && !await CanNpcContinue())
                {
                    return;
                }

                if (defender_dead && !await CanPlayerContinue())
                {
                }
            }
            else
            {
                await ui.UpdateConsoleText($"{pNpc.cricket.data.name}使用技能{npcSkill.meta.name}", consoleDelayTimes);
                (attacked_dead, defender_dead) = await _apply(npcSkill, pNpc.cricket, pPlayer.cricket);

                if (attacked_dead && !await CanNpcContinue())
                {
                    return;
                }

                if (defender_dead)
                {
                    await CanPlayerContinue();
                    return;
                }


                (attacked_dead, defender_dead) = await _apply(playerSkill, pPlayer.cricket, pNpc.cricket);
                await ui.UpdateConsoleText($"{pPlayer.cricket.data.name}使用技能{playerSkill.meta.name}",consoleDelayTimes);
                if (attacked_dead && !await CanPlayerContinue())
                {
                    return;
                }

                if (defender_dead && !await CanNpcContinue())
                {
                }
            }
        }
        
        /// <summary>
        /// 应用技能效果
        /// </summary>
        /// <param name="skill">使用的技能</param>
        /// <param name="user">使用者</param>
        /// <param name="defender">防御者</param>
        /// <returns>使用者死亡,防御者死亡</returns>
        private async UniTask<(bool, bool)> _apply(Skill skill, Cricket user, Cricket defender)
        {
            await skill.Apply(user, defender);
            var (defender_dead, attacker_dead) = (false, false);
            if (defender.data.healthAbility <= 0)
            {
                defender.Dead();
                defender_dead = true;
            }

            if (user.data.healthAbility <= 0)
            {
                user.Dead();
                attacker_dead = true;
            }

            return (attacker_dead, defender_dead);
        }

        private async UniTask MainLogic()
        {
            await _change_cricket_if_needed(); //回合开始时,如果当前精灵在上一回合被击败 则需要进行替换
            ui.RoundStart(); //显示ui
            await RoundPlaying(); //进行回合
        }

        private async UniTask _change_cricket_if_needed()
        {
            if (pPlayer.cricket.data.healthAbility <= 0)
            {
                //玩家当前的精灵已经GG
                //等待玩家选择新的精灵
                var next_cricket = await _inputHandler.GetPlayerNextCricket();
                await ui.UpdateConsoleText($"去吧!! {next_cricket.name}", consoleDelayTimes);
                pPlayer.ResetData(next_cricket);
                ui.DisConnect();
                ui.Connect(pPlayer.cricket.data, pNpc.cricket.data);
            }
            else if (pNpc.cricket.data.healthAbility <= 0)
            {
                //npc精灵GG
                var next_cricket = await _inputHandler.GetNpcNextCricket(_npc);
                await ui.UpdateConsoleText($"敌方派上:{next_cricket.name}", consoleDelayTimes);
                pNpc.ResetData(next_cricket);
                ui.DisConnect();
                ui.Connect(pPlayer.cricket.data, pNpc.cricket.data);
            }
        }

        #region 胜利/失败逻辑

        #region 判断是否还能继续战斗

        /// <summary>
        /// 判断玩家(判断是否还有血量>0的cricket)是否还能继续战斗 
        /// </summary>
        /// <returns>能:true,不能:false</returns>
        private async UniTask<bool> CanPlayerContinue()
        {
            await ui.UpdateConsoleText($"{pPlayer.cricket.name}倒下了", consoleDelayTimes);
            if (_player.HavaAvailableCricket())
            {
                return true;
            }

            await ui.UpdateConsoleText($"cricket lost", consoleDelayTimes);
            await PlayerLose();
            return false;
        }

        private async UniTask<bool> CanNpcContinue()
        {
            await ui.UpdateConsoleText($"{pNpc.cricket.name}倒下了", consoleDelayTimes);
            if (_npc.HaveAvailableCricket())
            {
                return true;
            }

            await ui.UpdateConsoleText($"{pNpc.name}战败了", consoleDelayTimes);
            await PlayerWin();
            return false;
        }

        #endregion

        #region 战斗结束

        private async UniTask PlayerLose()
        {
            await ui.UpdateConsoleText($"YouLost!", consoleDelayTimes);
            _attack_over = true;
            _round_start = false;
            ExitAttack();
        }

        private async UniTask PlayerWin()
        {
            await ui.UpdateConsoleText($"You Win", consoleDelayTimes);
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
            ui.DisConnect();
            ui.Close();
            pPlayer.ResetData(null);
            pNpc.ResetData(null);
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

            ui.Open();
            //获取战斗相机的位置
            _virtualCamera = virtualCamera;
            _player = player;
            _npc = npc;
            _npc.gameObject.SetActive(false); //隐藏npc游戏物体
            //激活他们
            pPlayer.gameObject.SetActive(true);
            pNpc.gameObject.SetActive(true);
            //
            pPlayer.ResetData(_player.FirstAvailableCricket());
            pNpc.ResetData(_npc.FirstAvailableCricket());
            ui.Connect(pPlayer.cricket.data, pNpc.cricket.data); //将数据连接到ui
        }

        #region 场景相关

        private Quaternion _previousRotation;

        private void _update_rotation()
        {
            var rotation = _virtualCamera.transform.rotation;
            if (rotation == _previousRotation) return;
            pPlayer.transform.localRotation = rotation;
            pNpc.transform.localRotation = rotation;
            environment.UpdateRotation(rotation);
            _previousRotation = rotation;
        }

        #endregion
    }
}