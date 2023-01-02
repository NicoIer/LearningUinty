using UnityEngine;

namespace PackageGame.UI
{
    public class FloatPanel : MonoBehaviour
    {
        public void SetPosition(Vector3 pos)
        {
            transform.localPosition = pos;
        }
    }
}