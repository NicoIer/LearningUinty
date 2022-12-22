using UnityEngine;

namespace Script.Tools.ObjectPool
{
    public class PoolObject : MonoBehaviour
    {
        //当前是否在对象池中
        public bool pooled;
        //其所在对象池的名称
        public string poolName;
    }
}