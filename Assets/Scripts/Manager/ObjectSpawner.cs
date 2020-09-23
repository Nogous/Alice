using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public static ObjectSpawner instance;
    private ObjectPooler objectPooler;

    [Header("Parameters")]
    [SerializeField] private float minTimeBetweenObstacles = 0;
    [SerializeField] private float maxTimeBetweenObstacles = 1;
    [SerializeField] private string[] obstacleTags;
    [SerializeField] private int xOffset = 1;
    [SerializeField] private int yOffset = 1;

    [Header("References")]
    [SerializeField] private Transform obstacleSpawnPoint;
    [SerializeField] PathCreation.PathCreator mainPath;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
    }

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
        InstantiateObject();
        yield return new WaitForSeconds(0.3f);
        InstantiateObject();
        yield return new WaitForSeconds(Random.Range(minTimeBetweenObstacles, maxTimeBetweenObstacles));
        StartCoroutine(SpawnObjects());
    }

    private void InstantiateObject()
    {
        GameObject obj = objectPooler.SpawnFromPool(obstacleTags[Random.Range(0, obstacleTags.Length)],
            obstacleSpawnPoint.position,
            Vector3.zero);
        Vector3 newOffset = new Vector3(Random.Range(-xOffset, xOffset), Random.Range(-yOffset, yOffset));
        Vector3 newRotation = new Vector3(Random.Range(-90, 90), Random.Range(-90, 90), Random.Range(-90, 90));
        obj.GetComponent<Obstacle>().SetChildOffset(newOffset);
        obj.GetComponent<Obstacle>().SetChildRotation(newRotation);
        obj.GetComponent<PathCreation.Examples.PathFollower>().pathCreator = mainPath;
        PathCreation.Examples.PathFollower spawnParentFollower = obstacleSpawnPoint.parent.GetComponent<PathCreation.Examples.PathFollower>();
        obj.GetComponent<PathCreation.Examples.PathFollower>().distanceTravelled = spawnParentFollower.distanceTravelled + spawnParentFollower.pathOffset;
    }

    public void InstantiateObject(string collectibleTag)
    {
        GameObject obj = objectPooler.SpawnFromPool(collectibleTag, obstacleSpawnPoint.position, Vector3.zero);
        Vector3 newOffset = new Vector3(Random.Range(-xOffset, xOffset), Random.Range(-yOffset, yOffset));
        Vector3 newRotation = new Vector3(Random.Range(-90, 90), Random.Range(-90, 90), Random.Range(-90, 90));
        obj.GetComponent<Obstacle>().SetChildOffset(newOffset);
        obj.GetComponent<Obstacle>().SetChildRotation(newRotation);
        obj.GetComponent<PathCreation.Examples.PathFollower>().pathCreator = mainPath;
        PathCreation.Examples.PathFollower spawnParentFollower = obstacleSpawnPoint.parent.GetComponent<PathCreation.Examples.PathFollower>();
        obj.GetComponent<PathCreation.Examples.PathFollower>().distanceTravelled = spawnParentFollower.distanceTravelled + spawnParentFollower.pathOffset;
    }
}
