using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NextLevelTrigger : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;

    [SerializeField] private string levelToLoad;
    [SerializeField] private GameObject LoadingScreen;

    public bool glitchScreen = false;
    public GameObject mainCamera;
    public GameObject glitchCamera;

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if (glitchScreen)
            {
                other.GetComponent<PlayerInput>().actions.Disable();
                StartCoroutine(GlitchAndLoad());
            }
            else
            {
                Time.timeScale = 0;
                gameManager.LoadSceneAsync(levelToLoad);
            }
        }
        
    }

    IEnumerator GlitchAndLoad()
    {
        mainCamera.SetActive(false);
        glitchCamera.SetActive(true);


        yield return new WaitForSeconds(3f);

        gameManager.LoadSceneAsync(levelToLoad);
    }

}
