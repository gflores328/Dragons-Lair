using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightMouseCollisionHandler : MonoBehaviour
{
    public bool isAimingRight = false;

    private void OnMouseEnter()
    {
        isAimingRight = true;
    }

    private void OnMouseExit()
    {
        isAimingRight = false;
    }
}
