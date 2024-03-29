using System.Collections;
using UnityEngine;
using TMPro;

public class Powerup : MonoBehaviour
{
    public GameObject powerUpDesc; // A public game object that grabs the gameobject that will display the text
    public Renderer powerUpRenderer; // Reference to the renderer component
    public string powerUpName; // Public string to specify the power-up name

    protected bool showDesc = false; // A bool variable to determine if the powerup should be showing or not
    protected float waitTime = 5f; // determines how long the text will be displayed for
    protected Coroutine coroutine; // a coroutine variable to hold the corutine script to be able to run the wait and disable function

    protected virtual void Awake()
    {
        powerUpRenderer = GetComponent<Renderer>(); // Grab the renderer component
    }

    protected virtual void Update()
    {
        if (showDesc && coroutine == null) // if showDesc is true and the coroutine is not null 
        {
            powerUpDesc.SetActive(true); // Display the text

            // Access the TextMeshPro component and set its text to the power-up name
            TextMeshProUGUI textMeshPro = powerUpDesc.GetComponentInChildren<TextMeshProUGUI>(); 
            if (textMeshPro != null) // make sure there was a text mesh object was grabbed
            {
                textMeshPro.text = powerUpName; // set the text to display the power up description 
            }
            else
            {
                Debug.LogError("TextMeshProUGUI component not found in powerUpDesc GameObject."); // if fails display to console
            }

            coroutine = StartCoroutine(WaitAndDisable(waitTime)); // Run the wait and disable corutine
        }
    }

    protected virtual void OnTriggerEnter(Collider other) // when it is collided with  check if it is the player
    {
        if (other.gameObject.CompareTag("Player"))
        {
            showDesc = true; //  tell it to show the text
            powerUpRenderer.enabled = false; // Turn off the renderer when picked up
        }
    }

    protected IEnumerator WaitAndDisable(float waitTime) // A Ienumerator run and disable with a parameter determining how long to wait
    {
        yield return new WaitForSeconds(waitTime); // wait for x amount of time
        showDesc = false; // turn the showDesc off
        powerUpDesc.SetActive(false); // Turn the text off
        Destroy(gameObject); // Destroy powerup powerup
    }
}
