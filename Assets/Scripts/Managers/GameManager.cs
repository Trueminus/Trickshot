using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    public CinemachineVirtualCamera playerCamera, bulletFollow;
    public GameObject player, clearZone;
    public static bool playerInControl = true;
    public int winCount = 0, neededWinCount = 1;
    public bool winPoints;
    public int[] startingAmmo;
    public int startingBullet = 0;


    //Assign cameras in the camera manager and set a default time
    private void OnEnable()
    {
        CameraSwitcher.RegisterCamera(playerCamera);
        CameraSwitcher.RegisterCamera(bulletFollow);

        CameraSwitcher.setActiveCamera(playerCamera);

        TimeManager.ResetTime();

        GunFire.ammoCounts = startingAmmo;
        GunFire.startingBullet = startingBullet;
    }



        // Manage control of the player

    //Activate or deactivate player character control
    public void SwitchControl()
    {
        if(playerInControl)
        {
            player.GetComponent<PlayerController>().enabled = false;
            player.GetComponentInChildren<PlayerViewCamera>().enabled = false;
            playerInControl = false;
        }
        else
        {
            player.GetComponent<PlayerController>().enabled = true;
            player.GetComponentInChildren<PlayerViewCamera>().enabled = true;
            playerInControl = true;
        }
        Debug.Log(playerInControl);
    }

    //Deactivate player character control without checking any flags
    public void DisableControl()
    {
        player.GetComponent<PlayerController>().enabled = false;
        player.GetComponentInChildren<PlayerViewCamera>().enabled = false;
        playerInControl = false;
    }

        //Keep track of defeated enemies and objectives


    //If the level is cleared by aquiring points, increment point counter and enable the goal upon reaching the indicated mark.
    public void updateWinCount(int num)
    {
        if(winPoints)
        {
            winCount += num;

            if (winCount >= neededWinCount)
            {
                clearZone.SetActive(true);
            }
        }
    }



}
