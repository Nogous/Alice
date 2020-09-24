using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pushable : MonoBehaviour
{
    public string hatTagName;
    public PathCreation.Examples.PathFollower pathFollower;
    public float hatColisionRange = 5f;

    private bool isAtPlayerSpeed = false;

    private void Update()
    {
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 0);

        Vector3 tmp = transform.localPosition.normalized * hatColisionRange;
        if (transform.localPosition.magnitude >=(tmp.magnitude))
        {
            transform.localPosition = transform.localPosition.normalized * hatColisionRange;
        }

        if (!isAtPlayerSpeed)
        SwitchToPlayerSpeed();
    }

    private void SwitchToPlayerSpeed()
    {
        if (pathFollower == null || PlayerEntity.instance == null) return;

        if (pathFollower.distanceTravelled <= PlayerEntity.instance.followers[0].distanceTravelled)
        {
            transform.parent = PlayerEntity.instance.followers[0].gameObject.transform;
            Destroy(pathFollower.gameObject);
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
