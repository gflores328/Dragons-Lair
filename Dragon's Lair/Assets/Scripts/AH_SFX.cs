using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AH_SFX : MonoBehaviour
{
    public AudioSource Smack;
    public AudioSource GoalHorn;
    public AudioSource YouWin;
    public AudioSource YouLose;

    public void PlaySmack()
    {
        Smack.Play();
    }

    public void PlayGoalHorn()
    {
        GoalHorn.Play();
    }

    public void PlayYouWin()
    {
        YouWin.Play();
    }

    public void PlayYouLose()
    {
        YouLose.Play();
    }

}
