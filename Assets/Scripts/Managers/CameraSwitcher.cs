using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public static class CameraSwitcher
{

    static List<CinemachineVirtualCamera> cameras = new List<CinemachineVirtualCamera>();

    public static CinemachineVirtualCamera activeCamera = null;

    public static bool canSwitch = true;


    

    public static void RegisterCamera(CinemachineVirtualCamera camera)
    {
        cameras.Add(camera);
        Debug.Log(camera + " registered");
    }

    public static void DeregisterCamera(CinemachineVirtualCamera camera)
    {
        cameras.Remove(camera);
    }

    public static void SwitchCamera(CinemachineVirtualCamera camera)
    {
        if(canSwitch)
        {
            camera.Priority = 5;
            activeCamera = camera;

            foreach (CinemachineVirtualCamera c in cameras)
            {
                if(c != camera && c.Priority != 0)
                {
                    c.Priority = 0;
                }
            }
        }
        
    }

    public static bool isActiveCamera(CinemachineVirtualCamera camera)
    {
        return camera == activeCamera;
    }

    public static CinemachineVirtualCamera getActiveCamera()
    {
        return activeCamera;
    }

    public static void setActiveCamera(CinemachineVirtualCamera camera)
    {
        activeCamera = camera;
    }

}
