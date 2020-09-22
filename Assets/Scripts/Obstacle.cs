using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour, IPooledObject
{
    public Transform target;
    [SerializeField] private float speed = 1;

    public void OnObjectSpawned()
    {

    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        if(Vector3.Distance(transform.position, target.position) <= 0.2f)
        {
            gameObject.SetActive(false);
        }
    }
}
