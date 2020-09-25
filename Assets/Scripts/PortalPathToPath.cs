using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalPathToPath : MonoBehaviour
{
    public int idNextPath;
    public int miniGameToStart;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter");

        if (other.CompareTag("Player"))
        {
            MasterPath.instance.SwitchMainPath(idNextPath);
            switch (miniGameToStart)
            {
                case 2:
                    print("miaou");
                    MiniGameManager.instance.ChangeState(State.FIRSTMG);
                    break;
            }
            gameObject.SetActive(false);
        }
    }
}