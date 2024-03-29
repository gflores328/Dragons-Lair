using System.Collections;
using UnityEngine;
using TMPro;

public class Powerup : MonoBehaviour
{
    public GameObject powerUpDesc;
    public Renderer powerUpRenderer; // Reference to the renderer component
    public string powerUpName; // Public string to specify the power-up name

    protected bool showDesc = false;
    protected float waitTime = 5f;
    protected Coroutine coroutine;

    protected virtual void Awake()
    {
        powerUpRenderer = GetComponent<Renderer>(); // Grab the renderer component
    }

    protected virtual void Update()
    {
        if (showDesc && coroutine == null)
        {
            powerUpDesc.SetActive(true);

            // Access the TextMeshPro component and set its text to the power-up name
            TextMeshProUGUI textMeshPro = powerUpDesc.GetComponentInChildren<TextMeshProUGUI>();
            if (textMeshPro != null)
            {
                textMeshPro.text = powerUpName;
            }
            else
            {
                Debug.LogError("TextMeshProUGUI component not found in powerUpDesc GameObject.");
            }

            coroutine = StartCoroutine(WaitAndDisable(waitTime));
        }
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            showDesc = true;
            powerUpRenderer.enabled = false; // Turn off the renderer when picked up
        }
    }

    protected IEnumerator WaitAndDisable(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        showDesc = false;
        powerUpDesc.SetActive(false);
        Destroy(gameObject);
    }
}
