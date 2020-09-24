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
                StartCoroutine(WaitAndEndTuto());
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

    IEnumerator WaitAndEndTuto()
    {
        yield return new WaitForSeconds(15);
        shouldSpawnObjects = false;
        yield return new WaitForSeconds(3);
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
        InstantiateObject(obstacleTags);
        float globalTime = 0;
        float time = Random.Range(0.2f, 0.5f);
        yield return new WaitForSeconds(time);
        InstantiateObject(obstacleTags);
        float secondTime = Random.Range(0.2f, 0.5f);
        globalTime = time + secondTime;
        yield return new WaitForSeconds(secondTime);
        InstantiateObject(obstacleTags);
        yield return new WaitForSeconds(Random.Range(minTimeBetweenObstacles, maxTimeBetweenObstacles) - globalTime);
        if(shouldSpawnObjects)
            StartCoroutine(SpawnObjects());
    }

    public void InstantiateObject(string [] objectTags)
    {
        GameObject obj = objectPooler.SpawnFromPool(objectTags[Random.Range(0, objectTags.Length)],
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
