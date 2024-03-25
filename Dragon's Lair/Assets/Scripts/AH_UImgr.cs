using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AH_UImgr : MonoBehaviour
{
    public GameObject pauseObject;
    public GameObject startMenu;
    public GameObject winObj;
    public GameObject lossObj;


    // Start is called before the first frame update
    void Start()
    {
        if (startMenu.activeSelf.Equals(true))
        {
            Time.timeScale = 0;
        }

        hidePaused();

    }

    // Update is called once per frame
    void Update()
    {

        //uses the p button to pause and unpause the game
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (Time.timeScale == 1)
            {
                Time.timeScale = 0;
                showPaused();
            }
            else if (Time.timeScale == 0)
            {
                //Debug.Log("high");
                Time.timeScale = 1;
                hidePaused();
            }
        }

        if (winObj.activeSelf.Equals(true))
        {
            Time.timeScale = 0;
        }

        if (lossObj.activeSelf.Equals(true))
        {
            Time.timeScale = 0;
        }

    }

    //controls the pausing of the scene
    public void pauseControl()
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
            showPaused();
        }
        else if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
            hidePaused();
        }

    }

    //shows objects with ShowOnPause tag
    public void showPaused()
    {
        pauseObject.SetActive(true);
    }

    //hides objects with ShowOnPause tag
    public void hidePaused()
    {
        pauseObject.SetActive(false);
    }

}
