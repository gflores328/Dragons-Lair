using UnityEngine;
// Play a Sound when the player goes into a colldier.
public class SplatSound : MonoBehaviour
{
    public AudioSource Splat;

    private void OnTriggerEnter(Collider other) {
        Splat.Play();
    }

    
}
