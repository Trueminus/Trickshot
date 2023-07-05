using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GUIManager : MonoBehaviour
{

    public static bool paused = false, canPause = true;
    public GameObject pauseMenu, gameOverMenu, levelClearMenu, popUp;
    public TextMeshProUGUI currentGun, ammoCount;
    
    void Update()
    {
        //Pause/Unpause the game
        if(Input.GetKeyDown(KeyCode.P))
        {
            if(canPause)// Can't pause if you're already dead
            {
                if (paused)
                {
                    Unpause();
                }
                else
                {
                    Pause();
                }
            }
           
        }
    }


    public void UpdateAmmoCount(int count)
    {
        ammoCount.text = count.ToString();
    }

    public void UpdateGun(string gunName)
    {
        currentGun.text = gunName;
    }

    public void HideHUD()
    {
        ammoCount.enabled = false;
        currentGun.enabled = false;
    }

    public void ShowHUD()
    {
        ammoCount.enabled = true;
        currentGun.enabled = true;
    }

    public void Pause()
    {
        TimeManager.PauseTime();
        Debug.Log("Paused");
        paused = true;
        pauseMenu.SetActive(true);
        GunFire.canFire = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        GameObject.Find("Manager").GetComponent<GameManager>().DisableControl();
        HideHUD();
    }

    public void Unpause()
    {
        TimeManager.ResumeTime();
        Debug.Log("Resumed");
        paused = false;
        pauseMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = false;
        if (!GunFire.controlling)
        {
            Cursor.lockState = CursorLockMode.Locked;
            GunFire.canFire = true;
            GameObject.Find("Manager").GetComponent<GameManager>().SwitchControl();
        }
        else
        {
            Cursor.lockState = CursorLockMode.Confined;
        }
        ShowHUD();
    }

    public void OpenLevelClear()
    {
        GunFire.canFire = false;
        canPause = false;
        TimeManager.PauseTime();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        HideHUD();
        levelClearMenu.SetActive(true);
    }

    public void OpenMessage()
    {
        popUp.SetActive(true);
        Pause();
    }

    public void CloseMessage()
    {
        popUp.SetActive(false);
        Unpause();
    }

    public void SetMessage(string newText)
    {
        popUp.GetComponentInChildren<TextMeshProUGUI>().text = newText;
    }

    public void OnDeath()
    {
        canPause = true;
        Debug.Log("Game Over");
        GunFire.canFire = false;

        gameOverMenu.SetActive(true);
    }
}
