using System.Collections;
using System.Collections.Generic;
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

    public PathCreation.PathCreator mainPath;
    public PathCreation.Examples.PathFollower playerPath;

    public PathCreation.PathCreator[] paths;

    public void SwitchMainPath(int id)
    {
        mainPath = paths[id];

        playerPath.SetPathCreator(mainPath);
        playerPath.SetDistanceInPath(0);
    }
}
