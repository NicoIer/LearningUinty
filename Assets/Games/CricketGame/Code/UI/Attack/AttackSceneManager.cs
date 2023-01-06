using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Cysharp.Threading.Tasks;
using Games.CricketGame.Code.Cricket_;
using Games.CricketGame.Code.UI.Attack;
using Games.CricketGame.Cricket_;
using Games.CricketGame.UI;
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

        #region 精灵生成位置

        public Transform p1;
        public Transform p2;

        #endregion

        #region 在场上的精灵

        private Cricket self;
        private Cricket other;

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
            c1.data.RandomInit();
            c1.data.name = "玩家";
            c2.data.RandomInit();
            c2.data.name = "敌人";
            self = c1;
            other = c2;
            _camera = attackCamera;
            var rotation = _camera.transform.rotation;
            self.transform.localRotation = rotation;
            other.transform.localRotation = rotation;
            ui.Initialize(self.data, other.data);
            RoundStart();
        }

        private void Update()
        {
            _update_rotation();
            if (AttackInputHandler.player_input != null)
            {
                AttackInputHandler.other_input = c2.random_skill();
            }

            if (AttackInputHandler.player_input != null && AttackInputHandler.other_input != null)
            {
                print("启动回合进行时");
                RoundPlaying();
                print("启动完成");
            }
        }

        #endregion

        public void EnterAttack(Cricket cricket1, Cricket cricket2, CinemachineVirtualCamera virtualCamera)
        {
            //获取战斗相机的位置
            _camera = virtualCamera;
            //将数据传递给UI 
            ui.Initialize(cricket1.data, cricket2.data);
            //在对应位置生成精灵
            //ToDO 调试时 暂时不要这个
            self = Instantiate(cricket1, p1);
            other = Instantiate(cricket2, p2);
            self.gameObject.SetActive(true);
            other.gameObject.SetActive(true);

            //将精灵看向相机
            self.transform.localRotation = _camera.transform.rotation;
            other.transform.localRotation = _camera.transform.rotation;
        }


        #region 回合逻辑

        private void RoundStart()
        {
            state = 0; //变更当前回合状态
            ui.RoundStart(); //
            //玩家输入交给UI
        }
        

        private void RoundPlaying()
        {
            state = 1;
            // ui.RoundPlaying();
            var s1 = AttackInputHandler.player_input;
            var s2 = AttackInputHandler.other_input;
            AttackInputHandler.player_input = null;
            AttackInputHandler.other_input = null;
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
                print("玩家先手");
                _apply_skill(c1, c2, s1);
                _apply_skill(c2, c1, s2);
            }
            else
            {
                print("对方先手");
                _apply_skill(c2, c1, s2);
                _apply_skill(c1, c2, s1);
            }

            RoundEnd();
        }

        private void _apply_skill(Cricket attacker, Cricket defenser, Skill skill)
        {
            skill.Apply(attacker, defenser);
            print($"{attacker.data.name}->{defenser.data.name}计算完毕");
        }

        private void RoundEnd()
        {
            print("回合结束");
            state = 3;
        }

        #endregion


        private void _update_rotation()
        {
            var rotation = _camera.transform.rotation;
            self.transform.localRotation = rotation;
            other.transform.localRotation = rotation;
        }
    }
}