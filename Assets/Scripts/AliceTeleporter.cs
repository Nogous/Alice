using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AliceTeleporter : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PathCreation.Examples.PathFollower playerFollower = other.transform.parent.GetComponent<PathCreation.Examples.PathFollower>();
            playerFollower.SetDistanceInPath(playerFollower.distanceTravelled - 10, true);
            for(int i = 0; i < PlayerEntity.instance.followers.Count; i++)
            {
                PlayerEntity.instance.followers[i].SetDistanceInPath(PlayerEntity.instance.followers[i].GetComponent<PathCreation.Examples.PathFollower>().distanceTravelled - 10, true);
            }
            
        }
    }
}
