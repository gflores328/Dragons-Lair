using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [Header("Left and Right Components")]
    public bool isLeftAndRightInstructTrigger;
    public GameObject leftRightInstructs; // A public variable to grab the game object that displays the left and right instructions
    
    public GameObject leftRightKeyboard; // A public variable to grab the game obect that displays the keyboard keys for the left and right

    public GameObject leftRightController; // A public variable to grab the game object that displays the controller button for the left and right

    [Header("Jump Instruction Components")]
    public bool isJumpInstructTrigger;
    public GameObject jumpInstructs; // A public variable to grab the game object that displays the jump instructions
    
    public GameObject jumpKeyboard; // A public variable to grab the game obect that displays the keyboard keys jump
    public GameObject jumpController; // A public variable to grab the game object that displays the controller button jump 

    [Header("Fire Instruction Components")]
    public bool isFireTrigger;
    public GameObject fireInstructs; // A public variable to grab the game object that displays fire instructions
    
    public GameObject fireKeyboard; // A public variable to grab the game obect that displays the keyboard keys fire instructions

    public GameObject fireController; // A public variable to grab the game object that displays the controller button fire

    [Header("Fire Up Instruction Components")]
    public bool isFireUpTrigger;
    public GameObject fireUpInstructs; // A public variable to grab the game object that displays the fire up instructions
    
    public GameObject fireUpKeyboard; // A public variable to grab the game obect that displays the keyboard keys for the fire up

    public GameObject fireUpController; // A public variable to grab the game object that displays the controller button for the fire up

    [Header("Fire Diag Instruction Components")]
    public bool isFireDiagTrigger;
    public GameObject fireDiagInstructs; // A public variable to grab the game object that displays the fire diag instructions
    
    public GameObject fireDiagKeyboard; // A public variable to grab the game obect that displays the keyboard keys for the fire diag

    public GameObject fireDiagController; // A public variable to grab the game object that displays the controller button for the fire diag

    [Header("Free Aim Instruction Components")]
    public bool isFreeAimTrigger;
    public GameObject freeAimInstructs; // A public variable to grab the game object that displays the free aim instructions
    
    public GameObject freeAimKeyboard; // A public variable to grab the game obect that displays the keyboard keys for the free aim

    public GameObject freeAimController; // A public variable to grab the game object that displays the controller button for the free aim



    //private variables
    GameManager gameManager;


    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>(); // Finds the game manger in the scene
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))

        {
            if(isLeftAndRightInstructTrigger)
            {
                setLeftAndRightInstructionsActive();
                if(gameManager.GetIsMouse())
                {
                    leftRightKeyboard.SetActive(true);
                    leftRightController.SetActive(false);
                }
                else
                {
                    leftRightKeyboard.SetActive(false);
                    leftRightController.SetActive(true);
                }
            }

            else if(isJumpInstructTrigger)

            {
                setJumpInstructionsActive();

                if(gameManager.GetIsMouse())

                {
                    jumpKeyboard.SetActive(true);
                    jumpController.SetActive(false);
                }
                else
                {
                    jumpKeyboard.SetActive(false);
                    jumpController.SetActive(true);
                }

            }

            else if(isFireTrigger)

            {
                setFireInstructionsActive();

                if(gameManager.GetIsMouse())

                {
                    fireKeyboard.SetActive(true);
                    fireController.SetActive(false);
                }
                else
                {
                    fireKeyboard.SetActive(false);
                    fireController.SetActive(true);
                }

            } 
            
            else if(isFireUpTrigger)

            {
                setFireUpInstructionsActive();
                if(gameManager.GetIsMouse())

                {
                    fireUpKeyboard.SetActive(true);
                    fireUpController.SetActive(false);
                }
                else
                {
                    fireUpKeyboard.SetActive(false);
                    fireUpController.SetActive(true);
                }

            }

            else if(isFireDiagTrigger)

            {
                setFireDiagInstructionsActive();
                if(gameManager.GetIsMouse())

                {
                    fireDiagKeyboard.SetActive(true);
                    fireDiagController.SetActive(false);
                }
                else
                {
                    fireDiagKeyboard.SetActive(false);
                    fireDiagController.SetActive(true);
                }

            }

            else if(isFreeAimTrigger)

            {
                setFreeAimInstructionsActive();

                if(gameManager.GetIsMouse())

                {
                    freeAimKeyboard.SetActive(true);
                    freeAimController.SetActive(false);
                }
                else
                {
                    freeAimKeyboard.SetActive(false);
                    freeAimController.SetActive(true);
                }

            }
        }
    }

    private void OnTriggerExit(Collider other)

    {
        if ( other.gameObject.CompareTag("Player") )

        {

            turnOffInstructions(); // calls the turn off instructions so on exit the instructions go away

        }
    }
    private void setLeftAndRightInstructionsActive()
    {
        leftRightInstructs.SetActive(true);
        jumpInstructs.SetActive(false);
        fireInstructs.SetActive(false);
        fireUpInstructs.SetActive(false);
        fireDiagInstructs.SetActive(false);
        freeAimInstructs.SetActive(false);
    }
    private void setJumpInstructionsActive()
    {
        leftRightInstructs.SetActive(false);
        jumpInstructs.SetActive(true);
        fireInstructs.SetActive(false);
        fireUpInstructs.SetActive(false);
        fireDiagInstructs.SetActive(false);
        freeAimInstructs.SetActive(false);
    }
    private void setFireInstructionsActive() // Sets fire instruction to true and all other instructions to false
    {
        leftRightInstructs.SetActive(false);
        jumpInstructs.SetActive(false);
        fireInstructs.SetActive(true);
        fireUpInstructs.SetActive(false);
        fireDiagInstructs.SetActive(false);
        freeAimInstructs.SetActive(false);
    }

    private void setFireUpInstructionsActive() // Sets fire up instruction to true and all other instructions to false
    {
        leftRightInstructs.SetActive(false);
        jumpInstructs.SetActive(false);
        fireInstructs.SetActive(false);
        fireUpInstructs.SetActive(true);
        fireDiagInstructs.SetActive(false);
        freeAimInstructs.SetActive(false);
    }

    private void setFireDiagInstructionsActive() // Sets fire diag instruction to true and all other instructions to false
    {
        leftRightInstructs.SetActive(false);
        jumpInstructs.SetActive(false);
        fireInstructs.SetActive(false);
        fireUpInstructs.SetActive(false);
        fireDiagInstructs.SetActive(true);
        freeAimInstructs.SetActive(false);
    }

    private void setFreeAimInstructionsActive() // Sets Free Aim instruction to true and all other instructions to false
    {
        leftRightInstructs.SetActive(false);
        jumpInstructs.SetActive(false);
        fireInstructs.SetActive(false);
        fireUpInstructs.SetActive(false);
        fireDiagInstructs.SetActive(false);
        freeAimInstructs.SetActive(true);
    }

    private void turnOffInstructions()

    {
        leftRightInstructs.SetActive(false);
        jumpInstructs.SetActive(false);
        fireInstructs.SetActive(false);
        fireUpInstructs.SetActive(false);
        fireDiagInstructs.SetActive(false);
        freeAimInstructs.SetActive(false);
    }
}
