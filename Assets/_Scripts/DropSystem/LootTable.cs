using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LootTable", menuName = "Space Fighter/Loot Table")]
public class LootTable : ScriptableObject
{
    [Serializable]
    public class Entry
    {
        public GameObject prefab;

        [Min(0), Tooltip("Relative weight (0 = never; higher = more likely).")]
        public int weight = 1;
    }

    [SerializeField] private List<Entry> entries = new List<Entry>();

    public bool HasValidEntries()
    {
        foreach (var e in entries)
            if (e != null && e.prefab != null && e.weight > 0) return true;

        return false;
    }

    public GameObject PickWeighted(System.Random rng = null)
    {
        //Calculate total weight of all items
        int total = 0;
        for (int i = 0; i < entries.Count; i++)
        {
            var e = entries[i];
            if (e == null || e.prefab == null || e.weight <= 0) continue;
            total += e.weight;
        }
        if (total <= 0) return null;

        //Generate a random number and check which item needs to be spawned
        if (rng == null) rng = new System.Random();
        int roll = rng.Next(0, total);
        int cumulativeWeight = 0;

        for (int i = 0; i < entries.Count; i++)
        {
            var e = entries[i];
            if (e == null || e.prefab == null || e.weight <= 0) continue;

            cumulativeWeight += e.weight;
            if (roll < cumulativeWeight)
                return e.prefab;
        }
        return null;
    }
}