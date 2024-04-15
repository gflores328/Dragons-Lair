using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MimicPhaseThreeTrigger : MonoBehaviour
{
    public GameObject entrance;
    public GameObject mimic;
    public GameObject healthBar;

    private void OnTriggerEnter(Collider other)
    {
        //entrance.SetActive(true);
        if (other.tag == "Player")
        {
            mimic.GetComponent<MimicPhaseThree>().start = true;
            healthBar.SetActive(true);

            Destroy(gameObject);
        }
    }
}
