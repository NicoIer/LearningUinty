using Cysharp.Threading.Tasks;
using Games.CricketGame.Npc_;
using Games.CricketGame.Player_;
using Script.Tools.DesignPattern;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Games.CricketGame.Manager
{


    public class GameManager : Singleton<GameManager>
    {
        public AttackSceneManager attackSceneManager;
        public Transform gameMap;
        [field: SerializeField]public GameInputHandler handler { get; private set; }

        #region 游戏状态

        [field: SerializeField] public bool _attcking { get; private set; }
        [field: SerializeField] public bool _mapping { get; private set; }

        #endregion

        protected override void Awake()
        {
            base.Awake();
            handler = transform.GetChild(0).GetComponent<GameInputHandler>();
        }

        private Npc npc;

        public void EnterAttackMap(Player player, Npc npc)
        {
            this.npc = npc;
            npc.gameObject.SetActive(false);
            _mapping = false;
            _attcking = true;
            ToAttack();
            attackSceneManager.EnterAttack(player, npc, CameraManager.instance.attackCamera);
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

        private void ToPlayer()
        {
            attackSceneManager.gameObject.SetActive(false);
            gameMap.gameObject.SetActive(true);
            CameraManager.instance.SwitchToPlayerCamera(false);
        }

        private void ToAttack()
        {
            attackSceneManager.gameObject.SetActive(true);
            gameMap.gameObject.SetActive(false);
            CameraManager.instance.SwitchToAttackCamera(false);
        }
    }
}