using Cinemachine;
using Games.CricketGame.Code.Cricket_;
using Games.CricketGame.Npc_;
using Games.CricketGame.Player_;
using Script.Tools.DesignPattern;
using UnityEngine;

namespace Games.CricketGame.Manager
{
    public class GameManager : Singleton<GameManager>
    {
        public AttackSceneManager attackSceneManager;
        public Transform gameMap;

        #region 游戏状态

        [field: SerializeField] public bool _attcking { get; private set; }
        [field: SerializeField] public bool _mapping { get; private set; }

        #endregion


        [field: SerializeField] public Camera mainCamera;
        [field: SerializeField] public CinemachineVirtualCamera playerCamera;
        [field: SerializeField] public CinemachineVirtualCamera attackCamera;
        private Player _player;
        private Npc npc;
        public void EnterAttackMap(Player player, Npc npc)
        {
            this._player = player;
            this.npc = npc;
            npc.gameObject.SetActive(false);
            _mapping = false;
            _attcking = true;
            ToAttack();
            attackSceneManager.EnterAttack(player, npc,attackCamera);
            
        }

        public void ExitAttackMap()
        {
            Destroy(npc);
            npc = null;
            print("退出战斗,回到大地图");
            _mapping = true;
            _attcking = false;
            ToPlayer();
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