using System.Collections;
using UnityEngine;

public class DisappearingLedges : MonoBehaviour
{
    [SerializeField] private float timeToDisappear = 2f;
    [SerializeField] private float timeToReappear = 5f;
    [SerializeField] private float blinkInterval = .1f;

    private MeshRenderer meshRenderer;
    private MeshCollider meshCollider;
    private BoxCollider boxCollider;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        meshCollider = GetComponent<MeshCollider>();
        boxCollider = GetComponent<BoxCollider>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(Vanish());
            Debug.Log("player on");
        }
    }

    IEnumerator Vanish()
    {
        // Blink before disappearing
        //StartCoroutine(Blink());

        yield return new WaitForSeconds(timeToDisappear);

        // Stop the blinking
        //StopCoroutine(Blink());

        meshRenderer.enabled = false;
        meshCollider.enabled = false;
        boxCollider.enabled = false;
        

        yield return new WaitForSeconds(timeToReappear);

        
        // Ensure the mesh renderer is enabled when the platform reappears
        meshRenderer.enabled = true;
        meshCollider.enabled = true;
        boxCollider.enabled = true;
    }

    IEnumerator Blink()
    {
        while (true)
        {
            meshRenderer.enabled = !meshRenderer.enabled;
            yield return new WaitForSeconds(blinkInterval);
        }
    }
}
