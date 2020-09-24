using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigHat : MonoBehaviour
{
    public PathCreation.Examples.PathFollower pathFollower;
    public PathCreation.Examples.PathFollower playerPathFollower;

    private void Start()
    {
        playerPathFollower = PlayerEntity.instance.followers[0];
        StartMiniGame();
    }

    public void StartMiniGame()
    {
        Debug.LogError("WAT");
        //pathFollower.pathCreator =  ObjectSpawner.instance.GetPath();
        //pathFollower.distanceTravelled = ObjectSpawner.instance.gameObject.GetComponent<PathCreation.Examples.PathFollower>().distanceTravelled;

        playerPathFollower = PlayerEntity.instance.followers[0];
    }

    // Update is called once per frame
    void Update()
    {

        //Debug.Log(pathFollower.distanceTravelled +" : "+ playerPathFollower.distanceTravelled +" / "+ pathFollower.speed + " : " + playerPathFollower.speed);

        if (pathFollower.distanceTravelled <= playerPathFollower.distanceTravelled)
            {
                pathFollower.distanceTravelled = playerPathFollower.distanceTravelled;
                pathFollower.speed = playerPathFollower.speed;
            }
    }

    public void EndMiniGame()
    {
        pathFollower.speed = 0;
        StartCoroutine(SetUnactiveIn(5f));
    }

    private IEnumerator SetUnactiveIn(float time)
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }
}
