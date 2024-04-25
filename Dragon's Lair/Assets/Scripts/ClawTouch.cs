using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ClawTouch : MonoBehaviour
{

    public bool canTouch = true;
    public GameObject otherClaw;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Touched");
        if (collision.gameObject.TryGetComponent<Prize>(out Prize prize) && canTouch)
        {
            GetComponentInParent<ClawMovement>().touchingPrize = true;
            canTouch = false;
            otherClaw.GetComponent<ClawTouch>().canTouch = false;
        }
    }
}
