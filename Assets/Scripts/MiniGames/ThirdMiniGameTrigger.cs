using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdMiniGameTrigger : MonoBehaviour
{

    [SerializeField] private int id = 0;

    public GameObject particulesActive;
    public GameObject particulesUnactive;

    private void Start()
    {
        id = transform.GetSiblingIndex() - 1;
        particulesUnactive.SetActive(true);
        particulesActive.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            particulesUnactive.SetActive(false);
            particulesActive.SetActive(true);
            ThirdMiniGame.instance.CheckIfCanActivateTrigger(id);
        }
    }

    public void ResetParticules()
    {
        particulesUnactive.SetActive(true);
        particulesActive.SetActive(false);
    }
}
