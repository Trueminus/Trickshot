using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunFire : MonoBehaviour
{

    public static bool canFire, controlling;
    //0: Basic
    //1: Boost
    //2: Needle
    //3: Precision
    //4: Shield
    public GameObject[] bullets;
    public string[] bulletNames = {"Basic","Boost","Needle","Precision","Shield"};
    public static int[] ammoCounts = {0,0,0,0,0};
    public static int startingBullet;
    public int currentBulletIndex;
    public Transform gunHole;
    GameObject justFired;
    public GameManager manager;
    public GUIManager gui;

    void Start()
    {
        canFire = true;
        controlling = false;
        currentBulletIndex = startingBullet;
        manager = GameObject.Find("Manager").GetComponent<GameManager>();
        gui = GameObject.Find("GUI").GetComponent<GUIManager>();

        gui.UpdateAmmoCount(ammoCounts[currentBulletIndex]);
        gui.UpdateGun(bulletNames[currentBulletIndex]);
    }

    void Update()
    {
        if(Input.GetButtonDown("Fire1") && canFire && ammoCounts[currentBulletIndex] > 0)
        {
            canFire = false;

            ammoCounts[currentBulletIndex]--;

            gui.UpdateAmmoCount(ammoCounts[currentBulletIndex]);

            Debug.Log("Bullet fired");

            GameObject newBullet = Instantiate(bullets[currentBulletIndex], gunHole.position, gunHole.rotation * Quaternion.Euler(0f, 0f, 0f));

            CameraSwitcher.SwitchCamera(manager.bulletFollow);

            manager.bulletFollow.transform.SetParent(newBullet.transform);

            manager.bulletFollow.Follow = newBullet.transform;

            manager.SwitchControl();

            controlling = true;
        }

        
        if(Input.GetKeyDown(KeyCode.LeftBracket) && currentBulletIndex > 0)
        {
            currentBulletIndex--;
            gui.UpdateGun(bulletNames[currentBulletIndex]);
            gui.UpdateAmmoCount(ammoCounts[currentBulletIndex]);
        }
        if(Input.GetKeyDown(KeyCode.RightBracket) && currentBulletIndex < bullets.Length-1)
        {
            currentBulletIndex++;
            gui.UpdateGun(bulletNames[currentBulletIndex]);
            gui.UpdateAmmoCount(ammoCounts[currentBulletIndex]);
        }
        
    }
}
