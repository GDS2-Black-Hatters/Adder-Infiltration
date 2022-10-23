using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is meant for Unity Inspector use.
/// Since Unity can's serialise dictionaries, this is an alternative to it.
/// </summary>
[System.Serializable]
public class FakeDictionary<key, value>
{
    [System.Serializable]
    private class Item
    {
        public key key;
        public value value;
    }
    [SerializeField] private Item[] items;
    [NonSerialized] private Dictionary<key, value> dictionary;

    /// <summary>
    /// Be sure to call this on Awake/Start.
    /// Should only be called once.
    /// </summary>
    /// <returns>The dictionary from the inspector. A warning will be produced if the key exists during runtime.</returns>
    public Dictionary<key, value> ToDictionary()
    {
        if (dictionary == null)
        {
            dictionary = new();
            foreach (Item item in items)
            {
                if (dictionary.ContainsKey(item.key))
                {
                    Debug.LogError(item.key + " already exists in the dictionary!");
                    continue;
                }
                dictionary.Add(item.key, item.value);
            }
        }
        return dictionary;
    }
}
