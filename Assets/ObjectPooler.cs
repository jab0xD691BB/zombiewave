using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjectPoolItem
{
    public int amountToPool;
    public GameObject objectToPool;
    public bool shouldExpand;


}


public class ObjectPooler : MonoBehaviour
{

    public static ObjectPooler SharedInstance;

    /*public List<GameObject> pooledObjects;
    public GameObject objectToPoolGun;
    public int amountToPool;*/
    public List<GameObject> pooledObjects;
    public List<ObjectPoolItem> itemsToPool;


    private void Awake()
    {
        SharedInstance = this;
    }

    private void Start()
    {
        pooledObjects = new List<GameObject>();
        foreach(ObjectPoolItem item in itemsToPool)
        {
            for(int i=0; i < item.amountToPool; i++)
            {
                GameObject obj = (GameObject)Instantiate(item.objectToPool);
                obj.SetActive(false);
                pooledObjects.Add(obj);
            }
        }
        /*for(int i = 0; i < amountToPool; i++)
        {
            GameObject obj = (GameObject)Instantiate(objectToPoolGun);
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }*/
    }

    public GameObject GetPooledObject(string tag)
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy && pooledObjects[i].tag == tag)
            {
                return pooledObjects[i];
            }
        }

        /*foreach(ObjectPoolItem item in itemsToPool)
        {
            if(item.objectToPool.name == name)
            {

            }
        }*/

        return null;
    }

}
