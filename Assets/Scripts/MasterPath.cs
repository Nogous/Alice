using UnityEngine;

public class MasterPath : MonoBehaviour
{
    public static MasterPath instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.Log("l'instance de MasterPath existe deja. :)");
            Destroy(gameObject);
        }
    }
    
    [Header("References")]
    public PathCreation.PathCreator mainPath;
    public PathCreation.Examples.PathFollower playerPath;
    public PathCreation.Examples.PathFollower obstacleSpawner;
    public PathCreation.Examples.PathFollower obstacleDestroyer;

    public PathCreation.PathCreator[] paths;

    [Header("Values")]
    [SerializeField] private float spawnerDistance = 20;
    [SerializeField] private float destroyerDistance = -10;

    private void Start()
    {
        obstacleSpawner.SetPathOffset(spawnerDistance);
        
        obstacleDestroyer.SetPathOffset(destroyerDistance);
    }

    public void SwitchMainPath(int id)
    {
        mainPath = paths[id];

        playerPath.SetPathCreator(mainPath);
        playerPath.SetDistanceInPath(0);

        obstacleSpawner.SetPathCreator(mainPath);
        obstacleSpawner.SetPathOffset(spawnerDistance);

        obstacleDestroyer.SetPathCreator(mainPath);
        obstacleDestroyer.SetPathOffset(destroyerDistance);
    }
}
