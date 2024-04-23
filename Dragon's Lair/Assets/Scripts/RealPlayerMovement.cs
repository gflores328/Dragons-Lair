using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//Created by Aaron Torres
//Script Title: Real PlayerMovement
//Description: This script will be handling the player movement in the real life section

public class RealPlayerMovement : MonoBehaviour
{
    [Header("Player Movement Settings")]
    public float playerSpeedMultiplier; // A float variable that will deteremine how fast the player is moving, The max force that can be applied to the movement, how powerful the jump is, how strong the gravity is.

    public float raycastLength;
    
    
    public bool inRealLife = true; // A public bool to determine if the player is in real life or the arcade chibi world used to restrict movement

    public Animator animator;
    private PlayerInput playerInput; // A private variable that is meant to grab the PlayerInput component that is attached to the player.

    private InputAction walkAction; // A private variable that is meant to hold the move action so that its values can be accessed

    private InputAction interactAction; // A private variable that is meant to hold the interact action

    private InputAction pauseAction; // A private variable that holds the pause action

    private AudioSource footStep; 

    private CharacterController characterController; // A Character Controller object which will hold the player's character controller

    private playerState currentPlayerState; // the state that will hold the players current state by using the playerState enum created below

    //private DialougeInteraction dialougeInteraction;
    //private MenuInteraction menuInteraction;

    //GABE ADDED
    //private ItemInteraction itemInteraction;
    //private DialogueTree dialogueTree;
    private Interact interact;
    private bool inventoryOpen = false;
    private InputAction inventoryAction;
    private InventoryUI inventoryUIArray;
    private GameObject inventoryUI;
    private bool gamePaused = false;



    private GameObject gameManagerObj;

    private GameManager gameManager;

    public GameObject currentInteractable;
    
    public enum playerState // An enum that has a real life and chibi state to easily determine what state the character is in
    {
        RealLife,
        Chbi,
    }
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>(); // Grabs the Player Input compoent from the player and assigns it to the playerInput that was initalized above
        walkAction = playerInput.actions.FindAction("Walk"); // Searches for the action and stores it inside of the  walk action variable
        interactAction = playerInput.actions.FindAction("Interact"); // Searches for the action and stores it inside of the  interact action variable
        interactAction.performed += OnInteract;
        pauseAction = playerInput.actions.FindAction("Pause"); // Searches for the action and stores it inside of the  interact action variable
        pauseAction.performed += Pause;
        gameManagerObj =  GameObject.Find("GameManager");
        gameManager = gameManagerObj.GetComponent<GameManager>();
        //Cursor.lockState = CursorLockMode.Locked;
        inventoryAction = playerInput.actions.FindAction("Inventory");
        inventoryAction.performed += OpenInventory;
        inventoryUIArray = FindObjectOfType<InventoryUI>(true);
        if (inventoryUIArray != null)
        {
            inventoryUI = inventoryUIArray.gameObject;
        }
        footStep = GetComponent<AudioSource>();

