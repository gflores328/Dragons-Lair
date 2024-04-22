using System.Collections;
using UnityEngine;
using UnityEngine.UI; // Import the UnityEngine.UI namespace to access UI components

public class DoorCutscene : MonoBehaviour
{
    public Transform endPosition;
    public GameObject player;
    public GameObject fadeImage;
    public GameObject questionPopUp;
    public float fadeDuration = 1.0f;

    private bool isFading = false;

    private RealPlayerMovement realPlayerMovement;
    void Start()
    {
        // Ensure the fade image is initially turned off
        fadeImage.SetActive(false);
        realPlayerMovement = FindObjectOfType<RealPlayerMovement>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            questionPopUp.SetActive(true); // Activate the question popup
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            questionPopUp.SetActive(false); // Activate the question popup
        }
    }

    // This method is called when the player answers the question
    public void OnQuestionAnswered(bool answer)
    {
        questionPopUp.SetActive(false); // Hide the question popup
        if (!isFading) // Ensure that a fade transition is not already in progress
        {
            if (answer) // If the player answered yes
            {
                StartCoroutine(DoTeleportation()); // Start the teleportation process
            }
           
        }
    }

    // Coroutine for the teleportation process
    private IEnumerator DoTeleportation()
    {
        isFading = true; // Set the flag to indicate that a fade transition is in progress
        if (realPlayerMovement != null)
        {
            realPlayerMovement.EnablePlayerMovement(false);
        }
        // Fade out
        fadeImage.SetActive(true); // Activate the fade image
        yield return StartCoroutine(FadeOut());

        // Teleport player
        //player.GetComponent<Collider>().enabled = false;
        realPlayerMovement.SetPlayerPosition(endPosition.position);
        Debug.Log("Player teleported to end position");
        // Fade in
        //player.GetComponent<Collider>().enabled = true;
        yield return StartCoroutine(FadeIn());
        fadeImage.SetActive(false); // Deactivate the fade image
        if (realPlayerMovement != null)
        {
            realPlayerMovement.EnablePlayerMovement(true);
        }

        isFading = false; // Reset the flag after the fade transition is complete
    }

    // Coroutine for fading out
    private IEnumerator FadeOut()
    {
        float timer = 0f;
        Color startColor = fadeImage.GetComponent<Image>().color; // Get the initial color
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 1f); // Fully opaque black

        while (timer < fadeDuration)
        {
            float t = timer / fadeDuration;
            fadeImage.GetComponent<Image>().color = Color.Lerp(startColor, endColor, t); // Interpolate between initial color and opaque black
            timer += Time.deltaTime;
            yield return null;
        }

        fadeImage.GetComponent<Image>().color = endColor; // Ensure final color is fully opaque black
    }

    // Coroutine for fading in
    private IEnumerator FadeIn()
    {
        float timer = 0f;
        Color startColor = fadeImage.GetComponent<Image>().color; // Get the initial color
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 0f); // Fully transparent

        while (timer < fadeDuration)
        {
            float t = timer / fadeDuration;
            fadeImage.GetComponent<Image>().color = Color.Lerp(startColor, endColor, t); // Interpolate between initial color and fully transparent
            timer += Time.deltaTime;
            yield return null;
        }

        fadeImage.GetComponent<Image>().color = endColor; // Ensure final color is fully transparent
    }
}
