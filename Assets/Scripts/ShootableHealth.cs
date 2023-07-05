using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootableHealth : MonoBehaviour
{
    public bool objective;
    public int value, health;
    public string bulletTag;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(bulletTag))
        {
            health--;
            if(health <= 0)
            {
                if (objective)
                {
                    GameObject.Find("Manager").GetComponent<GameManager>().updateWinCount(value);
                }

                if (transform.parent != null)
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

}
