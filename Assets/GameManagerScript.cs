using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameManagerScript instance;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private PlayerScript playerScript;
    [SerializeField] private GameObject options;
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private Button quitButton;
    private bool isMenuActive;

    private void Awake()
    {

    }

    void Start()
    {
 
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isMenuActive = !isMenuActive;
            pauseMenu.SetActive(isMenuActive);

            if (isMenuActive)
            {
                Time.timeScale = 0f;
                playerScript.DisablePlayerInput();
            }
            else
            {
                options.SetActive(false);
                Time.timeScale = 1f;
                playerScript.EnablePlayerInput();
            }
        }
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            Destroy(gameObject); // Destroy the GameManager object
        }
    }

    public void continuePressed()
    {
        Time.timeScale = 1f;
        isMenuActive = false;
        pauseMenu.SetActive(isMenuActive);
        playerScript.EnablePlayerInput();

    }

    public void quitPressed()
    {
        StartCoroutine(QuitPressedCoroutine());
    }

    private IEnumerator QuitPressedCoroutine()
    {
        // Ensure the player input is enabled and time scale is reset
        playerScript.EnablePlayerInput();
        Time.timeScale = 1f;

        // Stop the music
        if (audioManager != null)
        {
            //audioManager.Stop("levelmusic");
        }

        // Optionally, wait for a frame or two to ensure the music stop is processed
         yield return null;


        // Load the Main Menu scene
        SceneManager.LoadScene("MainMenu");
    }

}
