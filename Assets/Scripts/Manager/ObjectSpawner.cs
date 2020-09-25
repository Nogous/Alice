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
    public float xOffset = 0.5f;
    public float yOffset = 0.5f;
    public float xBigOffset = 3;
    public float yBigOffset = 3;
    [SerializeField] private int _numberOfObjectsOnBigArea = 3;
    [SerializeField] private int _numberOfObjectsOnLittleArea = 2;

    [Header("References")]
    [SerializeField] private Transform obstacleSpawnPoint;
    [SerializeField] PathCreation.PathCreator mainPath;

    private bool shouldSpawnObjects = false;

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


        MiniGameManager.instance.onChangeState += () =>
        {
            if (MiniGameManager.instance.state == State.TUTO)
            {
                StartCoroutine(WaitAndSpawnObjects());
            }
        };
    
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.K) && GameManager.instance.onDebugMode)
        {
            StartCoroutine(SpawnObjects());
        }
    }

    public IEnumerator WaitAndEndTuto()
    {
        shouldSpawnObjects = false;
        yield return new WaitForSeconds(1f);
        foreach (Transform child in objectPooler.transform)
        {
            child.gameObject.SetActive(false);
        }
        MiniGameManager.instance.ChangeState(State.NONE);
        if (GameManager.instance.onPhaseChange != null) GameManager.instance.onPhaseChange.Invoke(2);
    }

    private IEnumerator WaitAndSpawnObjects()
    {
        yield return new WaitForSeconds(0.5f);
        shouldSpawnObjects = true;
        StartCoroutine(SpawnObjects());
    }

    private IEnumerator SpawnObjects()
    {
        if (shouldSpawnObjects)
        {
            for (int i = 0; i <= _numberOfObjectsOnBigArea; i++)
            {
                InstantiateObject(obstacleTags, xBigOffset, yBigOffset);
            }
            //
            for (int i = 0; i <= _numberOfObjectsOnLittleArea; i++)
            {
                InstantiateObject(obstacleTags, xOffset, yOffset);
            }
            float globalTime = 0;
            float time = Random.Range(minTimeBetweenObstacles / 4, minTimeBetweenObstacles / 2);
            yield return new WaitForSeconds(time);
            for (int i = 0; i <= _numberOfObjectsOnBigArea; i++)
            {
                InstantiateObject(obstacleTags, xBigOffset, yBigOffset);
            }
            //
            for (int i = 0; i <= _numberOfObjectsOnLittleArea; i++)
            {
                InstantiateObject(obstacleTags, xOffset, yOffset);
            }
            float secondTime = Random.Range(minTimeBetweenObstacles / 4, minTimeBetweenObstacles / 2);
            globalTime = time + secondTime;
            yield return new WaitForSeconds(secondTime);
            for (int i = 0; i <= _numberOfObjectsOnBigArea; i++)
            {
                InstantiateObject(obstacleTags, xBigOffset, yBigOffset);
            }
            //
            for (int i = 0; i <= _numberOfObjectsOnLittleArea; i++)
            {
                InstantiateObject(obstacleTags, xOffset, yOffset);
            }
            //
            InstantiateObject(obstacleTags, xOffset, yOffset);
            yield return new WaitForSeconds(Random.Range(minTimeBetweenObstacles, maxTimeBetweenObstacles) - globalTime);
            if (shouldSpawnObjects)
                StartCoroutine(SpawnObjects());
        }
        
    }

    public void InstantiateObject(string [] objectTags, float offsetX, float offsetY)
    {
        GameObject obj = objectPooler.SpawnFromPool(objectTags[Random.Range(0, objectTags.Length)],
            obstacleSpawnPoint.position,
            Vector3.zero);
        Vector3 newOffset = new Vector3(Random.Range(-offsetX, offsetX), Random.Range(-offsetY, offsetY));
        Vector3 newRotation = new Vector3(Random.Range(-90, 90), Random.Range(-90, 90), Random.Range(-90, 90));
        obj.GetComponent<Obstacle>().SetChildOffset(newOffset);
        obj.GetComponent<Obstacle>().SetChildRotation(newRotation);
        obj.GetComponent<PathCreation.Examples.PathFollower>().pathCreator = MasterPath.instance.mainPath;
        PathCreation.Examples.PathFollower spawnParentFollower = obstacleSpawnPoint.parent.GetComponent<PathCreation.Examples.PathFollower>();
        obj.GetComponent<PathCreation.Examples.PathFollower>().distanceTravelled = spawnParentFollower.distanceTravelled + spawnParentFollower.pathOffset;
    }

    public void InstantiateObject(string collectibleTag, float offsetX, float offsetY)
    {
        GameObject obj = objectPooler.SpawnFromPool(collectibleTag, obstacleSpawnPoint.position, Vector3.zero);
        Vector3 newOffset = new Vector3(Random.Range(-offsetX, offsetX), Random.Range(-offsetY, offsetY));
        Vector3 newRotation = new Vector3(Random.Range(-90, 90), Random.Range(-90, 90), Random.Range(-90, 90));
        obj.GetComponent<Obstacle>().SetChildOffset(newOffset);
        obj.GetComponent<Obstacle>().SetChildRotation(newRotation);
        obj.GetComponent<PathCreation.Examples.PathFollower>().pathCreator = mainPath;
        PathCreation.Examples.PathFollower spawnParentFollower = obstacleSpawnPoint.parent.GetComponent<PathCreation.Examples.PathFollower>();
        obj.GetComponent<PathCreation.Examples.PathFollower>().distanceTravelled = spawnParentFollower.distanceTravelled + spawnParentFollower.pathOffset;
    }

    public PathCreation.PathCreator GetPath()
    {
        return mainPath;
    }
}
