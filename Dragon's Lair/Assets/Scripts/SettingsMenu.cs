/*
 * CREATED BY: Trevor Minarik
 * 
 * LAST MODIFIED BY: Trevor Minarik
 * LAST MODIFIED ON: Feb 26, 2024 at 11:46 PM
 * 
 * TUTORIAL FOLLOWED: "SETTINGS MENU in Unity" https://www.youtube.com/watch?v=YOaYQrN1oYQ
 * 
 * Handles functions of all the buttons, drop down boxes, and sliders seen in the options menu
<<<<<<< HEAD
 * 
 * 
 * Updated by Jo Marie Leatherman on 3/27/2024
 * 
 * changed resolution code to filter based upon current monitor settings and offer only options
 * that will work with the monitor in use. Tested sound and it functions fine. Unable to run builds 
 * on Mac outside the editor so another team member will need to test full settings menu functions 
 * on other devices to check cross-compatibility and make sure resolutions match.
 * 
 * Used this tutorial:  https://www.youtube.com/watch?v=JRnNqQ2wbOU
 * 
=======
>>>>>>> parent of 48c47f2 (Trying out a different way to do the resolution setting. Please test on yalls computers and see if it still has duplicated options or if it is filtering. Also check the full screen button and make sure sound works okay since I was moving some things around and want to make sure I didn't break any of the other parts of the settings menu - JML)
 */

using System.Collections;
using System.Collections.Generic;
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

    List<int>widths = new List<int>() { 568, 800, 960, 1024, 1280, 1440, 1710, 2048, 2560, 2880 };
    List<int> heights = new List<int>() { 320, 600, 600, 640, 800, 900, 1107, 1280, 1656, 1864};

    public void SetScreenSize(int index)
    {
        bool fullscreen = Screen.fullScreen;
        int width = widths[index];
        int height = heights[index];

        Screen.SetResolution(width, height, fullscreen);
        
    }

    public void GetFullScreen(bool _fullscreen)
    {
        Screen.fullScreen = _fullscreen;
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


    //An array that stores a list of resolutions
    private Resolution[] resolutions;
    private void Awake()
    {
        UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(backButton);
    }
    //This function is run when the scene starts
    //Initalizes the options in the 'resolutionDropdown' dropdown box and the position of the volume slider
    private void Start()
    {
        //Set up local variables

        //Gets the list of resolutions supported by the current monitor
        resolutions = Screen.resolutions;
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
        }

        //Update the options in the attached dropdown box

        //Populate the attached dropdown box with the resolution option strings
        resolutionDropdown.AddOptions(options);
        //Set the attached dropdown box to display the option that matches the current screen resolution
        resolutionDropdown.value = currentResolutionIndex;
        //Refresh the attached dropdown box to show the option that matches the current screen resolution
        resolutionDropdown.RefreshShownValue();

        //Update the default slider position to match the current audio mixer volume

        //A float for storing the volume
        float volume;
        //Get the current audio volume from the audio mixer
        audioMixer.GetFloat("Master", out volume);
        //Set the default slider value to the saved volume value
        volumeSlider.value = volume;



    }

    //Updates the current resolution of the game window based on the given resolution index
    public void SetResolution(int resolutionIndex)
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
    }

    /*
     * Controls the quality of the graphics based on the given quality index
     * A list of quality options can be found at Edit > Project Settings > Quality
     * 
     * The list of quality options found on the dropdown box is NOT directly connected to the options found in the project settings
     * Any adjustment to the options in project settings will have to be manually adjusted on the dropdown box
     */

    //Sets the volume of a specific chanel in the attached audio mixer equal to the given float value
    
}
    



    

    


    
