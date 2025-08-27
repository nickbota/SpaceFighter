using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LootTable", menuName = "Space Fighter/Loot Table")]
public class LootTable : ScriptableObject
{
    [Serializable]
    public class Entry
    {
        [Tooltip("Prefab to spawn (heart, powerup, shield, etc.).")]
        public GameObject prefab;

        [Min(0), Tooltip("Relative weight (0 = never; higher = more likely).")]
        public int weight = 1;
    }

    [Tooltip("Weighted list of possible drops.")]
    public List<Entry> entries = new List<Entry>();

    /// <summary>Returns true if at least one entry can be rolled.</summary>
    public bool HasValidEntries()
    {
        foreach (var e in entries)
        {
            if (e != null && e.prefab != null && e.weight > 0) return true;
        }
        return false;
    }

    /// <summary>Pick one entry by weight. Returns null if none valid.</summary>
    public GameObject PickWeighted(System.Random rng = null)
    {
        int total = 0;
        for (int i = 0; i < entries.Count; i++)
        {
            var e = entries[i];
            if (e == null || e.prefab == null || e.weight <= 0) continue;
            total += e.weight;
        }
        if (total <= 0) return null;

        if (rng == null) rng = new System.Random();
        int roll = rng.Next(0, total);
        int cum = 0;

        for (int i = 0; i < entries.Count; i++)
        {
            var e = entries[i];
            if (e == null || e.prefab == null || e.weight <= 0) continue;

            cum += e.weight;
            if (roll < cum)
                return e.prefab;
        }
        return null;
    }
}