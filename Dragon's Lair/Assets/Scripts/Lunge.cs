using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lunge : MonoBehaviour
{

    private MimicPhaseTwo mimicPhaseTwo;
    // Start is called before the first frame update
    void Start()
    {
        GetComponentInParent<MimicPhaseTwo>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   
    public void Begin()
    {
        //mimicPhaseTwo.StartLunge();
    }
}
