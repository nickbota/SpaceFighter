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
        GameObject obj = pool.Count > 0 ? pool.Dequeue() : Instantiate(prefab, transform);

        obj.transform.position = position;
        obj.transform.rotation = rotation;

        //If the object is a bullet set up the game manager
        if (obj.TryGetComponent(out Bullet bullet))
        {
            bullet = obj.GetComponent<Bullet>();
            bullet.GetComponent<PausableObject>().SetGameManager(gameManager);
            bullet?.Initialize(this);
        }

        obj.SetActive(true);

        return obj;
    }

    public void ReturnToPool(GameObject obj)
    {
        obj.SetActive(false);
        pool.Enqueue(obj);
    }
}
