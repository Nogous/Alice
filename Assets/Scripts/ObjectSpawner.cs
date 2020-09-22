using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{

    private ObjectPooler objectPooler;

    [Header("Parameters")]
    [SerializeField] private float minTimeBetweenObstacles;
    [SerializeField] private float maxTimeBetweenObstacles;
    [SerializeField] private string[] obstacleTags;
    [SerializeField] private int minX;
    [SerializeField] private int maxX;

    // Start is called before the first frame update
    void Start()
    {
        objectPooler = ObjectPooler.instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.K))
        {
            StartCoroutine(SpawnObjects());
        }
    }

    private IEnumerator SpawnObjects()
    {
        objectPooler.SpawnFromPool(obstacleTags[Random.Range(0, obstacleTags.Length)], 
            new Vector3(Random.Range(minX, maxX), 15, 0), 
            new Vector3(Random.Range(-90, 90), Random.Range(-90, 90), Random.Range(-90, 90)));
        yield return new WaitForSeconds(Random.Range(minTimeBetweenObstacles, maxTimeBetweenObstacles));
        StartCoroutine(SpawnObjects());
    }
}
