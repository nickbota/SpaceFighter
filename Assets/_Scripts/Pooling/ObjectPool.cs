using UnityEngine;
using Zenject;
using System.Collections.Generic;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private int poolSize = 20;

    private Queue<GameObject> pool = new Queue<GameObject>();

    private GameManager gameManager;
    [Inject]
    private void Init(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }

    private void Awake()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(prefab, transform);
            obj.SetActive(false);
            pool.Enqueue(obj);
        }
    }

    public GameObject GetFromPool(Vector3 position, Quaternion rotation)
    {
        GameObject obj = pool.Count > 0 ? pool.Dequeue() : Instantiate(prefab);

        obj.transform.position = position;
        obj.transform.rotation = rotation;
        obj.SetActive(true);

        Bullet bullet = obj.GetComponent<Bullet>();
        bullet.GetComponent<PausableObject>().SetGameManager(gameManager);
        bullet?.Initialize(this);

        return obj;
    }

    public void ReturnToPool(GameObject obj)
    {
        obj.SetActive(false);
        pool.Enqueue(obj);
    }
}
