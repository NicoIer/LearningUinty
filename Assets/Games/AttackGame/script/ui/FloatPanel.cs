using UnityEngine;

namespace AttackGame.script.ui
{
    public class FloatPanel : MonoBehaviour
    {
        public void SetPosition(Vector3 pos)
        {
            transform.localPosition = pos;
        }
    }
}