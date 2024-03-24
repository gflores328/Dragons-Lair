using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftMouseCollisionHandler : MonoBehaviour
{
    public bool isAimingLeft = false;

    private void OnMouseEnter()
    {
        isAimingLeft = true;
    }

    private void OnMouseExit()
    {
        isAimingLeft = false;
    }
}
