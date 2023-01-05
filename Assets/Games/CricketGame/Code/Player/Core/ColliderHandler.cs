using UnityEngine;

namespace Games.CricketGame.Manager.Code.Player.Core
{
    public class ColliderHandler
    {
        private Player _player;

        public ColliderHandler(Player player)
        {
            _player = player;
            player.triggerEnter2D += OnTriggerEnter2D;
            player.triggerExit2D += OnTriggerExit2D;
            player.collisionEnter2D += OnCollisionEnter2D;
            player.collisionExit2D += OnCollisionExit2D;
        }

        #region Collider2D Event

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.collider.CompareTag("Cricket"))
            {
                Debug.Log("遭遇野生精灵,现在要进入战斗画面");
                Nico.Scene.CameraManager.Instance.ToAttack();
            }
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            if (other.collider.CompareTag("Cricket"))
            {
                Debug.Log("离开野生精灵");
            }
        }

        #endregion

        #region Trigger Event

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("Cricket"))
            {
                Debug.Log("遭遇野生精灵");
            }
        }


        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Cricket"))
            {
                Debug.Log("离开野生精灵");
            }
        }

        #endregion
    }
}