using System.Collections;
using UnityEngine;

public class DisappearingLedges : MonoBehaviour
{
    [SerializeField] private float timeToDisappear = 2f;
    [SerializeField] private float timeToReappear = 5f;
    [SerializeField] private float blinkInterval = .1f;


    private bool isBlinking;
    public MeshRenderer meshRenderer;
    public MeshCollider meshCollider;
    private BoxCollider boxCollider;

    private Coroutine blinkCoroutine;

    void Start()
    {
        // meshRenderer = GetComponent<MeshRenderer>();
        // meshCollider = GetComponent<MeshCollider>();
         boxCollider = GetComponent<BoxCollider>();
    }

    void OnTriggerEnter(Collider other)
    {
        
        StartCoroutine(Vanish());
        Debug.Log("player on");
        
    }

    IEnumerator Vanish()
    {
        // Blink before disappearing
        blinkCoroutine = StartCoroutine(Blink());

        yield return new WaitForSeconds(timeToDisappear);

        // Stop the blinking
        StopCoroutine(blinkCoroutine);
        isBlinking = false;
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
        isBlinking = true;
        while (isBlinking)
        {
            meshRenderer.enabled = !meshRenderer.enabled;
            yield return new WaitForSeconds(blinkInterval);
        }
    }
}
