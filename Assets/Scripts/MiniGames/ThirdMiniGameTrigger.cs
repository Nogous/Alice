using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdMiniGameTrigger : MonoBehaviour
{

    [SerializeField] private int id = 0;

    private void Start()
    {
        id = transform.GetSiblingIndex() - 1;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ThirdMiniGame.instance.CheckIfCanActivateTrigger(id);
        }
    }
}
