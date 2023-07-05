using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletControl : MonoBehaviour
{
    public float baseForwardForce, forwardAcceleration;
    private float activeForwardForce;
    public string[] collisionTagList;

    // Update is called once per frame
    void Update()
    {
        activeForwardForce = Mathf.Lerp(activeForwardForce, baseForwardForce, forwardAcceleration * Time.deltaTime);

        transform.position += transform.forward * activeForwardForce * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        foreach (string tag in collisionTagList)
        {
            if (other.gameObject.CompareTag(tag))
            {
                Destroy(gameObject);
            }
            if (other.CompareTag("Player"))
            {
                Debug.Log("Shot the player");
            }
        }

    }
}
