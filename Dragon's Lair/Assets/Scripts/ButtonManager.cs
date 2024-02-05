using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    [Header("Buttons")]
    public Button startGame;
    public Button controls;
    public Button creditsScreen;
    public Button quitGame;
    public Button aboutGame;

    [Header("Panels")]
    [Tooltip("Panel GameObject in inspector pairs with a chosen UI panel to add make active/hide functionality")]
    public GameObject Panel;

    void Awake()
    {
        if (startGame != null) { startGame.gameObject.SetActive(true); }
        else if (controls != null) { controls.gameObject.SetActive(true); }
        else if (creditsScreen != null) { creditsScreen.gameObject.SetActive(true); }
        else if (quitGame != null) { quitGame.gameObject.SetActive(true); }
        else if (aboutGame != null) { aboutGame.gameObject.SetActive(true); }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void LoadLevel1()
    {
        SceneManager.LoadScene("Level1");
    }
    public void LoadLevelbyName(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }
}
