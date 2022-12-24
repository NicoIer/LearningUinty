using UnityEngine;

namespace AttackGame.UI
{
    public class FloatPanel : MonoBehaviour
    {
        public void SetPosition(Vector3 pos)
        {
            transform.localPosition = pos;
        }
    }
}