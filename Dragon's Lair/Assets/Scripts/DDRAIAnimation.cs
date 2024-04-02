using UnityEngine;

public class DDRAIAnimation : MonoBehaviour
{
    // Array to hold different poses of the character
    public GameObject[] poses;

    // Interval between each pose change
    public float poseChangeInterval = 2f;

    // Time since the last pose change
    private float timeSinceLastPoseChange;

    // Index of the current pose in the poses array
    private int currentPoseIndex = -1;

    // Start is called before the first frame update
    void Start()
    {
        // Start by changing the pose immediately when the script starts
        ChangePose();
    }

    // Update is called once per frame
    void Update()
    {
        // Increment the time since the last pose change
        timeSinceLastPoseChange += Time.deltaTime;

        // Check if it's time to change the pose
        if (timeSinceLastPoseChange >= poseChangeInterval)
        {
            // Change the pose and reset the timer
            ChangePose();
            timeSinceLastPoseChange = 0f;
        }
    }

    // Function to change the character's pose
    private void ChangePose()
    {
        // Randomly select a new pose index from the poses array
        int newPoseIndex = Random.Range(0, poses.Length);

        // Ensure the new pose is different from the current pose
        while (newPoseIndex == currentPoseIndex)
        {
            newPoseIndex = Random.Range(0, poses.Length);
        }

        // Deactivate the current pose if it's not the first pose
        if (currentPoseIndex >= 0)
        {
            poses[currentPoseIndex].SetActive(false);
        }

        // Update the current pose index
        currentPoseIndex = newPoseIndex;

        // Activate the new pose
        poses[currentPoseIndex].SetActive(true);
    }
}
