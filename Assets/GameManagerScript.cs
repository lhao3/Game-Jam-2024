using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameManagerScript instance;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private PlayerScript playerScript;
    [SerializeField] private GameObject options;
    private bool isMenuActive;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
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
        playerScript.EnablePlayerInput();
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");  //Quit button in pause menu returns to main menu
    }

}
