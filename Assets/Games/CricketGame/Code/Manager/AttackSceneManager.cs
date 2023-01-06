using System;
using Cinemachine;
using Games.CricketGame.Manager.Code.Pokemon;
using UnityEngine;

namespace Games.CricketGame.Manager.Code.UI
{
    /// <summary>
    /// 战斗管理 
    /// </summary>
    public class AttackSceneManager : MonoBehaviour
    {
        //ToDo 还有战斗场景地图啥的 没有设计 
        public Transform p1;
        public Transform p2;
        private Cricket self;
        private Cricket other;
        public AttackPanel ui;
        private CinemachineVirtualCamera _camera;

        #region debug

        [field: SerializeField] public CinemachineVirtualCamera attackCamera;
        public Cricket c1;
        public Cricket c2;

        #endregion

        private void Awake()
        {
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
            c2.data.RandomInit();
            self = c1;
            other = c2;
            _camera = attackCamera;
            self.transform.localRotation = _camera.transform.rotation;
            other.transform.localRotation = _camera.transform.rotation;
            ui.Initialize(self.data, other.data);
        }

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

        private void Update()
        {
            self.transform.localRotation = _camera.transform.rotation;
            other.transform.localRotation = _camera.transform.rotation;
        }
    }
}