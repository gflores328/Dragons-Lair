using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RhythmGameAnimation : MonoBehaviour
{
    
    [Header("Different Poses")]

    public GameObject leftPose;
    public GameObject rightPose;
    public GameObject upPose;
    public GameObject downPose;
    public GameObject idlePose;

    [Header("The references to the Input Actions")]

    public InputActionReference leftAction;

    public InputActionReference rightAction;
    
    public InputActionReference downAction;

    public InputActionReference upAction;


    private bool isLeftPressed;

    private bool isRightPressed;
    
    private bool isUpPressed;

    private bool isDownPressed;

    
    void Start()
    {
        leftAction.action.started += OnLeftPressed;
        leftAction.action.canceled += OnLeftPressed;
        rightAction.action.started += OnRightPressed;
        rightAction.action.canceled += OnRightPressed;
        upAction.action.started += OnUpPressed;
        upAction.action.canceled += OnUpPressed;
        downAction.action.started += OnDownPressed;
        downAction.action.canceled += OnDownPressed;

    }

    // Update is called once per frame
    void Update()
    {
        if ( isLeftPressed )
        {
            setGameObject(leftPose, true);
            setGameObject(rightPose, false);
            setGameObject(upPose, false);
            setGameObject(downPose, false);
            setGameObject(idlePose, false);

        }

        else if ( isRightPressed )

        {
            setGameObject(leftPose, false);
            setGameObject(rightPose, true);
            setGameObject(upPose, false);
            setGameObject(downPose, false);
            setGameObject(idlePose, false);
        }

        else if ( isUpPressed )

        {
            setGameObject(leftPose, false);
            setGameObject(rightPose, false);
            setGameObject(upPose, true);
            setGameObject(downPose, false);
            setGameObject(idlePose, false);
        }

        else if ( isDownPressed )

        {
            setGameObject(leftPose, false);
            setGameObject(rightPose, false);
            setGameObject(upPose, false);
            setGameObject(downPose, true);
            setGameObject(idlePose, false);
        }

        else 

        {
            setGameObject(leftPose, false);
            setGameObject(rightPose, false);
            setGameObject(upPose, false);
            setGameObject(downPose, false);
            setGameObject(idlePose, true);
        }
        
    }

    private void OnLeftPressed(InputAction.CallbackContext context)

    {
        isLeftPressed = context.ReadValueAsButton();
    }
    private void OnRightPressed(InputAction.CallbackContext context)

    {
        isRightPressed = context.ReadValueAsButton();
    }
    private void OnUpPressed(InputAction.CallbackContext context)

    {
        isUpPressed = context.ReadValueAsButton();
    }
    private void OnDownPressed(InputAction.CallbackContext context)

    {
        isDownPressed = context.ReadValueAsButton();
    }

    private void setGameObject(GameObject gameObject, bool isOn)
    {
        gameObject.SetActive(isOn);
    }

}
