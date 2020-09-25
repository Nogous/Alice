using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AliceTeleporter : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PathCreation.Examples.PathFollower playerFollower = other.GetComponent<PathCreation.Examples.PathFollower>();
            playerFollower.SetDistanceInPath(playerFollower.distanceTravelled - 10);
        }
    }
}
