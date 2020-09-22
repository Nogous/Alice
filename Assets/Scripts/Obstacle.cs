using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour, IPooledObject
{
    public Transform target;
    [SerializeField] private float speed;

    public void OnObjectSpawned()
    {

    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }
}
