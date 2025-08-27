using UnityEngine;

public class DropSpawner : MonoBehaviour
{
    [Header("Table & Chance")]
    [SerializeField] private LootTable lootTable;
    [Range(0f, 1f)][SerializeField] private float dropChance = 0.35f;
    private System.Random rng;

    private void Awake()
    {
        rng = new System.Random();
    }

    public void TryDrop()
    {
        if (lootTable == null || !lootTable.HasValidEntries())
            return;

        float finalChance = Mathf.Clamp01(dropChance);
        if (Random.value > finalChance) return;

        var prefab = lootTable.PickWeighted(rng);
        Vector3 pos = transform.position;
        GameObject go = Instantiate(prefab, pos, Quaternion.identity);
    }
}