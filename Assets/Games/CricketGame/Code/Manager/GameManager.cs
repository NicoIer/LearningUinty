using Cinemachine;
using Games.CricketGame.Code.Cricket_;
using Games.CricketGame.Player_;
using Script.Tools.DesignPattern;
using UnityEngine;

namespace Games.CricketGame.Manager
{
    public class GameManager : Singleton<GameManager>
    {
        public AttackSceneManager attackSceneManager;
        public Transform gameMap;
        public bool _attcking { get; private set; }
        [field: SerializeField] public Camera mainCamera;
        [field: SerializeField] public CinemachineVirtualCamera playerCamera;
        [field: SerializeField] public CinemachineVirtualCamera attackCamera;

        private void Start()
        {
            DebugAttack();
        }

        private void DebugAttack()
        {
        }

        /// <summary>
        /// 玩家和野生精灵遭遇 进入战斗
        /// </summary>
        /// <param name="player"></param>
        /// <param name="cricket"></param>
        public void EnterAttackMap(Player player, Cricket cricket)
        {
            _attcking = true;
            ToAttack();
        }

        public void ExitAttackMap()
        {
            _attcking = false;
        }
        public void ToPlayer()
        {
            attackSceneManager.gameObject.SetActive(false);
            gameMap.gameObject.SetActive(true);
            attackCamera.enabled = false;
            attackCamera.gameObject.SetActive(false);
            playerCamera.enabled = true;
            playerCamera.gameObject.SetActive(true);
            mainCamera.orthographic = true;
        }

        public void ToAttack()
        {
            attackSceneManager.gameObject.SetActive(true);
            gameMap.gameObject.SetActive(false);
            playerCamera.enabled = false;
            playerCamera.gameObject.SetActive(false);
            attackCamera.enabled = true;
            attackCamera.gameObject.SetActive(true);
            mainCamera.orthographic = false;
        }
    }
}