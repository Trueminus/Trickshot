using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropAmmo : MonoBehaviour
{
    public GameObject toDrop;


    void OnDestroy()
    {
        Instantiate(toDrop,transform.position,transform.rotation);
    }

}
