using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour, IManager
{
    [System.Serializable]
    private class PrefabPool
    {
        public string poolName; //The name of the pool
        public GameObject prefab; //The prefab for cloning
        private Transform pool; //The GameObject where all the clones of the prefab will go under.

        /// <summary>
        /// Should be called only once. Set the pool GameObject
        /// </summary>
        /// <param name="pool"></param>
        public void SetPool(Transform pool)
        {
            this.pool = pool;
        }

        /// <summary>
        /// Get a GameObject in the pool.
        /// </summary>
        /// <param name="autoActivate">Activate the GameObject upon return? Defaults to true.</param>
        /// <returns>A GameObject from the pool.</returns>
        public GameObject GetObject()
        {
            for (int i = 0; i < pool.childCount; i++)
            {
                GameObject obj = pool.GetChild(i).gameObject;
                if (!obj.activeInHierarchy)
                {
                    obj.SetActive(true);
                    return obj;
                }
            }
            return Instantiate(prefab, pool);
        }
    }

    [SerializeField] private PrefabPool[] prefabPools;
    private GameObject poolManager;
    private readonly Dictionary<string, PrefabPool> pools = new();

    public void StartUp()
    {
        poolManager = new("PoolManager");
        poolManager.transform.parent = transform;

        foreach (PrefabPool pool in prefabPools)
        {
            Transform obj = new GameObject(pool.poolName).transform;
            obj.parent = poolManager.transform;
            pool.SetPool(obj);
            pools.Add(pool.poolName, pool);
        }
    }

    /// <summary>
    /// Get a GameObject from the specified pool.
    /// </summary>
    /// <param name="poolName">The pool the GameObject is from.</param>
    /// <param name="autoActive">Activate given GameObject or not? Defaults to true.</param>
    /// <returns>The GameObject from the specified pool.</returns>
    public GameObject GetObjectFromPool(string poolName)
    {
        return pools[poolName].GetObject();
    }

    /// <summary>
    /// Get a component from a GameObject from the specified pool.
    /// </summary>
    /// <param name="poolName">The pool the GameObject is from.</param>
    /// <param name="autoActive">Activate given GameObject or not? Defaults to true.</param>
    /// <returns>The component of the game GameObject from the specified pool.</returns>
    public T GetObjectFromPool<T>(string poolName) where T : Component
    {
        return pools[poolName].GetObject().GetComponentInChildren<T>();
    }
}