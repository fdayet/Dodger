using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// This class holds the base properties of the object pool.
/// Currently it stores name, prefab and size of the object pool. 
/// </summary>
[System.Serializable]
public class ObjectPool
{
    public string Name;
    public GameObject prefab;
    public int Size;
}

public class ObjectPoolManager : MonoBehaviour
{
    // Object Pool list. It is filled in editor. The same can be initialised with code. 
    public List<ObjectPool> ObjectPoolList;
    public static bool ready = false;
    // store all object pools by its name.
    private static Dictionary<string, Queue<GameObject>> objectPoolDictionary;

    /// <summary>
    /// Initialize the object pool. Here the objects are instantiated and stored in the 
    /// respective object pool.
    /// </summary> 
    public void Start()
    {
        objectPoolDictionary = new Dictionary<string, Queue<GameObject>>();
        foreach (ObjectPool pool in ObjectPoolList)
        {
            // Create a empty parent object for each object pool
            // This is optional and depends on your requirement. 
            GameObject poolParentObj = new GameObject();
            poolParentObj.name = pool.Name + "Pool";
            GameObject.DontDestroyOnLoad(poolParentObj);

            // create object pool and store objects to it
            Queue<GameObject> poolQueue = new Queue<GameObject>();
            for (int i = 0; i < pool.Size; i++)
            {
                GameObject obj = GameObject.Instantiate(pool.prefab);
                obj.SetActive(false);
                obj.transform.parent = poolParentObj.transform;
                //PooledObjectBehaviour pooledObjectBehaviour = obj.AddComponent<PooledObjectBehaviour>();
                //pooledObjectBehaviour.Pool = pool;
                poolQueue.Enqueue(obj);
            }
            // add object pool to the dictionary  
            objectPoolDictionary.Add(pool.Name, poolQueue);
        }
        ready = true;

    }

    /// <summary>
    /// Get object from the object pool
    /// Sets the position and rotation of object
    /// </summary>
    public static GameObject GetObjectFromPool(string poolName, Vector3 position, Quaternion rotation)
    {
        GameObject obj = null;
        if (objectPoolDictionary.ContainsKey(poolName))
        {
            if (objectPoolDictionary[poolName].Count > 0)
            {
                obj = objectPoolDictionary[poolName].Dequeue();
                obj.transform.position = position;
                obj.transform.rotation = rotation;
                //obj.SetActive(true);
            }
            else
            {
                //Debug.Log(poolName + " is empty");
            }
        }
        else
        {
            //Debug.Log(poolName + " object pool is not available");
        }
        return obj;
    }

    /// <summary>
    /// Returns the object to its pool
    /// </summary>
    public static void ReturnObjectToPool(string poolName, GameObject poolObject)
    {
        if (objectPoolDictionary.ContainsKey(poolName))
        {
            objectPoolDictionary[poolName].Enqueue(poolObject);
            poolObject.SetActive(false);
        }
        else
        {
            //Debug.Log(poolName + " object pool is not available");
        }
    }
}