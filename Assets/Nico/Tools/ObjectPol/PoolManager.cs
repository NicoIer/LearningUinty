using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Script.Tools.ObjectPool
{
    /// <summary>
    /// 对象池管理器
    /// 是一个单例,管理给定的所有Pool
    /// </summary>
    public class PoolManager : DesignPattern.Singleton<PoolManager>
    {
        /// <summary>
        /// 对象池存储
        /// </summary>
        private Dictionary<string, Pool> _pools = new();

        /// <summary>
        /// 对象池初始化
        /// </summary>
        [SerializeField] private List<PoolInfo> infos;

        #region UnityCallBack

        protected override void Awake()
        {
            base.Awake();
            //对每个在inspector中注册的pool,进行实例化
            foreach (var poolInfo in infos)
            {
                var pool = new Pool(poolInfo.pool_name, poolInfo.root, poolInfo.prefab, poolInfo.capacity,
                    poolInfo.start_size);
                AddPool(poolInfo.pool_name, pool);
            }
        }

        #endregion

        #region PoolFunctions

        public bool AddPool(string pool_name, Pool pool)
        {
            if (_pools.ContainsKey(pool_name))
            {
                Debug.LogWarning($"the pool_name:{pool_name} has been assigned to poolManager try other name");
                return false;
            }

            _pools.Add(pool_name, pool);
            return true;
        }

        public bool DeletePool(string pool_name)
        {
            if (_pools.ContainsKey(pool_name))
            {
                return _pools.Remove(pool_name);
            }

            Debug.LogWarning($"the pool_name:{pool_name} has not been assigned");
            return false;
        }

        public Pool PopPool(string pool_name)
        {
            _pools.TryGetValue(pool_name, out var pool);
            if (pool == null)
            {
                Debug.LogWarning($"there is no pool named:{pool_name}");
                return null;
            }
            else
            {
                _pools.Remove(pool_name);
                return pool;
            }
        }

        /// <summary>
        /// 从指定对象池中 获取 一个可用的对象
        /// </summary>
        /// <param name="pool_name"></param>
        /// <returns></returns>
        /// <exception cref="KeyNotFoundException"></exception>
        public PoolObject GetObject(string pool_name, Vector3 position, Quaternion quaternion)
        {
            if (_pools.ContainsKey(pool_name))
            {
                return _pools[pool_name].GetObject(position, quaternion);
            }

            throw new KeyNotFoundException($"the pool {pool_name} can't be found!!");
        }

        public bool ReturnObject(string pool_name, PoolObject poolObject)
        {
            if (_pools.ContainsKey(pool_name))
            {
                return _pools[pool_name].ReturnObject(poolObject);
            }

            throw new KeyNotFoundException($"the pool {pool_name} can't be found!!");
        }

        #endregion
    }
}