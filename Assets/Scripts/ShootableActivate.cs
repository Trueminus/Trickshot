using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class ShootableActivate : MonoBehaviour
{
    public bool objective, destroyedOnHit, isSwitch, playing, pause = false;
    public int value;
    public PlayableDirector director;
    public string bulletTag;

    public void Awake()
    {
        //director = GetComponent<PlayableDirector>();
    }


    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(bulletTag))
        {
            if(objective) // This also gives you points to break
            {
                GameObject.Find("Manager").GetComponent<GameManager>().updateWinCount(value);
            }
            if(destroyedOnHit) // This gets broken
            {
                Destroy(gameObject);
            }
            if(isSwitch && !playing && !pause) //Activate the switch for the first time
            {
                director.Play();
                playing = true;
            }
            else if(isSwitch && playing) // Flip the switch and pause
            {
                director.Pause();
                playing = false;
                pause = true;
            }
            else if(isSwitch && !playing && pause) // Flip the switch and resume
            {
                director.Resume();
                playing = true;
                pause = false;
            }
            else //Ignore all that switch nonsense, this just turns it on
            {
                director.Play();
            }

        }


    }
}
