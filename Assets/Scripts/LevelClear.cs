using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelClear : MonoBehaviour
{
    public GUIManager gui;
    private void Start()
    {
        gui = GameObject.Find("GUI").GetComponent<GUIManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //Flag the next level as unlocked.
            //REMINDER: Each scene's build index is its entry in unlocked + 1
                //Scene ID 1 is unlocked[0]
            LevelUnlockManager.unlocked[(SceneManager.GetActiveScene().buildIndex)] = true;
            //Debug.Log("Unlocked level: " + (SceneManager.GetActiveScene().buildIndex));

            //
            GUIManager.canPause = false;
            gui.OpenLevelClear();
        }
    }
}
