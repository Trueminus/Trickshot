using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUnlockManager : MonoBehaviour
{
    public Button[] levelButtons;

    //Static array to unlock levels from other scripts

    //This is going to annoying to keep track of, prepare to come back here a lot.
    //Keep Scene Index of each level at unlocked[i+1]
    //Unlock next level with unlocked[(SceneManager.GetActiveScene().buildIndex)+1] = true;
                      //Scene Index: 1     2      3      4      5      6      7      8      9      10     11     12
    public static bool[] unlocked = {true, false, false, false, false, false, false, false, false, false, false, false};


    //Upon activating the level select screen, run through what levels are unlocked and activate their selection buttons.
    private void OnEnable()
    {
        for(int i = 0; i < levelButtons.Length; i++ )
        {
            if(unlocked[i]) { levelButtons[i].interactable = true; }
        }
    }
}
