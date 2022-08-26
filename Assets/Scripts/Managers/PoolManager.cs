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
        public GameObject GetObject(bool autoActivate = true)
        {
            GameObject obj;
            foreach (Transform child in DoStatic.GetChildren(pool))
            {
                obj = child.gameObject;
                if (!obj.activeInHierarchy)
                {
                    obj.SetActive(autoActivate);
                    return obj;
                }
            }
            obj = Instantiate(prefab, pool);
            obj.SetActive(autoActivate);
            return obj;
        }

        /// <summary>
        /// Add items into the pool.
        /// </summary>
        /// <param name="item">The prefab object.</param>
        public void AddToPool(GameObject item)
        {
            item.transform.parent = pool.transform;
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
    public GameObject GetObjectFromPool(string poolName, bool autoActive = true)
    {
        return pools[poolName].GetObject(autoActive);
    }

    /// <summary>
    /// Get a component from a GameObject from the specified pool.
    /// </summary>
    /// <param name="poolName">The pool the GameObject is from.</param>
    /// <param name="autoActive">Activate given GameObject or not? Defaults to true.</param>
    /// <returns>The component of the game GameObject from the specified pool.</returns>
    public T GetObjectFromPool<T>(string poolName, bool autoActive = true) where T : Component
    {
        return pools[poolName].GetObject(autoActive).GetComponentInChildren<T>();
    }

    /// <summary>
    /// Add an GameObject into a specified pool.
    /// </summary>
    /// <param name="poolName">The pool to add the GameObject into.</param>
    /// <param name="gameObject">The GameObject to be added into the pool.</param>
    public void AddObjectIntoPool(string poolName, GameObject gameObject)
    {
        pools[poolName].AddToPool(gameObject);
    }
}