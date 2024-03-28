/*
 * CREATED BY: Trevor Minarik
 * 
 * LAST MODIFIED BY: Trevor Minarik
 * LAST MODIFIED ON: Feb 26, 2024 at 11:46 PM
 * 
 * TUTORIAL FOLLOWED: "SETTINGS MENU in Unity" https://www.youtube.com/watch?v=YOaYQrN1oYQ
 * 
 * Handles functions of all the buttons, drop down boxes, and sliders seen in the options menu
 * 
 * 
 * Updated by Jo Marie Leatherman on 3/27/2024
 * 
 * changed resolution code to filter based upon current monitor settings and offer only options
 * that will work with the monitor in use. Tested sound and it functions fine. Unable to run builds 
 * on Mac outside the editor so another team member will need to test full settings menu functions 
 * on other devices to check cross-compatibility and make sure resolutions match.
 * 
 * 
 */

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;    //Used by volume slider
using UnityEngine.UI;       //Used by volume slider

public class SettingsMenu : MonoBehaviour
{
    //An audio mixer that recieves changes in volume
    public AudioMixer audioMixer;
    //A text mesh pro dropdown box.
    //Its options will be overwritten to match Unity's list based on the computer that is running the game
    public TMPro.TMP_Dropdown resolutionDropdown;
    //A slider that alters the master volume
    public Slider volumeSlider;

    public GameObject backButton; // The reference to the back button so the controller knows to be selected on it when it first loads in;


    //A float for storing the volume
    float volume;


    //An array that stores a list of resolutions
    //private Resolution[] resolutions;


    private Resolution currentResolution;

    // Fixed aspect ratio parameters
    static public bool FixedAspectRatio = true;
    static public float TargetAspectRatio = 4 / 3f;

    // Windowed aspect ratio when FixedAspectRatio is false
    static public float WindowedAspectRatio = 4f / 3f;

    // List of horizontal resolutions to include
    int[] resolutions = new int[] { 600, 800, 1024, 1280, 1400, 1600, 1920 };

    public Resolution DisplayResolution;
    public List<Vector2> WindowedResolutions, FullscreenResolutions;

    int currWindowedRes, currFullscreenRes;

    int currentResolutionIndex;

    private void Awake()
    {
        UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(backButton);

    }
    //This function is run when the scene starts
    //Initalizes the options in the 'resolutionDropdown' dropdown box and the position of the volume slider
    private void Start()
    {
        //Set up local variables


        StartCoroutine(StartRoutine());


        IEnumerator StartRoutine()
        {

            if (Application.platform == RuntimePlatform.OSXPlayer)
            {
                DisplayResolution = Screen.currentResolution;
            }
            else
            {
                if (Screen.fullScreen)
                {
                    Resolution r = Screen.currentResolution;
                    Screen.fullScreen = false;

                    yield return null;
                    yield return null;

                    DisplayResolution = Screen.currentResolution;

                    Screen.SetResolution(r.width, r.height, true);

                    yield return null;
                }
                else
                {
                    DisplayResolution = Screen.currentResolution;
                }

            }

            InitResolutions();
        }

    }


    private void InitResolutions()
    {
        float screenAspect = (float)DisplayResolution.width / DisplayResolution.height;

        WindowedResolutions = new List<Vector2>();
        FullscreenResolutions = new List<Vector2>();

        foreach (int w in resolutions)
        {
            if (w < DisplayResolution.width)
            {
                // Adding resolution only if it's 20% smaller than the screen
                if (w < DisplayResolution.width * 0.8f)
                {
                    Vector2 windowedResolution = new Vector2(w, Mathf.Round(w / (FixedAspectRatio ? TargetAspectRatio : WindowedAspectRatio)));
                    if (windowedResolution.y < DisplayResolution.height * 0.8f)
                        WindowedResolutions.Add(windowedResolution);

                    FullscreenResolutions.Add(new Vector2(w, Mathf.Round(w / screenAspect)));
                }
            }
        }

        // Adding fullscreen native resolution
        FullscreenResolutions.Add(new Vector2(DisplayResolution.width, DisplayResolution.height));

        // Adding half fullscreen native resolution
        Vector2 halfNative = new Vector2(DisplayResolution.width * 0.5f, DisplayResolution.height * 0.5f);
        if (halfNative.x > resolutions[0] && FullscreenResolutions.IndexOf(halfNative) == -1)
            FullscreenResolutions.Add(halfNative);

        FullscreenResolutions = FullscreenResolutions.OrderBy(resolution => resolution.x).ToList();

        bool found = false;

        if (Screen.fullScreen)
        {
            currWindowedRes = WindowedResolutions.Count - 1;

            for (int i = 0; i < FullscreenResolutions.Count; i++)
            {
                if (FullscreenResolutions[i].x == Screen.width && FullscreenResolutions[i].y == Screen.height)
                {
                    currFullscreenRes = i;
                    found = true;
                    break;
                }
            }

            if (!found)
                SetResolution(FullscreenResolutions.Count - 1, true);
        }
        else
        {
            currFullscreenRes = FullscreenResolutions.Count - 1;

            for (int i = 0; i < WindowedResolutions.Count; i++)
            {
                if (WindowedResolutions[i].x == Screen.width && WindowedResolutions[i].y == Screen.height)
                {
                    found = true;
                    currWindowedRes = i;
                    break;
                }
            }

            if (!found)
                SetResolution(WindowedResolutions.Count - 1, false);
        }
    }

