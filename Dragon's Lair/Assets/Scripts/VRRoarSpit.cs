using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRRoarSpit : MonoBehaviour
{
    public GameObject spit;


    public void StartSpit()
    {
        spit.SetActive(true);
    }

    public void StopSpit()
    {
        spit.SetActive(false);
    }
}
