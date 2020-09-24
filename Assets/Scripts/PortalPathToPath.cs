using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalPathToPath : MonoBehaviour
{
    public int idNextPath;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter");

        if (other.CompareTag("Player"))
        {
            MasterPath.instance.SwitchMainPath(idNextPath);
            gameObject.SetActive(false);
        }
    }
}