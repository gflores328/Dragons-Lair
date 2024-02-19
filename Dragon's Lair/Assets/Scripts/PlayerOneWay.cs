using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerOneWay : MonoBehaviour
{
    
    public InputActionReference downAction;
    [SerializeField] private Collider playerCollider;

    private GameObject currentOneWayPlatform;
    public bool isMovingDown = false;

    

    
    
    private void OnEnable()
    {
        downAction.action.Enable();
        downAction.action.performed += OnDownPerformed;

    }
    private void OnDisable()

    {
        downAction.action.Disable();
        downAction.action.performed -= OnDownPerformed;

    }
    private void OnCollisionEnter(Collision collision)
    {
        if ( collision.gameObject.CompareTag("OneWayPlatform"))
        {
            currentOneWayPlatform = collision.gameObject;
            Debug.Log("Grabbed this");
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if ( collision.gameObject.CompareTag("OneWayPlatform"))
        {
            currentOneWayPlatform = null;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("OneWayPlatform"))

        {
            currentOneWayPlatform = other.gameObject;
            StartCoroutine(DisableCollision());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("OneWayPlatform"))

        {
            currentOneWayPlatform = null;
        }
    }

    private void OnDownPerformed(InputAction.CallbackContext context)

    {

        isMovingDown = true;
        if(currentOneWayPlatform != null)

        {
            StartCoroutine(DisableCollision());
        }
        
    }

    
    private IEnumerator DisableCollision()
    {
        Collider platformCollider = currentOneWayPlatform.GetComponent<Collider>();

        Physics.IgnoreCollision(playerCollider, platformCollider);
        yield return new WaitForSeconds(1.5f);
        Physics.IgnoreCollision(playerCollider, platformCollider,false);
        isMovingDown = false;
    }

    
    
}
