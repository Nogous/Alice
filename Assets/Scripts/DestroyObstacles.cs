﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObstacles : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            other.gameObject.SetActive(false);
        }
    }
}
