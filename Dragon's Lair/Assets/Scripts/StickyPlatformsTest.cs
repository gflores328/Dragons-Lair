//Jo Marie Leatherman 
using System;
using UnityEngine;

//helpds player stay on ledges when they jump to them
public class StickyPlatformClass : MonoBehaviour
{
    public GameObject child; //player
    public GameObject parent; //ledge
    private Transform _originalParent;


    public void SetParent(Transform newParent)
    {
        _originalParent = transform.parent;
        transform.parent = newParent;
    }

    public void ResetParent()
    {
        transform.parent = _originalParent;
    }

    private void OnCollisionEnter(Collision collision)
    {

        //void OnCollisionEnter(Collision collision)
        //{
        // Sets newParent as parent to the child object
        //child.transform.SetParent(parent.transform);

        //keeps player from falling off the ledge > set worldPosistionStays to false
        //keeps local orientation instead of global orientation

        //child.transform.SetParent(parent.transform);
        //}

        var platMovement = collision.collider.GetComponent<MovingLedgesTest>();
        if (platMovement != null)
        {
            SetParent(transform);
        }
    }

    private void OnCollisionExit(Collision collision)
    {

        //void OnCollisionExit(Collision collision)
        //{
        //Setting parent to null unparents the objects
        //child.transform.SetParent(null);
        //

        var platMovement = collision.collider.GetComponent<MovingLedgesTest>();
        if (platMovement != null)
        {
            ResetParent();
        }
    }

}