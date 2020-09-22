using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{

    private ObjectPooler objectPooler;

    [Header("Parameters")]
    [SerializeField] private float minTimeBetweenObstacles = 0;
    [SerializeField] private float maxTimeBetweenObstacles = 1;
    [SerializeField] private string[] obstacleTags;
    [SerializeField] private int xOffset = 1;
    [SerializeField] private int yOffset = 1;

    [Header("References")]
    [SerializeField] private Transform obstacleSpawnPoint;
    [SerializeField] private Transform obstacleTargetPoint;

    // Start is called before the first frame update
    void Start()
    {
        objectPooler = ObjectPooler.instance;


        if (!GameManager.instance.onDebugMode)
        {
            StartCoroutine(WaitAndSpawnObjects());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.K) && GameManager.instance.onDebugMode)
        {
            StartCoroutine(SpawnObjects());
        }
    }

    private IEnumerator WaitAndSpawnObjects()
    {
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(SpawnObjects());
    }

    private IEnumerator SpawnObjects()
    {
        GameObject obj = objectPooler.SpawnFromPool(obstacleTags[Random.Range(0, obstacleTags.Length)], 
            new Vector3(obstacleSpawnPoint.position.y + Random.Range(-xOffset, xOffset), obstacleSpawnPoint.position.y + Random.Range(-yOffset, yOffset), obstacleSpawnPoint.position.z), 
            new Vector3(Random.Range(-90, 90), Random.Range(-90, 90), Random.Range(-90, 90)));
        obj.GetComponent<Obstacle>().target = obstacleTargetPoint;
        yield return new WaitForSeconds(Random.Range(minTimeBetweenObstacles, maxTimeBetweenObstacles));
        StartCoroutine(SpawnObjects());
    }
}