    public void SetResolution(int index, bool fullscreen)
    {
        Vector2 r = new Vector2();
        if (fullscreen)
        {
            currFullscreenRes = index;
            r = FullscreenResolutions[currFullscreenRes];
        }
        else
        {
            currWindowedRes = index;
            r = WindowedResolutions[currWindowedRes];
        }

        bool fullscreen2windowed = Screen.fullScreen & !fullscreen;

        Debug.Log("Setting resolution to " + (int)r.x + "x" + (int)r.y);
        Screen.SetResolution((int)r.x, (int)r.y, fullscreen);

        // On OSX the application will pass from fullscreen to windowed with an animated transition of a couple of seconds.
        // After this transition, the first time you exit fullscreen you have to call SetResolution again to ensure that the window is resized correctly.
        if (Application.platform == RuntimePlatform.OSXPlayer)
        {
            // Ensure that there is no SetResolutionAfterResize coroutine running and waiting for screen size changes
            StopAllCoroutines();

            // Resize the window again after the end of the resize transition
            if (fullscreen2windowed) StartCoroutine(SetResolutionAfterResize(r));
        }
    }

    private IEnumerator SetResolutionAfterResize(Vector2 r)
    {
        int maxTime = 5; // Max wait for the end of the resize transition
        float time = Time.time;

        // Skipping a couple of frames during which the screen size will change
        yield return null;
        yield return null;

        int lastW = Screen.width;
        int lastH = Screen.height;

        //A list of screen resolution options saved as strings
        List<string> options = new List<string>();
        //Clears the placeholder options held by the attached dropdown box
        resolutionDropdown.ClearOptions();


        // Waiting for another screen size change at the end of the transition animation
        while (Time.time - time < maxTime)
        {
            if (lastW != Screen.width || lastH != Screen.height)
            {
                string option = (Screen.width + "x" + Screen.height);
                options.Add(option);
                currentResolutionIndex = +1;

                //If the selected resolution is the same as the current screen resolution, save the index of the selected resolution
                if (Screen.width == Screen.currentResolution.width &&
                    Screen.height == Screen.currentResolution.height)
                {
                    //Update the options in the attached dropdown box

                    //Populate the attached dropdown box with the resolution option strings
                    resolutionDropdown.AddOptions(options);
                    //Set the attached dropdown box to display the option that matches the current screen resolution
                    resolutionDropdown.value = currentResolutionIndex;
                    //Refresh the attached dropdown box to show the option that matches the current screen resolution
                    resolutionDropdown.RefreshShownValue();
                }

                //Debug.Log("End waiting");

                Screen.SetResolution((int)r.x, (int)r.y, Screen.fullScreen);
                yield break;
            }

            yield return null;
        }

    }



        //Controls whether the game window is full screen based on the given true or false value
        public void SetFullscreen(bool isFullscreen)
        {
            Screen.fullScreen = isFullscreen;
        }


        public void ToggleFullscreen()
        {
            SetResolution(
                Screen.fullScreen ? currWindowedRes : currFullscreenRes,
                !Screen.fullScreen);
        }


        public void SetQuality(int qualityIndex)
        {
            QualitySettings.SetQualityLevel(qualityIndex);
        }

        //Sets the volume of a specific chanel in the attached audio mixer equal to the given float value
        public void SetVolume(float volume)
        {
            //If the volume slider is at its minimum value, set the audio mixer to -80 db to properly mute the volume
            if (volume == volumeSlider.minValue)
            {
                audioMixer.SetFloat("Master", -80);
            }
            else
            {
                audioMixer.SetFloat("Master", volume);
            }

            //Update the default slider position to match the current audio mixer volume

            //Get the current audio volume from the audio mixer
            audioMixer.GetFloat("Master", out volume);
            //Set the default slider value to the saved volume value
            volumeSlider.value = volume;
        }

    
}

        /* currentResolution = Screen.currentResolution;

         //Gets the list of resolutions supported by the current monitor
         //resolutions = Screen.resolutions;


         //A list of screen resolution options saved as strings
         List<string> options = new List<string>();
         //Clears the placeholder options held by the attached dropdown box
         resolutionDropdown.ClearOptions();

         //Convert all of the supported screen resolutions to strings for displaying in the attached dropdown box

         //Used to automatically set dropdown box to the current screen resolution (once it's options have been populated)
         int currentResolutionIndex = 0;
         for (int i = 0; i < resolutions.Length; i++)
         {
             //Convert the selected resolution to a string
             string option = resolutions[i].width + " x " + resolutions[i].height;
             //Add the converted string to the list of resolution option strings
             options.Add(option);

             //If the selected resolution is the same as the current screen resolution, save the index of the selected resolution
             if (resolutions[i].width == Screen.currentResolution.width &&
                 resolutions[i].height == Screen.currentResolution.height)
             {
                 currentResolutionIndex = i;
             }
         }*/

        //Update the options in the attached dropdown box

        /*//Populate the attached dropdown box with the resolution option strings
        resolutionDropdown.AddOptions(options);
        //Set the attached dropdown box to display the option that matches the current screen resolution
        resolutionDropdown.value = currentResolutionIndex;
        //Refresh the attached dropdown box to show the option that matches the current screen resolution
        resolutionDropdown.RefreshShownValue();*/




        //Updates the current resolution of the game window based on the given resolution index
        /*public void SetResolution(int resolutionIndex)
        {
            //Gets the resolution from the list of resolutions supported by the monitor
            Resolution resolution = resolutions[resolutionIndex];
            //Set the screen size to the selected resolution
            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        }

        //Controls whether the game window is full screen based on the given true or false value
        public void SetFullscreen(bool isFullscreen)
        {
            Screen.fullScreen = isFullscreen;
        }*/

        /*
         * Controls the quality of the graphics based on the given quality index
         * A list of quality options can be found at Edit > Project Settings > Quality
         * 
         * The list of quality options found on the dropdown box is NOT directly connected to the options found in the project settings
         * Any adjustment to the options in project settings will have to be manually adjusted on the dropdown box
         */


    

    

