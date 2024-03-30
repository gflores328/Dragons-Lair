using System.Collections;
using UnityEngine;

public class DisappearingLedges : MonoBehaviour
{
    [SerializeField] private float timeToDisappear = 2f;
    [SerializeField] private float timeToReappear = 5f;
    [SerializeField] private float blinkInterval = .1f;

    private MeshRenderer meshRenderer;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
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
        StartCoroutine(Blink());

        yield return new WaitForSeconds(timeToDisappear);

        gameObject.SetActive(false);
        
        yield return new WaitForSeconds(timeToReappear);

        gameObject.SetActive(true);
        
        // Stop the blinking
        StopCoroutine("Blink");
        
        // Ensure the mesh renderer is enabled when the platform reappears
        meshRenderer.enabled = true;
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
