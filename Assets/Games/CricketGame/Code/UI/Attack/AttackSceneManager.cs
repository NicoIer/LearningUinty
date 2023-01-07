using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Cysharp.Threading.Tasks;
using Games.CricketGame.Code.Cricket_;
using Games.CricketGame.Code.UI.Attack;
using Games.CricketGame.Cricket_;
using Games.CricketGame.UI;
using Unity.VisualScripting;
using UnityEngine;

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
        public static AttackSceneManager instance;

        //ToDo 还有战斗场景地图啥的 没有设计 
        public int state;
        public bool round_start = false;

        #region 精灵生成位置

        public Transform p1;
        public Transform p2;

        #endregion

        #region 在场上的精灵

        private Cricket _self;
        private Cricket _other;

        #endregion

        #region ui

        public AttackPanel ui;

        #endregion

        #region 摄像机

        private CinemachineVirtualCamera _camera;

        #endregion

        #region debug

        [field: SerializeField] public CinemachineVirtualCamera attackCamera;
        public Cricket c1;
        public Cricket c2;

        #endregion


        #region Unity LifeTime

        private void Awake()
        {
            if (instance != null)
            {
                Debug.LogWarning("战斗场景管理器只能有一个!!!");
            }

            instance = this;

            if (p1 == null)
            {
                p1 = transform.Find("p1");
            }

            if (p2 == null)
            {
                p2 = transform.Find("p2");
            }
        }

        private void Start()
        {
            //ToDO 暂时这样用于调试 后续修改
            c1.data.RandomInit(40);
            c1.data.name = "玩家";
            c2.data.RandomInit(40);
            c2.data.name = "敌人";
            _self = c1;
            _other = c2;
            _camera = attackCamera;
            var rotation = _camera.transform.rotation;
            _self.transform.localRotation = rotation;
            _other.transform.localRotation = rotation;
            ui.Initialize(_self.data, _other.data);
        }

        private void Update()
        {
            _update_rotation(); //更新场景内的2D物体,使面朝相机
            if (!round_start)
            {
                print("开启UniTask执行回合逻辑");
                //如果回合没有开始 则开始回合
                MainLogic().Forget();
                round_start = true;
            }
        }

        #endregion

        private async UniTask MainLogic()
        {
            
            ui.RoundStart(); //显示ui
            var s1 = await PlayerSelect(); //等待玩家输入
            var s2 = NpcSelect(); //Npc立即选择一个输入
            ui.RoundPlaying();
            bool player_first = false;
            if (PriorityCompare.Compare(s1.meta.priority, s2.meta.priority))
            {
                //技能优先级高?
                player_first = true;
            }
            else if (c1.data.speedAbility > c2.data.speedAbility)
            {
                //速度快?
                player_first = true;
            }

            if (player_first)
            {
                await s1.Apply(_self, _other);
                print("玩家攻击完毕");
                await UniTask.Delay(3000);
                await s2.Apply(_other, _self);
                print("敌人攻击完毕");
            }
            else
            {
                await s2.Apply(_other, _self);
                print("敌人攻击完毕");
                await UniTask.Delay(3000);
                await s1.Apply(_self, _other);
                print("玩家攻击完毕");
            }

            round_start = false;
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
            return c2.random_skill();
        }

        public void EnterAttack(Cricket cricket1, Cricket cricket2, CinemachineVirtualCamera virtualCamera)
        {
            //获取战斗相机的位置
            _camera = virtualCamera;
            //将数据传递给UI 
            ui.Initialize(cricket1.data, cricket2.data);
            //在对应位置生成精灵
            //ToDO 调试时 暂时不要这个
            _self = Instantiate(cricket1, p1);
            _other = Instantiate(cricket2, p2);
            _self.gameObject.SetActive(true);
            _other.gameObject.SetActive(true);

            //将精灵看向相机
            _self.transform.localRotation = _camera.transform.rotation;
            _other.transform.localRotation = _camera.transform.rotation;
        }

        private void _update_rotation()
        {
            var rotation = _camera.transform.rotation;
            _self.transform.localRotation = rotation;
            _other.transform.localRotation = rotation;
        }
    }
}