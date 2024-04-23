using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MimicPhaseThreeTrigger : MonoBehaviour
{
    public GameObject entrance;
    public GameObject mimic;
    public GameObject healthBar;

    [Header("Cameras")]
    public GameObject mainCamera;
    public GameObject bossCamera;
    private void OnTriggerEnter(Collider other)
    {
        //entrance.SetActive(true);
        if (other.tag == "Player")
        {
            entrance.SetActive(true);
            mimic.GetComponent<MimicPhaseThree>().StartStartDelay();

            other.GetComponent<PlayerInput>().actions.Disable();

            mainCamera.SetActive(false);
            bossCamera.SetActive(true);

            Destroy(gameObject);
        }
    }
}
