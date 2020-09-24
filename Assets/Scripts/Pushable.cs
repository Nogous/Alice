using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pushable : MonoBehaviour
{
    public string hatTagName;
    public PathCreation.Examples.PathFollower pathFollower;

    private bool isAtPlayerSpeed = false;

    private void Update()
    {
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 0);

        if(!isAtPlayerSpeed)
        SwitchToPlayerSpeed();
    }

    private void SwitchToPlayerSpeed()
    {
        if (pathFollower == null) return;

        if (pathFollower.distanceTravelled <= PlayerEntity.instance.followers[0].distanceTravelled)
        {
            pathFollower.distanceTravelled = PlayerEntity.instance.followers[0].distanceTravelled;
            pathFollower.speed = PlayerEntity.instance.followers[0].speed;
            isAtPlayerSpeed = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(hatTagName))
        {
            HatMinigame.instance.GetHat();
            gameObject.SetActive(false);
        }
    }
}
