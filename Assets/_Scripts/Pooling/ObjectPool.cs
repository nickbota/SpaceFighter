using UnityEngine;
using Zenject;
using System.Collections.Generic;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private int poolSize = 20;
    private List<GameObject> pooledObjects = new List<GameObject>();

    private GameManager gameManager;
    [Inject]
    private void Init(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }

    public GameObject GetFromPool(Vector3 position, Quaternion rotation)
    {
        GameObject objectToReturn = null;

        //If too many projectiles return the first one
        if (pooledObjects.Count >= poolSize)
        {
            pooledObjects[0].SetActive(false);
            objectToReturn = pooledObjects[0];
        }
        else
        {
            bool newObjectNeeded = true;

            //If any object available return it
            foreach (var item in pooledObjects)
            {
                if (!item.gameObject.activeInHierarchy)
                {
                    objectToReturn = item;
                    newObjectNeeded = false;
                }
            }

            //Else create new object and return it
            if (newObjectNeeded)
            {
                GameObject newObject = Instantiate(prefab, position, rotation, transform);
                if (newObject.TryGetComponent(out Bullet bullet))
                {
                    //If any object available return it
                    foreach (var item in pooledObjects)
                    {
                        bullet = newObject.GetComponent<Bullet>();
                        bullet.GetComponent<PausableObject>().SetGameManager(gameManager);
                        bullet?.Initialize(this);
                    }
                }
                objectToReturn = newObject;
            }
        }

        objectToReturn.transform.position = position;
        objectToReturn.transform.rotation = rotation;
        objectToReturn.SetActive(true);

        if(!pooledObjects.Contains(objectToReturn) )
            pooledObjects.Add(objectToReturn);

        return objectToReturn;
    }

    public void ReturnToPool(GameObject obj)
    {
        obj.SetActive(false);
    }
}