        Cursor.visible = true;
    }

    
    void FixedUpdate()
    {
       
        RealLifeMovePlayer(); // Calls the RealLifeMovePlayer function
        AdjustForSlope(); // Calls the Adjust for slope

       

    }

    void SwitchActionMap(string actionMapName) // The function that switches the action map.
    {
        playerInput.SwitchCurrentActionMap(actionMapName);
        walkAction = playerInput.currentActionMap.FindAction("Walk");
    }

    //MovePlayer Function Description:
    //Designed to be the function that will actively move the player object in the game. allowing movement in all directions 
    void RealLifeMovePlayer()
    {
        Vector2 direction = walkAction.ReadValue<Vector2>();
        Vector3 targetVelocity = new Vector3(direction.x, 0, direction.y);
        
        // Get the character's forward direction
        //Vector3 characterForward = transform.forward;

        // Get Camera direction
        Vector3 cameraForward = Camera.main.transform.forward;
        cameraForward.y = 0f; // Ensure no vertical component
        
        // Transform the movement direction based on the character's forward direction
        targetVelocity = Quaternion.LookRotation(cameraForward) * targetVelocity;
        
        targetVelocity *= playerSpeedMultiplier;

        if (targetVelocity.magnitude >= 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(targetVelocity.normalized);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 3f);
        }
        
        characterController.Move(targetVelocity * Time.deltaTime);

        if (targetVelocity.magnitude > 0) 
        {
            // Set the animator bool "isWalking" to true
            animator.SetBool("isWalking", true);
        } 
        else 
        {
            // Set the animator bool "isWalking" to false
            animator.SetBool("isWalking", false);
        }
        //footStep.enabled = true;

       
        
    }

    void AdjustForSlope()
    {
        RaycastHit hit;
        Vector3 raycastOrigin = transform.position + Vector3.up * 0.1f; // Offset the raycast origin slightly above the character

        // Perform raycast
        if (Physics.Raycast(raycastOrigin, Vector3.down, out hit, raycastLength))
        {
            float moveDistance = Mathf.Max(0, raycastLength - hit.distance); // Calculate how much to move down
            Vector3 moveDirection = Vector3.down;
            characterController.Move(moveDirection * moveDistance);

            //Debug.Log($"RayCast Hit: {hit.collider.gameObject.name}"); // Log information about the hit object
            Debug.DrawRay(raycastOrigin, Vector3.down * raycastLength, Color.green); // Visualize the raycast
        }
        else
        {
            //Debug.Log("RayCast did not hit anything.");

            // If raycast doesn't hit anything, move character down by the full raycast length
            characterController.Move(Vector3.down * raycastLength);

            Debug.DrawRay(raycastOrigin, Vector3.down * raycastLength, Color.red); // Visualize the raycast
        }
    }
    
    void OnInteract(InputAction.CallbackContext value)
    {
        if(currentInteractable != null && currentInteractable.tag == "InteractMenu")
        {
            //menuInteraction.Interact();
        }
        if(currentInteractable != null && currentInteractable.tag == "Interact")
        {
         //  dialougeInteraction.Interact();
        }

        // GABE ADDED
        if(currentInteractable != null && currentInteractable.tag == "InteractItem")
        {
            interact.Interacted();
        }

        if (GetComponent<DetectiveThoughts>().lostInThought)
        {
            GetComponent<DetectiveThoughts>().Interact();
        }
        
       if(currentInteractable != null && currentInteractable.tag == "DialogueTree")
        {
            //dialogueTree.Interact();
        }
    }
  
   private void OnTriggerEnter(Collider other)
   {
        if(other.tag == "InteractMenu")
        {
            currentInteractable = other.gameObject;
           // menuInteraction = currentInteractable.GetComponent<MenuInteraction>();
        }
        else if(other.tag == "Interact")
        {
            //Debug.Log(other.gameObject.name);
            currentInteractable = other.gameObject;
            //dialougeInteraction = currentInteractable.GetComponent<DialougeInteraction>();
        }

        // GABE ADDED
        else if (other.tag == "InteractItem")
        {
            //Debug.Log(other.gameObject.name);
            currentInteractable = other.gameObject;
            interact = currentInteractable.GetComponent<Interact>();
        }

        else if (other.tag == "DialogueTree")
        {
            //Debug.Log(other.gameObject.name);
            currentInteractable = other.gameObject;
           // dialogueTree = currentInteractable.GetComponent<DialogueTree>();
        }

    }
   
    private void Pause(InputAction.CallbackContext value)
    {

        if (inventoryAction.enabled && interactAction.enabled && walkAction.enabled)
        {
            inventoryAction.Disable();
            interactAction.Disable();
            walkAction.Disable();

            GameObject.Find("DialogueManager").GetComponent<DialogueManager>().EndDialogue();

            gameManager.PauseGame();

        }
        else
        {
            inventoryAction.Enable();
            interactAction.Enable();
            walkAction.Enable();

            gameManager.PauseGame();
        }
    }

    // GABE ADDED:
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "InteractMenu" || other.tag == "Interact" || other.tag == "InteractItem" || other.tag == "DialogueTree")
        {
            currentInteractable = null;
        }
    }

    private void OpenInventory(InputAction.CallbackContext value)
    {
        if (!inventoryOpen)
        {
            pauseAction.Disable();
            interactAction.Disable();
            walkAction.Disable();

            GameObject.Find("DialogueManager").GetComponent<DialogueManager>().EndDialogue();

            inventoryUI.SetActive(true);
           
            inventoryOpen = true;
        }
        else if (inventoryOpen)
        {
            inventoryUI.SetActive(false);
            inventoryOpen = false;

            pauseAction.Enable();
            interactAction.Enable();
            walkAction.Enable();

        }
    }

    public void EnablePlayerMovement(bool enable)
    {
        if (enable)
        {
            walkAction.Enable(); // Enable walk action
            interactAction.Enable(); // Enable interact action
            characterController.enabled = true;
        }
        else
        {
            walkAction.Disable(); // Disable walk action
            interactAction.Disable(); // Disable interact action
            characterController.enabled = false;
        }
    }

    public void SetPlayerPosition(Vector3 newPosition)
    {
        characterController.enabled = false; // Temporarily disable the character controller to directly set the position
        gameObject.transform.position = newPosition; // Set the player's position
        characterController.enabled = true; // Re-enable the character controller
    }

}
