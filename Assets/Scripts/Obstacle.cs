using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour, IPooledObject
{
    [SerializeField] private Transform child;

    public void OnObjectSpawned()
    {

    }

    void Update()
    {
        
    }

    public void SetChildOffset(Vector3 offset)
    {
        child.localPosition = offset;
    }

    public void SetChildRotation(Vector3 rotation)
    {
        child.localEulerAngles = rotation;
    }
}
