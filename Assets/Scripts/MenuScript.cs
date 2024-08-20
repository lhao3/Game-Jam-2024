using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void EnterOptions()
    {
        SceneManager.LoadScene("Options");
    }

    public void EnterTutorial()
    {
        SceneManager.LoadScene("TutorialLevel1");
    }
}
