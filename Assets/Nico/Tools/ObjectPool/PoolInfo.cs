using System;
using UnityEngine;

namespace Script.Tools.ObjectPool
{
    /// <summary>
    /// PoolInfo 用于在inspeator面板简单的构建对象池
    /// </summary>
    [Serializable] public struct PoolInfo
    {
        public string pool_name;
        public uint capacity;
        public Transform root;
        public GameObject prefab;
        public uint start_size;
    }
}