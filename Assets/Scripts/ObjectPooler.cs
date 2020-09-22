using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    public static ObjectPooler instance;

    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionnary;

    public System.Action onPoolsCreated;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        poolDictionnary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            for(int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }
            poolDictionnary.Add(pool.tag, objectPool);
        }

        if (onPoolsCreated != null) onPoolsCreated.Invoke();
    }

    public GameObject SpawnFromPool(string tag, Vector3 position, Vector3 rotation)
    {
        if (!poolDictionnary.ContainsKey(tag))
        {
            return null;
        }

        GameObject objectToSpawn = poolDictionnary[tag].Dequeue();

        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = Quaternion.Euler(rotation);

        IPooledObject pooledObject = objectToSpawn.GetComponent<IPooledObject>();
        if(pooledObject != null)
        {
            pooledObject.OnObjectSpawned();
        }

        poolDictionnary[tag].Enqueue(objectToSpawn);

        return objectToSpawn;
    }

}
