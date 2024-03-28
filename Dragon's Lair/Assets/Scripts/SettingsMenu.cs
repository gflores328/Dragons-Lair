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
 * Used this tutorial:  https://www.youtube.com/watch?v=JRnNqQ2wbOU
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

    
}


    

    

