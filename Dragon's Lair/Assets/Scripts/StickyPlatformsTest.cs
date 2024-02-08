//Jo Marie Leatherman
using UnityEngine;

//helpds player stay on ledges when they jump to them
public class StickyPlatform : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            collider.gameObject.transform.SetParent(transform);
        }
    }

    //when exiting the collider the player can continue to other ledges
    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            collider.gameObject.transform.SetParent(null);
        }
    }
}