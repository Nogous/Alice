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
        particulesUnactive.transform.localPosition = Vector3.zero;
        particulesActive.SetActive(false);
        particulesActive.transform.localPosition = Vector3.zero;
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
        Debug.Log("mon problem a moi");
        particulesUnactive.SetActive(true);
        particulesActive.SetActive(false);
    }
}
