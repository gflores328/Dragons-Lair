using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;


public class PlayVideo : MonoBehaviour
{

    private VideoPlayer videoPlayer;
    private bool checkForFinish = false;

    public GameObject asyncLoader;

    // Start is called before the first frame update
    void Start()
    {
        videoPlayer = gameObject.GetComponent<VideoPlayer>();
        StartCoroutine(PlayWithDelay());
        
    }
    // Update is called once per frame
    void Update()
    {
        if (!videoPlayer.isPlaying && checkForFinish)
        {
            Debug.Log("Finished");
            asyncLoader.GetComponent<AsyncLoader>().runAsync();
        }
    }

    IEnumerator PlayWithDelay()
    {
        videoPlayer.Play();

        yield return new WaitForSeconds(5);
        checkForFinish = true;

    }
}

