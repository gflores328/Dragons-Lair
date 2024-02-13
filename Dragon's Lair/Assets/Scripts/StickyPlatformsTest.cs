//Jo Marie Leatherman 
using System;
using UnityEngine;

//helpds player stay on ledges when they jump to them
public class StickyPlatformClass : MonoBehaviour
{
    public GameObject child;
    public GameObject parent;

    private void OnCollisionEnter(Collision collision)
    {

        //void OnCollisionEnter(Collision collision)
        //{
        // Sets newParent as parent to the child object
        //child.transform.SetParent(parent.transform);

        //keeps player from falling off the ledge > set worldPosistionStays to false
        //keeps local orientation instead of global orientation

        child.transform.SetParent(parent.transform);
        //}
    }

    private void OnCollisionExit(Collision collision)
    {

        //void OnCollisionExit(Collision collision)
        //{
        //Setting parent to null unparents the objects
        child.transform.SetParent(null);
        //
    }
}