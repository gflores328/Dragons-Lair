using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class VRThreeLoad : MonoBehaviour
{
    public GameObject player;
    public CinemachineVirtualCamera zoomCamera;
    public GameManager gameManager;
    public Animator mimicAnimator;
    public AudioSource source;
    public AudioClip roarClip;


    IEnumerator LoadLevel()
    {
        player.transform.position = new Vector3(26.601f, 0.025f, 31.94f);

        zoomCamera.Priority = 5;

        yield return new WaitForSeconds(1f);

        mimicAnimator.SetBool("Mimic_Roar", true);
        source.PlayOneShot(roarClip);

        yield return new WaitForSeconds(.5f);
        mimicAnimator.SetBool("Mimic_Roar", false);

        yield return new WaitForSeconds(3f);

        gameManager.LoadSceneAsync("LavaOne");
    }

    public void MimicRoar()
    {
        StartCoroutine(LoadLevel());
    }

}
