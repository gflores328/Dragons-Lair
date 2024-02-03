/*
 * CREATED BY: Trevor Minarik
 * 
 * TUTORIAL FOLLOWED: "SETTINGS MENU in Unity" https://www.youtube.com/watch?v=YOaYQrN1oYQ
 * 
 * Handles functions of all the buttons, drop down boxes, and sliders seen in the options menu
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;    //Used by volume slider

public class SettingsMenu : MonoBehaviour
{
    //An audio mixer that recieves changes in volume
    public AudioMixer audioMixer;
    //A text mesh pro dropdown box.
    //Its options will be overwritten to match Unity's list based on the computer that is running the game
    public TMPro.TMP_Dropdown resolutionDropdown;

    //An array that stores a list of resolutions
    private Resolution[] resolutions;

    //This function is run when the scene starts
    //Initalizes the options in the 'resolutionDropdown' dropdown box
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
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    //Sets the volume of a specific chanel in the attached audio mixer equal to the given float value
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("masterVolume", volume);
    }
}
