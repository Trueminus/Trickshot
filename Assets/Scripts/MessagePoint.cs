using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessagePoint : MonoBehaviour
{
    public GUIManager gui;

    public void Start()
    {
        gui = GameObject.Find("GUI").GetComponent<GUIManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            gui.OpenMessage();

            Destroy(gameObject);
        }
    }


}
