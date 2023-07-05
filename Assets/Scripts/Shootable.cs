using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shootable : MonoBehaviour
{
    public bool objective;
    public int value;
    public string bulletTag;

    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(bulletTag))
        {
            if(objective)
            {
                GameObject.Find("Manager").GetComponent<GameManager>().updateWinCount(value);
            }

            if(transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            else
            {
                Destroy(gameObject);
            }

        }
    }

}
