using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveAmmo : MonoBehaviour
{
	public int bulletIndex = 0;
	public int bulletAmount = 0;
    private GUIManager gui;
    public void Start()
    {
        gui = GameObject.Find("GUI").GetComponent<GUIManager>();
    }

    private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Player")) 
        { 
            GunFire.ammoCounts[bulletIndex] += bulletAmount;
            gui.UpdateAmmoCount(GunFire.ammoCounts[bulletIndex]);
            Destroy(gameObject);
        }
	}





}
